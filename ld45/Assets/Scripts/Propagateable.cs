using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : MonoBehaviour
{
    // Start is called before the first frame update
    public float impulsTime = 0.5f;
    public bool activated = false;
    protected int timesActivated = 0;
    public bool propagates = true;

    public delegate void pulseAction();

    private pulseAction Action = null;

    public Propagateable [ ]neighbours = new Propagateable[6];

    public void setPulseAction(pulseAction pAction)
    {
        Action = pAction;
    }

    public IEnumerator receiveImpuls(Propagateable origin)
    {
        if(timesActivated<origin.timesActivated)
        {
            //Debug.Log("receiving, deceiving");
            if (Action != null)
            {
                Action();
            }
            timesActivated = origin.timesActivated;
            activated = true;
            yield return new WaitForSeconds(impulsTime);
            activated = false;
            propagateImpuls();
        }
    }
    public void propagateImpuls()
    {
        for(int i = 0;i<6;i++)
        {
            if(neighbours[i]!=null)
            {
                //Debug.Log("propagation in progress");
                StartCoroutine(neighbours[i].receiveImpuls(this));
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
