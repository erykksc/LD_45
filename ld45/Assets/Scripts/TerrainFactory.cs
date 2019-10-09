using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFactory : CellFactory
{
    // Start is called before the first frame update
    private void Awake()
    {
        base.Awake();

    }

    Vector2Int getNeighbourGridPos(Vector2Int pos, int index)
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
