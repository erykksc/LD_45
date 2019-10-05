using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //public static Vector2Int toHexCoords(Vector2 pos)
    //{

    //}
    //These are the neighbouring tiles
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
    public Cell right = null , lup = null , ldown = null, left = null,rup = null,rdown = null;
    
    public void Instantiate(Vector2Int p)
    {
        pos = p;
        transform.localPosition = getGlobalCoords(pos,1);
    }

    //This is on only ONCE per energy cycle. Used for singe-time actions
    public bool active = false;

    //This determines the energy of the tile
    public bool isActivated = false;

    public int timesActivated = 0;

    public Vector2Int pos;

    public static float timeStep = 0.5f;

    [SerializeField] private int hp = 10000;

    public Sprite [] sprites;


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

    public void getImpulse(Cell parent)
    {
        // Debug.Log("from getImpulse");
        if(parent.timesActivated>timesActivated)
        {
            //Debug.Log("got impulse");
            isActivated = true;

            try {  }
            catch { }

            WhenActivatedDoOnce();
            timesActivated = parent.timesActivated;
            StartCoroutine(propagateImpuls());
            
        }
    }
    public virtual void WhenActivatedDoOnce()
    {
    }


    // coroutine
    public IEnumerator propagateImpuls()
    {
        for(int i = 0;i<sprites.Length;i++)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            yield return new WaitForSeconds(timeStep / (sprites.Length-1));
        }
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        //GetComponent<SpriteRenderer>().color = Color.blue;
        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(timeStep);
        if(right!=null)
        {
            right.getImpulse(this);
        }
        if (left != null)
        {
            left.getImpulse(this);
        }
        if (rup != null)
        {
            rup.getImpulse(this);
        }
        if (rdown != null)
        {
            rdown.getImpulse(this);
        }
        if (lup != null)
        {
            lup.getImpulse(this);
        }
        if (ldown != null)
        {
            ldown.getImpulse(this);
        }
        isActivated = false;
        //GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        //GetComponent<SpriteRenderer>().color = Color.green;
        //GetComponent<SpriteRenderer>().color = Color.green;
        //GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void Update()
    {

    }

    // Update is called once per frame

    public void FixedUpdate()
    {
        //Setting sprite accordingly to cell's activation state
        //if (isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteDeactivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteActivated;
        //if (!isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteActivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteDeactivated;
    }


    //This will ensure that this GameObject is at coordinates expressed in Int values
    public void SnapToIntPosition()
    {
        Vector2 SnappedPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        transform.position = SnappedPosition;
    }
    ~Cell()
    {
        left.right = null;
        right.left = null;
        lup.rdown = null;
        rdown.lup = null;
        rup.ldown = null;
        ldown.rup = null;
    }

}
