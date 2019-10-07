using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silos : Cell
{
    // Start is called before the first frame update
    static public List<Silos> allInst;

    void Start()
    {
        if(allInst == null)
        {
            allInst = new List<Silos>();
        }
        allInst.Add(this);
    }
    static public int getAvailableBuildings()
    {
        int res = 0;
        for(int i = 0;i< allInst.Count;i++)
        {
            res += allInst[i].cash[0];
        }
        return res;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
