using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public GameObject Target;

    GameObject GetTarget()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Cell");
        //Debug.Log(objects.Length);
        
        GameObject lastObject = gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject x in objects)
        {
            Vector3 diff = x.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
            float dist = diff.sqrMagnitude;
            if (dist < distance)
            {
                Debug.Log("ok");
                lastObject = x;
                distance = dist;
            }
        }
        
        return lastObject;
    }
    void Start()
    {
        Target = GetTarget();   
    }



    void Update()
    {
        
    }
}
