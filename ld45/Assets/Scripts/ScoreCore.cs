using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ScoreCore : MonoBehaviour
{
    [Space]
    [Header("Price Related")]
    public Text CashDisplayer;
        public Text BankPriceDisplayer;
        public Text TurretPriceDisplayer;
        public Text WallPriceDisplayer;
        public static int[] Prices = { 15, 999, 20, 5 };

    public static int Cash=0;


    [Space]
    [Header("Round Related")]
    [SerializeField] private float nextRoundCheckTime = 0.0f;
    [SerializeField] private float roundCheckingRate = 0.5f;
    [SerializeField] private int roundNum = 0;
    [SerializeField] private float timeBetweenRounds = 10.0f;
    [SerializeField] private float startOfTheNextRoundTime = 0.0f;
    [SerializeField] private bool waitingForNextRound = false;

    [Space]
    [Header("Enemy Related")]
    public static GameObject mainSpawner;
    private int nextNumOfEnemiesGroups = 1;
    private int nextNumOfEnemiesPerGroup = 1;
    [SerializeField] private float distanceOfSpawnersFromGen = 25.0f;


    void Awake()
    {
        Cash = 80;
        if (CashDisplayer == null)
        {
            CashDisplayer = GameObject.Find("CashDisplayer").GetComponent<Text>();

        }
        BankPriceDisplayer.text = (Prices[0].ToString() + "$");
        TurretPriceDisplayer.text = (Prices[2].ToString() + "$");
        WallPriceDisplayer.text = (Prices[3].ToString() + "$");
    }

    void Update()
    {
        //Cash++;
        CashDisplayer.text =( Cash.ToString()   +"$")  ;
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
        roundNum += 1;
        mainSpawner.GetComponent<SpawnerSpawner>().Spawn(distanceOfSpawnersFromGen, nextNumOfEnemiesPerGroup, nextNumOfEnemiesGroups, 0.1f);
        //Temporary
        nextNumOfEnemiesGroups += 1;
        waitingForNextRound = false;
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
