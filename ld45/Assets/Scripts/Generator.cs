using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void Update()
    {
        
    }
}
