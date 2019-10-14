using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TerrainFactory tFactory;

    [SerializeField] Enemy [] preEnemies;

    static List<Enemy> enemies;

    float repulsionStrength = 1;

    void Start()
    {
        
    }
    private void Awake()
    {
        if(enemies==null)
        {
            enemies = new List<Enemy>();
        }
    }
    public void Initialize()
    {
        SpawnBatch(1000);
    }

    public void Spawn(int index)
    {
        if(index<0||index>preEnemies.Length-1)
        {
            return;
        }
        Enemy e = Instantiate(preEnemies[index]);

        //e.transform.parent = transform;

        int y = Random.Range(0, 3);
        Vector2Int pos;
        int doomCounter = 0;
        if(Random.Range(0,4)==0)
        {
            while(true)
            {
                pos = new Vector2Int(0, Random.Range(0, tFactory.getSize().y));
                if(((Terrain)tFactory.Find(pos)).distToGen<9999)
                {
                    break;
                }
                if(doomCounter>100)
                {
                    Destroy(e.gameObject);
                    return;
                }
                doomCounter++;
            }
        }
        else if(Random.Range(0, 3) == 0)
        {
            while (true)
            {
                pos = new Vector2Int(tFactory.getSize().x-1, Random.Range(0, tFactory.getSize().y));
                if (((Terrain)tFactory.Find(pos)).distToGen < 9999)
                {
                    break;
                }
                if (doomCounter > 100)
                {
                    Destroy(e.gameObject);
                    return;
                }
                doomCounter++;
            }
        }
        else if (Random.Range(0, 2) == 0)
        {
            while (true)
            {
                pos = new Vector2Int( Random.Range(0, tFactory.getSize().x),0);
                if (((Terrain)tFactory.Find(pos)).distToGen < 9999)
                {
                    break;
                }
                if (doomCounter > 100)
                {
                    Destroy(e.gameObject);
                    return;
                }
                doomCounter++;
            }
        }
        else
        {
            while (true)
            {
                pos = new Vector2Int(Random.Range(0, tFactory.getSize().x), tFactory.getSize().y-1);
                if (((Terrain)tFactory.Find(pos)).distToGen < 9999)
                {
                    break;
                }
                if (doomCounter > 100)
                {
                    Destroy(e.gameObject);
                    return;
                }
                doomCounter++;
            }
        }

        e.Instantiate(Cell.getGlobalCoords(pos,55f/64f), tFactory, this);
    }
    public void SpawnBatch(int count)
    {
        for(int i = 0;i<count;i++)
        {
            Spawn(0);
        }
    }

    public void AddToList(Enemy e)
    {
        if (e != null)
        {
            enemies.Add(e);
        }
    }
    public void removeFromList(Enemy e)
    {
        if(e!=null)
        {
            enemies.Remove(e);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
