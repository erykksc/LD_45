using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : MonoBehaviour
{
    // Start is called before the first frame update
    public float impulsTime = 0.5f;
    bool activated = false;
    private int timesActivated = 0;
    public bool propagates = true;

    public delegate void pulseAction();

    private pulseAction Action;

    Propagateable [ ]neighbours = new Propagateable[6];

    public void setPulseAction(pulseAction pAction)
    {
        Action = pAction;
    }

    public IEnumerator receiveImpuls(Propagateable origin)
    {
        Action();
        timesActivated = origin.timesActivated;
        activated = true;
        yield return new WaitForSeconds(impulsTime);
        activated = false;
        propagateImpuls();
    }
    public void propagateImpuls()
    {
        for(int i = 0;i<6;i++)
        {
            if(neighbours[i]!=null)
            {
                neighbours[i].receiveImpuls(this);
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
