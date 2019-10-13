using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : CellFactory
{
    // Start is called before the first frame update
    [SerializeField] TerrainFactory tFactory;

    private void Awake()
    {
        base.Awake();
        if(tFactory==null)
        {
            Debug.Log("In buildingFactory, Awake: Lacking tFactory object");
            return;
        }
        Build(new Vector2Int(tFactory.getSize().x / 2, tFactory.getSize().y / 2), 0);
        Build(new Vector2Int((tFactory.getSize().x / 2)+1, tFactory.getSize().y / 2), 2);

        // 0 must correspond to generator building
    }

    public void Build(Vector2Int pos,int index)
    {
        Add(pos, index);
    }

    new Building Add(Vector2Int pos,int index)
    {
        Building b = (Building)base.Add(pos, index);
        b.setTerrain((Terrain)tFactory.Find(tFactory.hexToIndex(pos)));
        Vector2Int npos;
        for(int i = 0;i<6;i++)
        {
            npos = getNeighbourGridPos(pos, i);
            Debug.Log($"NPos:{npos}");
            for(int j = 0;j<cells.Count;j++)
            {
                if(cells[j].pos==npos)
                {
                    Debug.Log("Connection forced");
                    b.setNeighbour(cells[j], i);
                }
            }
        }
        cells.Add(b);
        return b;
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
