using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
    [SerializeField] private int moneyToAdd = 1;
    
    public override void onImpulse()
    {
        ScoreCore.Cash = ScoreCore.Cash + moneyToAdd;
        
    }
    private void Awake()
    {
        setPulseAction(action);
    }
}
