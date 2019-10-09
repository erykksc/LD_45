using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    
    private int[] cellCount;

    public int getCellCount(int index)
    {
        if(index == -1)
        {
            int ret = 0;
            for(int i = 0;i<cellCount.Length;i++)
            {
                ret += cellCount[i];
            }
            return ret;
        }
        if(index<0||index+1>cellCount.Length)
        {
            Debug.Log("In CellFactory, getCellCount : incorrect index passed");
            return -1;
        }
        return cellCount[index];
    }

    private List<Cell> cells = new List<Cell>();

    private Cell [] cellPrefabs;

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
    public Cell Find(int index)
    {
        if(index>-1&&index<cells.Count)
        {
            return cells[index];
        }
        return null;
    }

    public Cell Add(Vector2Int pos,int index = 0)
    {
        Cell o;
        o = Instantiate(cellPrefabs[index]);
        o.InstantiateCell(pos, index, this);
        return null;
    }

    public void generateGrid(Vector2Int size)
    {
        cells = new List<Cell>(new Cell[size.x * size.y]);

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
                Destroy(cell.gameObject);
                cells[i] = cells[cells.Count - 1];
                cells.RemoveAt(cells.Count - 1);
            }
        }
    }

    public void DestroyCell(int index)
    {
        if(cells[index]==null)
        {
            return;
        }
        if(index>=0&&index<cells.Count)
        {
            Destroy(cells[index].gameObject);
            cells[index] = cells[cells.Count - 1];
            cells.RemoveAt(cells.Count - 1);
        }
    }
    protected void Awake()
    {
       if(cellCount == null&&cellPrefabs!=null)
        {
            cellCount = new int[cellPrefabs.Length];
        }
    }
}
/*
 Vector2Int getNeighbourPos(Vector2Int pos,int index)
    {
        if (index == 0) 
        {
            return new Vector2Int(pos.x + 1, pos.y);
        }
        if (index == 3)
        {
            return new Vector2Int(pos.x - 1, pos.y);
        }
        if (index == 1)
        {
            return new Vector2Int(pos.x - (pos.y + 1) % 2, pos.y + 1);
        }
        if (index == 4)
        {
            return new Vector2Int(pos.x - (pos.y + 1) % 2 + 1, pos.y - 1);
        }
        if (index == 2)
        {
            return new Vector2Int(pos.x - (pos.y + 1) % 2, pos.y - 1);
        }
        if (index == 5)
        {
            return new Vector2Int(pos.x - (pos.y + 1) % 2 + 1, pos.y + 1);
        }
        return new Vector2Int(0, 0);
    }

    */