using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    protected Vector2Int getNeighbourGridPos(Vector2Int pos, int index)
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
            return new Vector2Int(pos.x - (pos.y) % 2, pos.y + 1);
        }
        if (index == 4)
        {
            return new Vector2Int(pos.x - (pos.y) % 2 + 1, pos.y - 1);
        }
        if (index == 2)
        {
            return new Vector2Int(pos.x - (pos.y) % 2, pos.y - 1);
        }
        if (index == 5)
        {
            return new Vector2Int(pos.x - (pos.y) % 2 + 1, pos.y + 1);
        }
        return new Vector2Int(-1, -1);
    }

    [SerializeField] protected int[] cellCount;

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

    protected List<Cell> cells = new List<Cell>();

    [SerializeField] private Cell [] cellPrefabs;

   

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

    protected Cell Add(Vector2Int pos,int index = 0)
    {
        if(index<0||index>cellPrefabs.Length-1)
        {
            Debug.Log("In CellFactory, Add: Requested unexisting index");
            return null;
        }
        Cell o;
        o = Instantiate(cellPrefabs[index]);
        o.InstantiateCell(pos, index, this, true);
        o.transform.parent = transform;
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
       if(cellPrefabs!=null)
        {
            //Debug.Log("Instantiating...");
            cellCount = new int[cellPrefabs.Length];
        }
       if(cellPrefabs==null)
        {
            Debug.Log("CellFactory, Awake: No Prefabs to instantiate");
        }
    }
    public void removeFromList(Cell cell)
    {
        Debug.Log("In CellFactory, removeFromList: Cell removed from list");

        if(cell==null)
        {
            Debug.Log("In Cellfactory, reomveFromList: null argument passed");
            return;
        }
        Debug.Log("Destroying...");
        cellCount[cell.ID]--;
        for (int i = 0;i<cells.Count;i++)
        {
            if(cells[i]==cell)
            {
                cells[i] = cells[cells.Count - 1];
                cells.RemoveAt(cells.Count - 1);
            }
        }
    }
    public void addToList(Cell cell)
    {
        if(cell==null)
        {
            Debug.Log("In CellFactorym addToList: null argument passed");
            return;
        }
        if(cell.ID>-1&&cell.ID<cellCount.Length)
        {
            cellCount[cell.ID]++;
        }
        
    }
}