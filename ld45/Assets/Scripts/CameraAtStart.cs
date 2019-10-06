using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public MapGenerator MapGenerator;
    public CellFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.84252352941176470588235294117647f * (MapGenerator.xSize/2+0.5f), 0.72023225806451612903225806451613f * (MapGenerator.ySize/2+0.5f),-3);
        factory.Add(Cell.getHexCoords(new Vector2(0.84252352941176470588235294117647f * (MapGenerator.xSize / 2 + 0.5f), 0.72023225806451612903225806451613f * (MapGenerator.ySize / 2 + 0.5f)),1),1);
        ScoreCore.mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
