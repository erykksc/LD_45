using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagateable : MonoBehaviour
{
    // Start is called before the first frame update
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
            Debug.Log("receiving, deceiving");
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
                Debug.Log("propagation in progress");
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
