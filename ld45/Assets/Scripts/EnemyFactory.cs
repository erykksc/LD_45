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
        Spawn(0);
    }

    public void Spawn(int index)
    {
        if(index<0||index>preEnemies.Length-1)
        {
            return;
        }
        Enemy e = Instantiate(preEnemies[index]);

        int y = Random.Range(0, 4);
        Vector2Int pos;
        //if(y==0)
        {
            while(true)
            {
                pos = new Vector2Int(0, Random.Range(0, tFactory.getSize().y));
                if(((Terrain)tFactory.Find(pos)).distToGen<9999)
                {
                    break;
                }
            }
        }
        e.Instantiate(Cell.getGlobalCoords(pos,55f/64f), tFactory, this);
    }
    public void SpawnBatch(int count)
    {

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
        Vector3 dist;
        Vector2 repulsion;
        float ddist;
        
    }
}
