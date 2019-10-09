using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : Cell
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        renderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
