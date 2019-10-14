using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Building
{
    // Start is called before the first frame update
    public override void Upgrade()
    {
        base.Upgrade();
        animationOffset = current.level - 1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
