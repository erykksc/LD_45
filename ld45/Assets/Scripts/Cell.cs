using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Propagateable
{
    private Cell c1;
    public bool UpgradeWindowShowing = false;
    public GameObject UpgradeInterface;
    //public static Vector2Int toHexCoords(Vector2 pos)
    //{

    //}
    //These are the neighbouring tiles
    //This is on only ONCE per energy cycle. Used for singe-time actions
    //public bool active = false;

    //This determines the energy of the tile
    //public bool isActivated = false;

    //public int timesActivated = 0;
    public int []health;
    public int []moneyps;
    public float []range;
    public int []damage;
    public int []rays;
    public int []selfHeal;
    public int []cash;
    

    public int level = 0;
    
    public bool buildable;
    public bool isWater;

    public static float timeStep = 0.75f;

    [SerializeField] private int hp = 10000;

    public Sprite[] sprites;
    public int animationLength;

    public void Upgrade()
    {
        if(level+1<health.Length)
        {
            level++;
            health[0] = health[level];
            damage[0] = damage[level];
            selfHeal[0] = selfHeal[level];
            rays[0] = rays[level];
            moneyps[0] = moneyps[level];
            range[0] = range[level];
        }
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
        transform.localPosition = getGlobalCoords(pos,55f/64f);
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

    //Returns True if the cell has been destroyed
    public bool dealDamage(int damage)
    {
        //Debug.Log("a");
        health[0] -= damage;

        if (health[0] <= 0)
        {
            for(int i = 0;i<6;i++)
            {
                if(neighbours[i]!=null)
                {
                    neighbours[i].neighbours[(i + 3) % 6] = null;
                }
                neighbours[i] = null;
            }
            GameObject.Destroy(gameObject);
            return true;
        }
        if(health[0]-damage>health[level])
        {
            health[0] = health[level];
            
        }
        return false;
    }
    Vector2Int getPos()
    {
        return pos;
    }

    public void Awake()
    {

        GetComponent<SpriteRenderer>().sprite = sprites[0];
        setPulseAction(action);
        level = 0;
        health = new int[2];
        range = new float[2];
        rays = new int[2];
        moneyps = new int[2];
        cash = new int[2];
        selfHeal = new int[2];
        damage = new int[2];

        health[1] = 100;
        range[1] = 15;
        rays[1] = 0;
        moneyps[1] = 2;
        cash[1] = 100;
        damage[1] = 50;
        selfHeal[1] = 10;

        Upgrade();
    }

    private void Update()
    {
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && UpgradeWindowShowing==false ) { Debug.LogError("Opening Upgrades"); UpgradeWindowShowing = true;       ToggleUpgradeUI();  }
    }
    public void OnMouseExit()
    {
        if (UpgradeWindowShowing) { Debug.LogError("Closing Upgrades"); UpgradeWindowShowing = false;         ToggleUpgradeUI();   }
    }
    public void ToggleUpgradeUI()
    {
        //When upgrade window is not displayed load it from resources
        if (UpgradeWindowShowing)
        {
            GameObject Upgrader = Resources.Load<GameObject>("UpgraderMk4") as GameObject;
            Transform TargetTransform   =   Camera.main.transform;
            foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
            {
                if (trans.gameObject.name == "Canvas") TargetTransform = trans;
            }


            GameObject InstanceOfUpgrader = Instantiate(Upgrader, transform.position, Quaternion.identity, TargetTransform);



            UpgradeInterface = InstanceOfUpgrader;
        }

        //otherwise destroy it
        else
        {
            try {    Destroy(UpgradeInterface); }
            catch { }
        }


    }


    public void destroyCell()
    {
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                neighbours[i].neighbours[(i + 3) % 6] = null;
            }
            neighbours[i] = null;
        }
        Destroy(gameObject);
    }

    ~Cell()
    {
        for(int i = 0;i<6;i++)
        {
            if(neighbours[i]!=null)
            {
                neighbours[i].neighbours[(i + 3) % 6] = (Propagateable)null;
            }
            neighbours[i] = (Propagateable)null;
        }
    }

}
