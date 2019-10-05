using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public GameObject Target;
    public float maxSpeed;
    public float speed;

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



    void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
        //construct movement vector
        Vector2 position = gameObject.GetComponent<Transform>().position;
        Vector2 playerPos = Target.GetComponent<Transform>().position;
        //Debug.Log(playerPos);
        Vector2 move = playerPos - position;

        move = move.normalized * speed;
        //normalize
        gameObject.GetComponent<Rigidbody2D>().AddForce(move,ForceMode2D.Force);
    }
}
