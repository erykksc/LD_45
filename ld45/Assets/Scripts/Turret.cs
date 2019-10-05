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

    private GameObject GetTarget()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        // Debug.Log($"Enemies found: {objects.Length}");
        
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
        // Debug.Log($"Closest target: {lastObject}");
        return lastObject;
    }

    // Update is called once per frame
    public override void WhenActivatedDoOnce()
    {
        Shoot();
    }
    private void Shoot()
    {
        GameObject Target = GetTarget();
        // Debug.Log("Searching");
        Vector2 dist =  gameObject.GetComponent<Transform>().position - Target.GetComponent<Transform>().position;
        // Debug.Log($"Distance to target: {dist}");
        if (dist.sqrMagnitude < range && Target != gameObject)
        {
            // Debug.Log("One frame, one kill");
            Destroy(Target);
            DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, dist);
        }
    }
}
