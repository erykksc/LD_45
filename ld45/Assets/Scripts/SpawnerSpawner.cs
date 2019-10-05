using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawner : MonoBehaviour
{
    private int passed = 0;
    // Start is called before the first frame update
    [SerializeField] private float distance;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject enemyPrefab;
    
    public Vector2 randomUnitVector()
    {
        float random = Random.Range(0f, 260f);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public void Spawn(float distanceFromMainSpawner, int numOfEnemiesInGroup, int numOfGroups, float spawnRateOfMembers)
    {
        for(int i=0; i<numOfGroups; i++)
        {
            spawnSpawner(enemyPrefab, numOfEnemiesInGroup, spawnRateOfMembers);
        }
    }

    public void spawnSpawner(GameObject enemyPrefab, int numOfEnemies, float spawnRate, bool destroyAfterSpawning=true)
    {

        GameObject newSpawner = Instantiate(spawner, (Vector3) randomUnitVector()*distance + gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
        newSpawner.GetComponent<EnemySpawner>().setProperties(enemyPrefab, numOfEnemies, spawnRate, destroyAfterSpawning);
    }

    private void Start() {
        Spawn(25, 5, 2, 0.0f);
    }
}
