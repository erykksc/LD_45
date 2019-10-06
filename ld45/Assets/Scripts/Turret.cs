using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Cell
{
    // Start is called before the first frame update
    public float range;
    public int damage;
    public int damageSpeed;
    private int passed;


    //Finding a target by finding the nearest object with the tag ENEMY
    private GameObject GetTarget()
    {
       
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        
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

    public override void onImpulse()
    {
        Debug.Log("wrk");
        Shoot();
    }



    private void Shoot()
    {
        GameObject Target = GetTarget();
        Debug.Log("Searching");

        Vector2 dist =  Target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;

        if (Target!= gameObject ) Rotate(dist);


        if (dist.sqrMagnitude < range && Target != gameObject)
        {
            Debug.Log("One frame, one kill");
            Destroy(Target);
            DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, dist);
        }
    }
    private void Awake()
    {
        setPulseAction(action);
    }


    private void Rotate(Vector2 Vect2)
    {
        
        //Determining the rotation and rotating
        float RotAngle = Vector2.Angle(Vector2.up,Vect2);
        foreach (Transform trans in GetComponentsInChildren<Transform>())
        {
            if (trans.name != "TurretBase")
            {
                //trans.RotateAround(Vector3.forward, RotAngle);
                trans.rotation = Quaternion.Euler(0, 0, RotAngle-90);
            }
        }
       



    }


}
