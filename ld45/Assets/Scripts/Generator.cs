using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Building
{
    // Start is called before the first frame update
    float timeSinceLastPulse;
    

    void Start()
    {
        
    }
    private void Awake()
    {
        base.Awake();
        timeSinceLastPulse = 1f/current.pulseFrequency;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeSinceLastPulse<=0)
        {
            timesActivated++;
            propagateImpuls();
            onPulse();
            timeSinceLastPulse = 1f/ current.pulseFrequency;
        }
        timeSinceLastPulse -= Time.fixedDeltaTime;
    }
}
