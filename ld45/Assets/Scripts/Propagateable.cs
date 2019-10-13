using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : Cell
{

    public float conveyTime = 0.1f;
    public bool activated = false;
    protected int timesActivated = 0;
    public bool propagates = true;


    public delegate void pulseAction();

    private pulseAction Action = null;

    public IEnumerator animatePulse()
    {
        for (int i = 0; i < animationPerUpgrade; i++)
        {
            renderer.sprite = sprites[(animationOffset) * animationPerUpgrade + i];
            yield return new WaitForSeconds(animationDuration / (animationPerUpgrade - 1));
        }
        renderer.sprite = sprites[animationPerUpgrade * (animationOffset)];
    }

    public void setPulseAction(pulseAction pAction)
    {
        Action = pAction;
    }

    public IEnumerator receiveImpuls(Propagateable origin)
    {
        if(timesActivated<origin.timesActivated)
        {
            timesActivated = origin.timesActivated;
            activated = true;
            if (Action != null)
            {
                Action();
            }
            yield return new WaitForSeconds(conveyTime);
            activated = false;
            propagateImpuls();
        }
    }
    public void propagateImpuls()
    {
        if(!propagates)
        {
            return;
        }
        for(int i = 0;i<6;i++)
        {
            if(getNeighbour(i)!=null)
            {
                StartCoroutine(((Propagateable)getNeighbour(i)).receiveImpuls(this));
            }
        }
    }
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
