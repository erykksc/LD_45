using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silos : Cell
{
    // Start is called before the first frame update
    static public List<Silos> allInst;

    void Awake()
    {
        level=0;
        if(allInst == null)
        {
            allInst = new List<Silos>();
        }
        allInst.Add(this);
        Debug.Log($"Silos count: {allInst.Count}");
        Upgrade();
        setPulseAction(action);
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
    private void OnDestroy()
    {
        allInst.Remove(this);
        if(ID>=0)
        {
            ScoreCore.cellCount[ID]--;
        }
        
        ScoreCore.Prices[0] = 4 * ScoreCore.cellCount[0];
        ScoreCore.Prices[2] = 4 * ScoreCore.cellCount[2];
        ScoreCore.Prices[3] = 4 * ScoreCore.cellCount[3];
        ScoreCore.Prices[4] = 4 * ScoreCore.cellCount[4];
        Camera.main.GetComponent<ScoreCore>().PriceDisplayers[0].text = ScoreCore.Prices[0].ToString() + "$";
        Camera.main.GetComponent<ScoreCore>().PriceDisplayers[2].text = ScoreCore.Prices[2].ToString() + "$";
        Camera.main.GetComponent<ScoreCore>().PriceDisplayers[3].text = ScoreCore.Prices[3].ToString() + "$";
        Camera.main.GetComponent<ScoreCore>().PriceDisplayers[4].text = ScoreCore.Prices[4].ToString() + "$";

        CellFactory CF = null;
        Transform SemiTarget = null;
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "Canvas") SemiTarget = trans;
        }
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "GrassFactory") SemiTarget = trans;
        }
        CF = SemiTarget.GetComponent<CellFactory>();
        CellFactory.cellCount--;
    }
}
