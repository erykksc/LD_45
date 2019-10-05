using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
   
    // Update is called once per frame
    public override void WhenActivatedDoOnce()
    {
        ScoreCore.Cash += 1;
    }
}
