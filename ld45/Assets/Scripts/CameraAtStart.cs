using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public CellFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(26, 13,-3);
        factory.Add(Cell.getHexCoords(new Vector2(26, 13),1),1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
