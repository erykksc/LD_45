using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ScoreCore : MonoBehaviour
{
    public Text CashDisplayer;
    public static int Cash=0;


    void Awake()
    {
        if (CashDisplayer == null) CashDisplayer = GameObject.Find("CashDisplayer").GetComponent<Text>();
    }

    void Update()
    {
        //Cash++;
        CashDisplayer.text = Cash.ToString();
    }
}
