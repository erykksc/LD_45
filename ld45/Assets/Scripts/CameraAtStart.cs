using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public MapGenerator MapGenerator;
    public CellFactory factory;
    void Start()
    {
        MapGenerator = gameObject.GetComponent<MapGenerator>();
       // transform.position = new Vector3(0.84252352941176470588235294117647f * (MapGenerator.xSize / 2), 0.72023225806451612903225806451613f * (MapGenerator.ySize / 2), -3);
        factory.Add(new Vector2Int((int)((float)MapGenerator.xSize/2),(int)((float)MapGenerator.ySize/2)) ,1);
        ScoreCore.mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
        Vector2 pos = Cell.getGlobalCoords(new Vector2Int((int)((float)MapGenerator.xSize / 2), (int)((float)MapGenerator.ySize / 2)), 55f / 64f);
        transform.position = new Vector3(pos.x, pos.y, -3);
    }

    void Update()
    {

    }
}