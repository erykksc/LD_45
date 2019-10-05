using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int enemies2Spawn = 5;
    [SerializeField] private bool destroyAfterSpawning = true;
    private float nextSpawn = 0.0f;

    public void setEnemies2Spawn(int numOfEnemies)
    {
        enemies2Spawn = numOfEnemies;
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
        if (Time.time > nextSpawn && enemies2Spawn > 0)
        {
            enemies2Spawn -= 1;
            nextSpawn = Time.time + spawnRate;
            GameObject Enemy = Instantiate(enemyPrefab, gameObject.transform);
        }
        else if (enemies2Spawn <= 0 && destroyAfterSpawning)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
