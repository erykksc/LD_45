using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    // Start is called before the first frame update
    List<Cell> cells;
    public Cell [] cellPrefabs;


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
            if (o.pos.x - cells[i].pos.x == 0)
            {
                if (o.pos.y - cells[i].pos.y == 1)
                {
                    o.down = cells[i];
                    cells[i].up = o;
                }
                if (o.pos.y - cells[i].pos.y == -1)
                {
                    o.up = cells[i];
                    cells[i].down = o;
                }
            }
            if (o.pos.y - cells[i].pos.y == 0)
            {
                if (o.pos.x - cells[i].pos.x == 1)
                {
                    o.left = cells[i];
                    cells[i].right = o;
                }
                if (o.pos.x - cells[i].pos.x == -1)
                {
                    o.right = cells[i];
                    cells[i].left = o;
                }
            }
        }
        cells.Add(o);
        Debug.Log(cells.Count);
    }
    void Start()
    {

        cells = new List<Cell>();
        Add(new Vector2Int(0, 0),1);
        Add(new Vector2Int(1, 0));
        Add(new Vector2Int(-1, 0));
        Add(new Vector2Int(-2, 0),2);
        Add(new Vector2Int(-3, 0), 2);
        Add(new Vector2Int(-3, 1), 2);
        Add(new Vector2Int(-3, 2), 2);
        Add(new Vector2Int(-3, 3), 2);
        Add(new Vector2Int(-2, 3), 2);
        Add(new Vector2Int(-4, 3), 3);
        Add(new Vector2Int(-1, 3), 3);
        //cells[0].isActivated = true;

        //cells[3].timesActivated = 1;

        //cells[2].getImpulse(cells[3]);
        //StartCoroutine(cells[2].propagateImpuls());
        //Add(new Vector2(1, 0));
    }
}
   