using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Cell
{
    // Start is called before the first frame update

    //[SerializeField] static Sprite sprite;
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
        
        //sprite = gameObject.GetComponent<Sprite>();
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
