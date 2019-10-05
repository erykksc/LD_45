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
        while(active)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[i];
                yield return new WaitForSeconds(pulsationRate / (sprites.Length - 1));
            }
            GetComponent<SpriteRenderer>().sprite = sprites[0];


            timesActivated++;
            if (right != null)
            {
                right.getImpulse(this);
            }
            if (left != null)
            {
                left.getImpulse(this);
            }
            if (rup != null)
            {
                rup.getImpulse(this);
            }
            if (rdown != null)
            {
                rdown.getImpulse(this);
            }
            if (lup != null)
            {
                lup.getImpulse(this);
            }
            if (ldown != null)
            {
                ldown.getImpulse(this);
            }
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
