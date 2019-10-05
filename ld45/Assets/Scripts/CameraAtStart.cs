using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtStart : MonoBehaviour
{
    public MapGenerator Mapgen;
    //public MapGenerator ySize;
    public CellFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.LogWarning((xSize.xSize * 0.5f + 0.5f).ToString()+"/ "+ (ySize.ySize * 0.5f + 0.5f).ToString());
        transform.position = new Vector3(Mapgen.xSize * 0.5f + 0.5f, Mapgen.ySize * 0.5f + 0.5f, -3);
        factory.Add(Cell.getHexCoords(new Vector2(Mapgen.xSize*0.5f+0.5f, Mapgen.ySize*0.5f+1.5f),1),1);
        ScoreCore.mainSpawner = GameObject.FindGameObjectWithTag("MainSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(new Vector3((Mapgen.xSize * 0.5f + 0.5f),( Mapgen.ySize * 0.5f + 0.5f), -3),        4); 
    }

}
