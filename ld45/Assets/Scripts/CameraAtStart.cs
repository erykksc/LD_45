using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public CellFactory factory;
    void Start()
    {
        transform.position = new Vector3(22, 11,-3);
        factory.Add(Cell.getHexCoords(new Vector2(26, 13),1),1);
        ScoreCore.mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
    }

    void Update()
    {
        
    }
}
