using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
    [SerializeField] private int moneyToAdd = 1;
    
    // Update is called once per frame
    public override void onImpulse()
    {
        ScoreCore.Cash = ScoreCore.Cash + moneyToAdd;
        
    }
    private void Awake()
    {
        setPulseAction(action);

        level = 0;

        health = new int[2];
        range = new float[2];
        rays = new int[2];
        moneyps = new int[2];
        cash = new int[2];
        selfHeal = new int[2];
        damage = new int[2];

        health[1] = 100;
        range[1] = 15;
        rays[1] = 0;
        moneyps[1] = 2;
        cash[1] = 100;
        damage[1] = 50;
        selfHeal[1] = 10;

        Upgrade();

    }
}
