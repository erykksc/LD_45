using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silos : Cell
{
    // Start is called before the first frame update
    static List<Silos> all;

    void Start()
    {
        
    }
    private void Awake()
    {
        if(all == null)
        {
            all = new List<Silos>();
        }
        all.Add(this);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
