using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ScoreCore : MonoBehaviour
{
    public Text CashDisplayer;
    public static int Cash=0;
    [SerializeField] private float nextRoundCheckTime = 0.0f;
    [SerializeField] private float roundCheckingRate = 0.5f;
    [SerializeField] private int roundNum = 0;
    [SerializeField] private float timeBetweenRounds = 10.0f;
    [SerializeField] private float startOfTheNextRound = 0.0f;
    [SerializeField] private bool waitingForNextRound = true;
    private GameObject mainSpawner;
    private int nextNumOfEnemiesGroups = 1;
    private int nextNumOfEnemiesPerGroup = 1;
    [SerializeField] private float distanceOfSpawnersFromGen = 25.0f;


    void Awake()
    {
        if (CashDisplayer == null)
        {
            CashDisplayer = GameObject.Find("CashDisplayer").GetComponent<Text>();
        }
    }

    private void Start() {
        mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
    }

    void Update()
    {
        //Cash++;
        CashDisplayer.text = Cash.ToString();
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
            return false;
        }
    }

    private void startNextRound()
    {
        roundNum += 1;
        mainSpawner.GetComponent<SpawnerSpawner>().Spawn(distanceOfSpawnersFromGen, nextNumOfEnemiesPerGroup, nextNumOfEnemiesGroups, 0.1f);
        //Temporary
        nextNumOfEnemiesPerGroup += 1;
    }

    private void FixedUpdate() {
        if (Time.time > nextRoundCheckTime)
        {
            
            if (isRoundCompleted())
            {
                nextRoundCheckTime = Time.time + timeBetweenRounds;
                startOfTheNextRound = Time.time + timeBetweenRounds;
                waitingForNextRound = true;
            }
            else
            {
                nextRoundCheckTime = Time.time + roundCheckingRate;    
            }
        }
        if (waitingForNextRound && (Time.time > startOfTheNextRound))
        {
            startNextRound();
        }

    }
}
