using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : Cell
{
    [SerializeField] private float pulsationRate = 2.0f;

    IEnumerator pulsate()
    {
       while(true)
        {
            timesActivated++;
            propagateImpuls();
            StartCoroutine(animate());
            yield return new WaitForSeconds(2f);
        }
    }
    void Start()
    {
        StartCoroutine(pulsate());
    }
    private void Awake()
    {
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
        //sprite = gameObject.GetComponent<Sprite>();
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
