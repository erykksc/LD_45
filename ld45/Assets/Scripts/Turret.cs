using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Cell
{
    // Start is called before the first frame update
    [SerializeField] private float range;
    //public int damage;
    //public int damageSpeed;
    private int passed;
    public float timeGap = 0.5f;
    bool switch1 = true;
    private LineRenderer line;
    private int additionalRayCount = 0;


    //Finding a target by finding the nearest object with the tag ENEMY
    private GameObject GetTarget(Vector2 startingPos)
    {
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        
        GameObject lastObject = gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject x in objects)
        {
            Vector3 diff = x.GetComponent<Transform>().position - startingPos;
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
        //Debug.Log("wrk");
        StartCoroutine(initiateShooting());
    }



    private void Shoot()
    {
        GameObject Target = GetTarget(gameObject.GetComponent<Transform>().position);
        //Debug.Log("Searching");

        Vector2 dist =  Target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        // Ważne
        //if (Target!= gameObject ) Rotate(dist);


        if (dist.sqrMagnitude < range && Target != gameObject)
        {
            //DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, dist);
            List<Vector3> points;
            points.Add((Vector2) gameObject.GetComponent<Transform>().position + dist.normalized * 0.4f);
            points.Add((Vector2) Target.GetComponent<Transform>().position);
            //Debug.Log("One frame, one kill");
            Vector2 pos = Target.GetComponent<Transform>().position;
            Destroy(Target);

            for(int i = 0; i < additionalRayCount; i++)
            {
                Target = GetTarget(pos);
                if (Target == gameObject)
                {
                    break;
                }
                points.Add((Vector2) Target.GetComponent<Transform>().position);
                pos = Target.GetComponent<Transform>().position;
                Destroy(Target);
            }
            line.positionCount = points.Count;
            line.SetPositions(points);
            StartCoroutine(deleteLine());
        }
    }
    private IEnumerator initiateShooting()
    {
        // warunek - odległóść
        if(GetTarget()!=gameObject)
        {
            
        }
        
        StartCoroutine(animate());
        switch1 = false;
        yield return new WaitForSeconds(timeGap);
        Shoot();
    }
    private IEnumerator animate()
    {
        Transform ch;
        ch = GetComponentsInChildren<Transform>()[1];

        Vector3 origin = ch.right;
        Vector3 target = (GetTarget().GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position);
        target.z = origin.z = 0;
        origin = Vector3.Normalize(origin);
        target = Vector3.Normalize(target);
        float time = 0;
        
        while (time<timeGap)
        {
            foreach (Transform trans in GetComponentsInChildren<Transform>())
            {
                if (trans.name != "TurretBase")
                {
                    trans.right = -target*(time/timeGap)+origin*(1-time/timeGap);

                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    private void Awake()
    {
        setPulseAction(action);
        line = gameObject.GetComponent<LineRenderer>();
    }

    private IEnumerator deleteLine()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(0,0,-1000);
        points[1] = new Vector3(0,0,-1000);
        line.positionCount = 2;
        line.SetPositions(points);
    }

    private void Rotate(Vector2 Vect2)
    {
        
        //Determining the rotation and rotating
        //float RotAngle = Vector2.Angle(Vector2.up,Vect2);
        foreach (Transform trans in GetComponentsInChildren<Transform>())
        {
            if (trans.name != "TurretBase")
            {
                trans.right =-GetTarget().GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
                //trans.RotateAround(Vector3.forward, RotAngle);
                //trans.rotation = Quaternion.Euler(0, 0, RotAngle-90);


            }
        }
        



    }


}
