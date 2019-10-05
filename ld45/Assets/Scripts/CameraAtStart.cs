using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public CellFactory factory;
    // Start is called before the first frame update
    private int[] a;
    
    void Start()
    {
        int []a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Random ran = new Random();
        int mynum = a[Random.Range(1, 11)];
        //[ran.Next(0, a.Length];
        if (mynum < 9)
        {
            int b = 1;
            if (mynum == 9)
            {
                b = 2;

            }
            else
            {
                b = 3;
            }
        }
        transform.position = new Vector3(26, 13,-3);
        factory.Add(Cell.getHexCoords(new Vector2(26, 13),1), mynum);
        ScoreCore.mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
