using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject []enemyPrefab;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int numOfEnemies2Spawn = 5;
    [SerializeField] private bool destroyAfterSpawning = true;
    private float nextSpawn = 0.0f;

    public void setProperties(GameObject newEnemyPrefab, int newNumOfEnemies, float newSpawnRate, bool newDestroyAfterSpawning=true)
    {
        //enemyPrefab = newEnemyPrefab;
        setNumOfEnemies2Spawn(newNumOfEnemies);
        setSpawnRate(newSpawnRate);
        setDestroyAfterSpawning(newDestroyAfterSpawning);
    }

    public void setNumOfEnemies2Spawn(int newEnemies2Spawn)
    {
        numOfEnemies2Spawn = newEnemies2Spawn;
    }

    public void setSpawnRate(float newSpawnRate)
    {
        spawnRate = newSpawnRate;
    }

    public void setDestroyAfterSpawning(bool newBool)
    {
        destroyAfterSpawning = newBool;
    }

    private void FixedUpdate() {
        if (Time.time > nextSpawn && numOfEnemies2Spawn > 0)
        {
            numOfEnemies2Spawn -= 1;
            nextSpawn = Time.time + spawnRate;
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length+1)], gameObject.transform.position, Quaternion.identity);
        }
        else if (numOfEnemies2Spawn <= 0 && destroyAfterSpawning)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
