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
    List<Cell> cells = new List<Cell>();
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
    public Cell Add(Vector2Int pos,int index = 0)
    {
        Cell o;
        if(index>=cellPrefabs.Length)
        {
            return null;
        }
        o = Instantiate(cellPrefabs[index]);
        o.Instantiate(pos);
        o.name = cellPrefabs[index].name;
        for (int i = 0; i < cells.Count; i++)
        {

            if (o.pos.x - cells[i].pos.x == -1 + (o.pos.y % 2) && o.pos.y - cells[i].pos.y == 1)
            {
                o.neighbours[0] = cells[i];
                cells[i].neighbours[3] = o;
            }
            if (cells[i].pos.x - o.pos.x == -1 + (cells[i].pos.y % 2) && cells[i].pos.y - o.pos.y == 1)
            {
                //swap
                cells[i].neighbours[0] = o;
                o.neighbours[3] = cells[i];
            }
            ///
            if (cells[i].pos.x - o.pos.x == 0 + (cells[i].pos.y % 2) &&  cells[i].pos.y - o.pos.y == 1) 
            {
                //swap
                cells[i].neighbours[1] = o;
                o.neighbours[4] = cells[i];
            }
            if (o.pos.x - cells[i].pos.x == 0 + (o.pos.y % 2) && o.pos.y - cells[i].pos.y == 1)
            {
                o.neighbours[1] = cells[i];
                cells[i].neighbours[4] = o;
            }
            ///
            if (o.pos.x - cells[i].pos.x == 1 && o.pos.y - cells[i].pos.y == 0)
            {
                o.neighbours[2] = cells[i];
                cells[i].neighbours[5] = o;
            }
            if (cells[i].pos.x - o.pos.x  == 1 && cells[i].pos.y - o.pos.y == 0)
            {
                cells[i].neighbours[2] = o;
                o.neighbours[5] = cells[i];
            }

        }
        cells.Add(o);
        //Debug.Log(cells.Count);
        return o;
    }
    public void DestroyCell(Vector2Int pos)
    {
        for(int i = 0;i<cells.Count;i++)
        {
            if(pos==cells[i].pos)
            {
                Destroy(cells[i].gameObject);
                cells[i] = cells[cells.Count - 1];
                cells.RemoveAt(cells.Count - 1);
            }
        }
    }
    public void DestroyCell(Cell cell)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cell == cells[i])
            {
                Destroy(cells[i].gameObject);
                cells[i] = cells[cells.Count - 1];
                cells.RemoveAt(cells.Count - 1);
            }
        }
    }
    public void DestroyCell(int index)
    {
        if(index>=0&&index<cells.Count)
        {
            Destroy(cells[index].gameObject);
            cells[index] = cells[cells.Count - 1];
            cells.RemoveAt(cells.Count - 1);
        }
    }
    void Awake()
    {
       
    }
}
   