using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : Cell
{
    public Sprite[] heads;

    public SpriteRenderer renderer;

    public float timeGap = 0.5f;
    //bool switch1 = true;
    private LineRenderer line;
    private List<GameObject> Deleted = new List<GameObject>();
    //[SerializeField] private int additionalRayCount = 0;

    //Finding a target by finding the nearest object with the tag ENEMY
    private GameObject GetTarget(Vector3 startingPos)
    {
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy").ToArray();
        objects = objects.ToArray().Except(Deleted.ToArray()).ToArray();        
        GameObject lastObject = gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject x in objects)
        {
            Vector3 diff =  x.GetComponent<Transform>().position - startingPos;
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
        Deleted.Clear();
        StartCoroutine(initiateShooting());
    }

    private void dealDamage2Enemy(GameObject target)
    {
        target.GetComponent<Enemy>().dealDamage(damage[0]);
        Deleted.Add(target);
    }

    private void Shoot()
    {
        GameObject Target = GetTarget(gameObject.GetComponent<Transform>().position);
        Vector2 dist =  Target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        // Ważne

        if (dist.sqrMagnitude < range[0] && Target != gameObject)
        {
            List<Vector3> points = new List<Vector3>();
            points.Add((Vector2) gameObject.GetComponent<Transform>().position + dist.normalized * 0.4f);
            points.Add((Vector2) Target.GetComponent<Transform>().position);
            Vector2 pos = Target.GetComponent<Transform>().position;
            dealDamage2Enemy(Target);

            for(int i = 0; i < rays[0]; i++)
            {
                Target = GetTarget(pos);
                dist =  Target.GetComponent<Transform>().position - points[points.Count -1];
                if (Target == gameObject || dist.sqrMagnitude > (range[0]/4))
                {
                    break;
                }
                points.Add((Vector2) Target.GetComponent<Transform>().position);
                pos = Target.GetComponent<Transform>().position;
                dealDamage2Enemy(Target);
            }
            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
            StartCoroutine(deleteLine());
        }
    }
    private IEnumerator initiateShooting()
    {
        // warunek - odległóść

        if(GetTarget(gameObject.GetComponent<Transform>().position)!=gameObject)
        {
            
        }
        
        StartCoroutine(animate());
        yield return new WaitForSeconds(timeGap);
        Shoot();
    } 
    private IEnumerator animate()
    {
        Transform ch;
        ch = GetComponentsInChildren<Transform>()[1];

        Vector3 origin = ch.right;
        Vector3 target = (GetTarget(gameObject.GetComponent<Transform>().position).GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position);
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

        level = 0;

        animationLength = 6;

        renderer = GetComponentsInChildren<SpriteRenderer>()[1];
        Upgrade();
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
        float RotAngle = Vector2.Angle(Vector2.up,Vect2);
        foreach (Transform trans in GetComponentsInChildren<Transform>())
        {
            if (trans.name != "TurretBase")
            {
                trans.right =-GetTarget(gameObject.GetComponent<Transform>().position).GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
            }
        }
    }

    public override void Upgrade()
    {
        if (level + 1 < health.Length)
        {
            level++;
            health[0] = health[level];
            damage[0] = damage[level];
            selfHeal[0] = selfHeal[level];
            rays[0] = rays[level];
            moneyps[0] = moneyps[level];
            range[0] = range[level];
            cash[0] = cash[level];
            renderer.sprite = heads[level-1];
        }
    }

}
