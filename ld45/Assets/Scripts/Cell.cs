using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Propagateable
{
    //public static Vector2Int toHexCoords(Vector2 pos)
    //{

    //}
    //These are the neighbouring tiles
    //This is on only ONCE per energy cycle. Used for singe-time actions
    //public bool active = false;

    //This determines the energy of the tile
    //public bool isActivated = false;

    //public int timesActivated = 0;

    

    public Vector2Int pos;

    public static float timeStep = 0.5f;

    [SerializeField] private int hp = 10000;

    public Sprite[] sprites;


    static void Swap(ref Cell a,ref Cell b)
    {
        for(int i = 0;i<6;i++)
        {
            if(a.neighbours[i]!=null)
            {
                a.neighbours[i].neighbours[(i + 3) % 6] = b;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            if (b.neighbours[i] != null)
            {
                b.neighbours[i].neighbours[(i + 3) % 6] = a;
            }
        }
        Propagateable[] neighbours = new Propagateable[6];
        neighbours = a.neighbours;
        a.neighbours = b.neighbours;
        b.neighbours = neighbours;
        Vector2Int pos = a.pos;
        a.pos = b.pos;
        b.pos = pos;
    }

    public void action()
    {
        StartCoroutine(animate());
        onImpulse();
    }
    public virtual void onImpulse()
    {
        
    }
    public IEnumerator animate()
    {
        for(int i =0;i<sprites.Length;i++)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            yield return new WaitForSeconds(timeStep / (sprites.Length-1));
        }
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

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
        //Debug.Log($"data:{y}");
        float min = float.MaxValue;
        Vector2Int mV = new Vector2Int(0, 0);
        Vector2 tPos = new Vector2(0, 0);
        for (int i = x - 3; i < x + 3; i++)
        {
            for (int j = y - 3; j < y + 3; j++)
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
    
    public void Instantiate(Vector2Int p)
    {
        pos = p;
        transform.localPosition = getGlobalCoords(pos,1);
    }

    public void InstantiateCell(Vector2Int p)
    {
        pos = p;
        transform.position = (Vector2)pos;
    }
    public int getHp()
    {
        return hp;
    }
    public void setHp(int newHp)
    {
        hp = newHp;
    }

    public void dealDamage(int damage)
    {
        if (damage > 0)
        {
            hp -= damage;
        }

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    Vector2Int getPos() { return pos; }

    /// Function
    /*public IEnumerator animate(int duration)
    {
        Color color = new Color(1, 1, 1);
        float t = 0;
        while(t<duration)
        {
            color.r = 1 * (t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }*/


    // coroutine


    public void Awake()
    {

        GetComponent<SpriteRenderer>().sprite = sprites[0];
        setPulseAction(action);
        //GetComponent<SpriteRenderer>().color = Color.green;
        //GetComponent<SpriteRenderer>().color = Color.green;
        //GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void Update()
    {

    }

    public void FixedUpdate()
    {
    }
    

    ~Cell()
    {
        Debug.Log("Destructor called");
        for(int i = 0;i<6;i++)
        {
            if(neighbours[i]!=null)
            {
                neighbours[i].neighbours[(i + 3) % 6] = null;
            }
        }
    }

}
