using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    protected bool isShown = true;

    public bool isCellShown
    {
        get
        {
            return isShown;
        }
        set
        {
            isShown = value;
            if (!isShown&&renderer!=null)
            {
                Debug.Log("deactivated");
                renderer.sprite = null;
            }
        }

    }
    [SerializeField] private Cell[] neighbours = { null, null, null, null, null, null, };

    public int distToGen;//Dijkstra

    protected CellFactory factory;

    public CellFactory getFactory()
    {
        return factory;
    }

    public void setNeighbour(Cell prop, int index)
    {
        neighbours[index] = prop;
        if (prop == null)
        {
            return;
        }
        prop.neighbours[(index + 3) % 6] = this;
    }
    public Cell getNeighbour(int index)
    {
        return neighbours[index];
    }

    public int ID;
    public Vector2Int pos;
    protected SpriteRenderer renderer;

    public static float animationDuration = 0.75f;

    public int animationOffset;

    [SerializeField] protected Sprite[] sprites;
    public int animationPerUpgrade;
    

    public static Vector2 getGlobalCoords(Vector2Int pos, float size)
    {
        return new Vector2((-(pos.y % 2) * 0.5f + pos.x) * size, (pos.y * Mathf.Sqrt(3) * 0.5f) * size);
    }
    public static Vector2Int getHexCoords(Vector2 pos, float size)
    {
        float xstep, ystep;
        xstep = size;
        ystep = size * Mathf.Sqrt(3) * 0.5f;
        int x, y;
        x = (int)Mathf.Round(pos.x / xstep);
        y = (int)Mathf.Round(pos.y / ystep);
        float min = float.MaxValue;
        Vector2Int mV = new Vector2Int(0, 0);
        Vector2 tPos = new Vector2(0, 0);
        for (int i = x - 2; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                tPos = getGlobalCoords(new Vector2Int(i, j), size);
                if (Mathf.Pow(tPos.x - pos.x, 2) + Mathf.Pow(tPos.y - pos.y, 2) < min)
                {
                    min = Mathf.Pow(tPos.x - pos.x, 2) + Mathf.Pow(tPos.y - pos.y, 2);
                    mV = new Vector2Int(i, j);
                }
            }
        }
        return mV;
    }
    
    public void InstantiateCell(Vector2Int p,int index,CellFactory fac, bool shown = false)
    {
        pos = p;
        transform.position = getGlobalCoords(pos,55f/64f);
        ID = index;
        factory = fac;
        isCellShown = shown;
    }
    
    public Vector2Int getHexPos()
    {
        return pos;
    }
    public Vector2 getLocalPos()
    {
        return transform.localPosition;
    }

    public void Awake()
    {
        if(sprites==null)
        {
            return;
        }
        if(sprites.Length==0)
        {
            return;
        }
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
    }
    private void Update()
    {
    }
    

    protected void OnDestroy()
    {
        factory.removeFromList(this);
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                neighbours[i].neighbours[(i + 3) % 6] = null;
            }
        }
    }
}
