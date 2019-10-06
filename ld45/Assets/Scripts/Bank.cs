using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
   
    public override void onImpulse()
    {
        ScoreCore.Cash++;
        
    }
    private void Awake()
    {
        setPulseAction(action);
    }
}
