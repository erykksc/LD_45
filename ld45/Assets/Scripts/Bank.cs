using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
    
    public override void onImpulse()
    {
        ScoreCore.Cash = ScoreCore.Cash + moneyps[0];
        
    }
    private void Awake()
    {
        setPulseAction(action);

        level = 0;

        Upgrade();

    }
}
