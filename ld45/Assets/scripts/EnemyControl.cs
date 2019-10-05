using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public GameObject Target;
    public float maxSpeed;
    public float speed;
    private bool isRunningAway = false;
    private float startedToRunAway = 0.0f;
    [SerializeField] private float runAwayFor = 0.2f; 

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
                lastObject = x;
                distance = dist;
            }
        }
        
        return lastObject;
    }
    void Start()
    {
        //Target = GetTarget();   
    }



    void FixedUpdate()
    {
        if (Target == null)
        {
            Target = GetTarget();
        }
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

        if (isRunningAway)
        {
            move = -move;
            if (Time.time>startedToRunAway+runAwayFor)
            {
                isRunningAway = false;
            }
        }

        //normalize
        gameObject.GetComponent<Rigidbody2D>().AddForce(move,ForceMode2D.Force);
    }
    public void run()
    {
        isRunningAway = true;
        startedToRunAway = Time.time;
    }
}
