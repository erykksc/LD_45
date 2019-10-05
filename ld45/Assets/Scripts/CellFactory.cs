using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    static int getSign(int v)
    {
        if(v<0)
        {
            return -1;
        }
        return 1;
    }
    // Start is called before the first frame update
    List<Cell> cells;
    public Cell [] cellPrefabs;

    public Cell Find(Vector2Int pos)
    {
        for(int i = 0;i<cells.Count;i++)
        {
            if(pos == cells[i].pos)
            {
                return cells[i];
            }
        }
        return null;
    }
    public void Add(Vector2Int pos,int index = 0)
    {
        Cell o;
        if(index>=cellPrefabs.Length)
        {
            return;
        }
        o = Instantiate(cellPrefabs[index]);
        o.Instantiate(pos);
        o.name = cellPrefabs[index].name;
        for (int i = 0; i < cells.Count; i++)
        {

            if (o.pos.x - cells[i].pos.x == -1 + (o.pos.y % 2) && o.pos.y - cells[i].pos.y == 1)
            {
                o.rdown = cells[i];
                cells[i].lup = o;
            }
            if (cells[i].pos.x - o.pos.x == -1 + (cells[i].pos.y % 2) && cells[i].pos.y - o.pos.y == 1)
            {
                //swap
                cells[i].rdown = o;
                o.lup = cells[i];
            }
            ///
            if (cells[i].pos.x - o.pos.x == 0 + (cells[i].pos.y % 2) &&  cells[i].pos.y - o.pos.y == 1) 
            {
                //swap
                cells[i].ldown = o;
                o.rup = cells[i];
            }
            if (o.pos.x - cells[i].pos.x == 0 + (o.pos.y % 2) && o.pos.y - cells[i].pos.y == 1)
            {
                o.ldown = cells[i];
                cells[i].rup = o;
            }
            ///
            if (o.pos.x - cells[i].pos.x == 1 && o.pos.y - cells[i].pos.y == 0)
            {
                o.left = cells[i];
                cells[i].right = o;
            }
            if (cells[i].pos.x - o.pos.x  == 1 && cells[i].pos.y - o.pos.y == 0)
            {
                cells[i].left = o;
                o.right = cells[i];
            }

        }
        cells.Add(o);
        //Debug.Log(cells.Count);
    }
    void Awake()
    {

        cells = new List<Cell>();
    }
}
   