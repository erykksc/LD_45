using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawner : MonoBehaviour
{
    public int Delay = 10;
    private int passed = 0;
    // Start is called before the first frame update
    public float distance;
    public GameObject toSpawn;
    
    public Vector2 RandomUnitVector()
    {
        float random = Random.Range(0f, 260f);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    void FixedUpdate()
    {
        if (Delay <= passed)
        {
            Spawn();
            passed = 0;
        }
        else
        {
            passed++;
        }
    }
    public void Spawn()
    {
        Instantiate(toSpawn, (Vector3) RandomUnitVector()*distance + gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
    }
}
