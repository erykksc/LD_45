using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : ScriptableObject
{
    // Start is called before the first frame update
    List<Cell> cells;
    Cell cell;

    public void Add(Vector2 pos)
    {
        Cell o = Instantiate(cell);
        for(int i = 0;i<cells.Count;i++)
        {
            if(o.pos.x-cells[i].pos.x==0)
            {
                if(o.pos.y-cells[i].pos.y==1)
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
            if(o.pos.y-cells[i].pos.y==0)
            {
                if(o.pos.x-cells[i].pos.x==1)
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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
