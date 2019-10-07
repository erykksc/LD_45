using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : MonoBehaviour
{
    public int terrainType;
    public float impulsTime = 0.5f;
    public float conveyTime = 0.1f;
    public bool activated = false;
    protected int timesActivated = 0;
    public bool propagates = true;
    public Vector2Int pos;

    public delegate void pulseAction();

    private pulseAction Action = null;

    public Propagateable [ ]neighbours = { null, null, null, null, null, null, };

    public void refresh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                neighbours[i].neighbours[(i + 3) % 6] = this;
            }
        }
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
            yield return new WaitForSeconds(impulsTime-conveyTime);
        }
    }
    public void propagateImpuls()
    {
        for(int i = 0;i<6;i++)
        {
            if(neighbours[i]!=null)
            {
                StartCoroutine(neighbours[i].receiveImpuls(this));
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
