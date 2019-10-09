using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFactory : CellFactory
{
    // Start is called before the first frame update
    bool getRandomBool(float prob)
    {
        prob *= 1000f;
        return Random.Range(0, 1001) <= prob;
    }

    private void Awake()
    {
        base.Awake();
        GenerateMap();
    }

    delegate bool distribution (Vector2Int pos,float size, float ratio);

    bool Distr1(Vector2Int pos,float size,float ratio)
    {
        float dist = Mathf.Pow((pos.y) / ratio, 2) + Mathf.Pow(pos.x, 2);
        return dist < Mathf.Pow(size, 2);
    }

    [SerializeField] private int buildable;

    [SerializeField] private Vector2Int size;

    Vector2Int getNeighbourGridPos(Vector2Int pos, int index)
    {
        if (index == 0)
        {
            return new Vector2Int(pos.x + 1, pos.y);
        }
        if (index == 3)
        {
            return new Vector2Int(pos.x - 1, pos.y);
        }
        if (index == 1)
        {
            return new Vector2Int(pos.x - (pos.y ) % 2, pos.y + 1);
        }
        if (index == 4)
        {
            return new Vector2Int(pos.x - (pos.y ) % 2 + 1, pos.y - 1);
        }
        if (index == 2)
        {
            return new Vector2Int(pos.x - (pos.y ) % 2, pos.y - 1);
        }
        if (index == 5)
        {
            return new Vector2Int(pos.x - (pos.y ) % 2 + 1, pos.y + 1);
        }
        return new Vector2Int(-1, -1);
    }

    public void GenerateMap()
    {
        GenerateGrid();
        if (Random.Range(0, 2) == 1)
        {
            GenerateLine(new Vector2Int(Random.Range(0, size.x), 0), 0, 200, 2);
        }
        else
        {
            GenerateLine(new Vector2Int(0,Random.Range(0, size.y)), 0, 200, 2);
        }
        float rratio = 0.4f + Random.Range(0, 100) / 50f;
        GenerateSpot(new Vector2Int(20, 20), 4, 9,Distr1, rratio);
        GenerateSpot(new Vector2Int(20, 20), 7, 7,Distr1, rratio);
        Debug.Log($"cells Count: {cells.Count}");
    }

    private int hexToIndex(Vector2Int pos)
    {
        return pos.y + pos.x * size.x;
    }

    public void Swap(Cell a, Cell b)
    {
        if(b==null||a==null)
        {
            Debug.Log("In TerrainFactory, Swap: on or more parameters is null");
            return;
        }
        for(int i = 0;i<cells.Count;i++)
        {
            if(cells[i] == a)
            {
                for(int k = 0;k<6;k++)
                {
                    b.setNeighbour(a.getNeighbour(k), k);
                    a.setNeighbour(null, k);
                }
                cells[i] = b;
                b.isCellShown = true;
                Destroy(a.gameObject);
                break;
            }
        }
    }

    private void GenerateGrid()
    {
        for(int i = 0,c = 0;i<size.x;i++)
        {
            for(int j =0;j<size.y;j++,c++)
            {
                cells.Add(Add(new Vector2Int(i, j), 0));
                cells[c].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 60 * Random.Range(0, 7)));
            }
        }
        Vector2Int neighbour;
        for(int i = 0;i<size.x;i++)
        {
            for(int j = 0;j<size.y;j++)
            {
                for(int k = 0;k<6;k++)
                {
                    neighbour = getNeighbourGridPos(new Vector2Int(i, j), k);
                    if(neighbour.x>-1&&neighbour.x<size.x&&neighbour.y>-1&&neighbour.y<size.y&&neighbour.x!=-1&&neighbour.y!=-1)
                    {
                        cells[hexToIndex(new Vector2Int(i, j))].setNeighbour(cells[hexToIndex(neighbour)], k);
                    }
                }
            }
        }
    }

    void GenerateLine(Vector2Int pos,int dir,int size,int index)
    {
        Cell origin;
        Cell prev = Find(pos);
        if(prev == null)
        {
            Debug.Log("In TerrainFactory, GenerateLine: input is null");
            return;
        }
        for(int i = 0;i<size&&prev!=null;i++)
        {
            pos = prev.pos;
            origin = Add(pos, index);
            prev = Find(pos);
            if(prev==null)
            {
                Destroy(origin.gameObject);
                break;
            }
            Swap(prev, origin);
            prev = origin.getNeighbour((dir + Random.Range(0, 3) +5 ) % 6);
        }
    }
    void GenerateSpot(Vector2Int pos, int index, float size,distribution distr, float ratio = 1)
    {
        float prob;
        Cell temp;
        Cell origin;
        Vector2Int p;
        float dist;
        for (int i = (int)(pos.x - size); i < pos.x + size; i++) 
        {
            for(int j = (int)(pos.y - size * ratio); j<pos.y+size*ratio;j++)
            {
                p = new Vector2Int(i, j);
                dist = Mathf.Pow((j - pos.y)/ratio, 2) + Mathf.Pow(i - pos.x, 2);

                if(distr(p-pos,size,ratio))
                {
                    origin = Add(p, index);
                    temp = Find(p);
                    Swap(temp, origin);
                }
            }
        }
    }
    

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
