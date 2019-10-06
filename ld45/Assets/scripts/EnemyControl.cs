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
    [SerializeField] private float bounceForce = 1.0f;
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private float runAwayFor = 0.2f; 

    [SerializeField] private float AnimationSpeed = 1;

    private Vector2 moveDirection;
    private Vector2 NonReversedMove;

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
                StartCoroutine(   animate()   ); 
                 
                  
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
        moveDirection = playerPos - position;

        moveDirection = moveDirection.normalized;
        //Extract movevector here

        NonReversedMove = moveDirection;
        if (isRunningAway)
        {
            moveDirection = -moveDirection;
            if (Time.time>startedToRunAway+runAwayFor)
            {
                isRunningAway = false;
            }
        }

        //normalize
        gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection*speed,ForceMode2D.Force);
    }


public IEnumerator animate()
    {
                int i=0;
        while (true)
      {
        float speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        Vector2 Direction =  NonReversedMove;

        //Handle rotation
        gameObject.transform.up = Direction;

        //handle animation speed
;
        if(i<Sprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = Sprites[i];
            i++;

        }
        else
        {
            i=0;
        }
            //Debug.LogWarning(speed+" :"+i);
            yield return new WaitForSeconds( 1/(   speed+AnimationSpeed)     );
      }

    }

    public void run()
    {
        isRunningAway = true;
        startedToRunAway = Time.time;
    }
    public void bounce()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(-moveDirection*bounceForce, ForceMode2D.Force);
    }
}
