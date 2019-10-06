using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ScoreCore : MonoBehaviour
{
    [Header("Cash related")]
    public static int Cash=0;
    public static int[] Prices = { 20, 999, 15, 5 };
        public Text CashDisplayer;
        public Text TimeDisplayer;


        public Text[] PriceDisplayers=new Text[4];



    [Header("Round related")]
    [SerializeField] private float nextRoundCheckTime = 0.0f;
    [SerializeField] private float roundCheckingRate = 0.5f;
    [SerializeField] private int roundNum = 1;
    [SerializeField] private float timeBetweenRounds = 10.0f;
    [SerializeField] private float startOfTheNextRoundTime = 0.0f;
    [SerializeField] private bool waitingForNextRound = false;
    public static GameObject mainSpawner;
    private int nextNumOfEnemiesGroups = 1;
    private int nextNumOfEnemiesPerGroup = 0;
    [SerializeField] private float distanceOfSpawnersFromGen = 25.0f;


    //Starting cash is set and text objects are assigned
    void Awake()
    {
        Cash = 80;
        if (CashDisplayer == null)
        {
            CashDisplayer = GameObject.Find("CashDisplayer").GetComponent<Text>();
        }
        PriceDisplayers[0].text=(Prices[0]+"$");
        PriceDisplayers[2].text=(Prices[2]+"$");
        PriceDisplayers[3].text=(Prices[3]+"$");
    }

    //Current Cash level is updated
    void Update()
    {
        CashDisplayer.text = Cash.ToString();
        TimeDisplayer.text = Mathf.FloorToInt(Time.time).ToString();
    }

    private bool isRoundCompleted()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void startNextRound()
    {
        changeEnemiesInNextRound(roundNum);
        roundNum += 1;
        mainSpawner.GetComponent<SpawnerSpawner>().Spawn(distanceOfSpawnersFromGen, nextNumOfEnemiesPerGroup, nextNumOfEnemiesGroups, 0.1f);
        //Temporary
        waitingForNextRound = false;
    }

    private void changeEnemiesInNextRound(int round)
    {
        round -= 1;
        if (round%2==0)
        {
            
            nextNumOfEnemiesPerGroup += 1;
        }
        else
        {
            nextNumOfEnemiesGroups += 1;
        }
        Debug.Log($"Wave enemies {round} ,{nextNumOfEnemiesGroups}, {nextNumOfEnemiesPerGroup}");
    }

    private void FixedUpdate() {
        if (Time.time > nextRoundCheckTime)
        {
            if (isRoundCompleted() && !waitingForNextRound)
            {
                nextRoundCheckTime = Time.time + timeBetweenRounds;
                startOfTheNextRoundTime = Time.time + timeBetweenRounds;
                waitingForNextRound = true;
            }
            else
            {
                nextRoundCheckTime = Time.time + roundCheckingRate;    
            }
        }
        if (waitingForNextRound && (Time.time > startOfTheNextRoundTime))
        {
            startNextRound();
        }

    }
}
