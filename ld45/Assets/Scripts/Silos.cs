using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silos : Cell
{
    // Start is called before the first frame update
    static public List<Silos> allInst;

    void Awake()
    {
        if(allInst == null)
        {
            allInst = new List<Silos>();
        }
        allInst.Add(this);
        Debug.Log($"Silos count: {allInst.Count}");
    }
    static public int getAvailableBuildings()
    {
        if (allInst == null)
        {
            allInst = new List<Silos>();
        }
        int res = 12;
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
