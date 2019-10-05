using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Cell
{
    //[SerializeField] static Sprite sprite;
    // Start is called before the first frame update

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        //sprite = gameObject.GetComponent<Sprite>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
