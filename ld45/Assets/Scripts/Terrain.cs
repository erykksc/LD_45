using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : Cell
{

    // Start is called before the first frame update

    public int buildable = 0;

    public int distToGen;
   
    void Start()
    {
        
    }
    private void Awake()
    {
        base.Awake();
        distToGen = int.MaxValue;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 60 * Random.Range(0, 7)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        base.OnDestroy();
    }
}
