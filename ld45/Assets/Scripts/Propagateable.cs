using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : Cell
{
    [System.Serializable]
    public class Properties

    {
        public int hp;
        public int moneyps;
    }


    [SerializeField] private Properties properties;

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
            GetComponent<SpriteRenderer>().sprite = sprites[(animationOffset) * animationPerUpgrade + i];
            yield return new WaitForSeconds(animationDuration / (animationPerUpgrade - 1));
        }
        GetComponent<SpriteRenderer>().sprite = sprites[animationPerUpgrade * (animationOffset)];
    }

    public void setPulseAction(pulseAction pAction)
    {
        Action = pAction;
    }

    public IEnumerator receiveImpuls(Propagateable origin)
    {
        if(timesActivated<origin.timesActivated)
        {
            if (Action != null)
            {
                Action();
            }
            timesActivated = origin.timesActivated;
            activated = true;
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
