using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    //public CellFactory factory;

    // Start is called before the first frame update
    void Start()
    {
        //factory.Add(new Vector2Int(0, 0), 1);
    }
    Vector2 getGlobalCoords(Vector2Int pos,float size)
    {
        return new Vector2(-(pos.y % 2) * 0.5f * size + pos.x, pos.y * Mathf.Sqrt(3) * 0.5f * size);
    }
    Vector2Int getHexCoords(Vector2 pos,float size)
    {
        float xstep, ystep;
        xstep = size * Mathf.Sqrt(3);
        ystep = size * 3 * 0.5f;
        int x, y;
        x = (int)Mathf.Round(pos.x / xstep);
        y = (int)Mathf.Round(pos.y / ystep);
        Debug.Log("data:");
        float min = size + 99;
        Vector2Int mV = new Vector2Int(0,0);
        Vector2 tPos;
        for(int i = x-3;i<x+3;i++)
        {
            for(int j = y-3;j<y+3;y++)
            {
                tPos = getGlobalCoords(new Vector2Int(i, j), size);
                if(Vector2.Distance(tPos,pos)<min)
                {
                    mV = new Vector2Int(i, j);
                }
            }
        }

        return mV;
    }
    public static Vector2Int snap(Vector2 pos,float size)
    {
        float xstep, ystep;
        xstep = size * Mathf.Sqrt(3);
        ystep = size * 3*0.5f;
        int x, y;
        x = (int)Mathf.Round(pos.x / xstep);
        y = (int)Mathf.Round(pos.y / ystep);
        Debug.Log("data:");
        Debug.Log(y);
       // y--;
        //for(int i = y;i<y+3;i++)
       // {

       // }

        return new Vector2Int(0, 0);
    }
    private void Awake()
    {
        Debug.Log("Official: ");
        Vector2 position = (getGlobalCoords(new Vector2Int(10, 10),10));
        Debug.Log(getHexCoords(position, 10));
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("button pressed");
            Vector2 mouse = Input.mousePosition;
            Debug.Log(mouse);
        }
    }
}
