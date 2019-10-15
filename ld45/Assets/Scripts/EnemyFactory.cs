using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TerrainFactory tFactory;
    [SerializeField] BuildingFactory bFactory;

    [SerializeField] Enemy[] preEnemies;

    static List<Enemy> enemies;

    float repulsionStrength = 1;

    void Start()
    {

    }
    private void Awake()
    {
        if (enemies == null)
        {
            enemies = new List<Enemy>();
        }
    }
    public void Initialize()
    {
        SpawnBatch(100);
    }

    public void Spawn(int index)
    {
        if (index < 0 || index > preEnemies.Length - 1)
        {
            return;
        }
        Enemy e = Instantiate(preEnemies[index]);

        //e.transform.parent = transform;

        int y = Random.Range(0, 3);
        Vector2Int pos;
        int doomCounter = 0;
        if (Random.Range(0, 4) == 0)
        {
            while (true)
            {
                pos = new Vector2Int(0, Random.Range(0, tFactory.getSize().y));
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
        else if (Random.Range(0, 3) == 0)
        {
            while (true)
            {
                pos = new Vector2Int(tFactory.getSize().x - 1, Random.Range(0, tFactory.getSize().y));
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
                pos = new Vector2Int(Random.Range(0, tFactory.getSize().x), 0);
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
                pos = new Vector2Int(Random.Range(0, tFactory.getSize().x), tFactory.getSize().y - 1);
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

        e.Instantiate(Cell.getGlobalCoords(pos, 55f / 64f), tFactory, this);
    }
    public void SpawnBatch(int count)
    {
        for (int i = 0; i < count; i++)
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
        if (e != null)
        {
            enemies.Remove(e);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float ddist;
        Vector2 dir;
        Vector2 bPos;
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = i + 1; j < enemies.Count; j++)
            {
                if (enemies[i].transform.localPosition.x - enemies[j].transform.localPosition.x > 1.5f || enemies[i].transform.localPosition.y - enemies[j].transform.localPosition.y > 1.5f)
                {
                    continue;
                }
                dir = enemies[i].transform.localPosition - enemies[j].transform.localPosition;
                ddist = Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2);
                if (ddist < 1)
                {
                    dir = (Vector2)Vector3.Normalize(dir);
                    enemies[i].vel += dir * (1/ (ddist + 0.3f)) * Time.fixedDeltaTime;
                    enemies[j].vel -= dir * (1 / (ddist + 0.3f)) * Time.fixedDeltaTime;
                }
            }
        }
        Building building;
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < bFactory.getCellCount(-1); j++)
            {
                building = (Building)bFactory.Find(j);
                dir = (Vector2)enemies[i].transform.localPosition - building.getLocalPos();
                if(dir.x>1.5f||dir.y>1.5f)
                {
                    continue;
                }
                ddist = Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2);
                if (ddist < 1)
                {
                    dir = (Vector2)Vector3.Normalize(dir);
                    enemies[i].vel += dir * (4 / (ddist + 0.3f)) * Time.fixedDeltaTime;
                }
            }
        }
    }
}
