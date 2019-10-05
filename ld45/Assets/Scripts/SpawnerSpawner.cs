using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawner : MonoBehaviour
{
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
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(toSpawn, (Vector3) RandomUnitVector()*distance + gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
        DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, RandomUnitVector()*distance);
        Debug.Log(RandomUnitVector()*distance);
    }
}
