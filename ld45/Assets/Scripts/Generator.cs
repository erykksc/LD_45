using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Cell
{
    // Start is called before the first frame update
    IEnumerator pulsate()
    {
        while(active)
        {
            yield return new WaitForSeconds(2);
            timesActivated++;
            if (right != null)
            {
                right.getImpulse(this);
            }
            if (left != null)
            {
                left.getImpulse(this);
            }
            if (up != null)
            {
                up.getImpulse(this);
            }
            if (down != null)
            {
                down.getImpulse(this);
            }
        }
    }
    void Start()
    {
        StartCoroutine(pulsate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
