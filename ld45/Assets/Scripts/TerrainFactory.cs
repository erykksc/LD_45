using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFactory : CellFactory
{
    

    [SerializeField] private Vector2Int size;

    // Start is called before the first frame update
    bool getRandomBool(float prob)
    {
        prob *= 1000f;
        return Random.Range(0, 1001) <= prob;
    }

    public Vector2Int getSize()
    {
        return size;
    }

    bool checkHexCoords(Vector2Int pos)
    {
        return (pos.x > -1 && pos.y > -1 && pos.x < size.x && pos.y < size.y);
    }

    new protected void Awake()
    {
        base.Awake();
    }

    delegate bool distribution(Vector2Int pos, float size, float ratio);

    bool Distr1(Vector2Int pos, float size, float ratio)
    {
        float dist = Mathf.Pow((pos.y) / ratio, 2) + Mathf.Pow(pos.x, 2);
        return dist < Mathf.Pow(size, 2);
    }
    bool Distr2(Vector2Int pos, float size, float ratio)
    {
        float height = Mathf.Pow(size, 2);
        return getRandomBool((height - (Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y / ratio, 2))) / (height));
    }

    public void GenerateMap()
    {
        GenerateGrid();

        GenerateRocks(5, 1);

        float rratio2 = 0.4f + Random.Range(0, 100) / 50f;

        
        for(int i = 0;i<3+ (size.x*size.y)/300f;i++)
        {
            GenerateSpot(new Vector2Int(Random.Range(0,size.x), Random.Range(0,size.y)), 6, 4+Random.Range(0,2), Distr2, 0.75f + Random.Range(0,100)/75f);
        }

        // Path Generator
        if (Random.Range(0, 2) == 1)
        {
            GenerateLine(new Vector2Int(Random.Range(0, size.x), 0), 0, 200, 2);

        }
        else
        {
            GenerateLine(new Vector2Int(0, Random.Range(0, size.y)), 0, 200, 2);
        }

        // River Generator
        if (Random.Range(0, 2) == 1)
        {
            GenerateLine(new Vector2Int(Random.Range(10, size.x-10), 0), 0, 200, 7);

        }
        else
        {
            GenerateLine(new Vector2Int(0, Random.Range(10, size.y-10)), 0, 200, 7);
        }


        Vector2Int[] lakePositions = { new Vector2Int(10, 10), new Vector2Int(size.x - 10, 10), new Vector2Int(size.x - 10, size.y - 10), new Vector2Int(10, size.y - 10) };

        float lsize, ratio;

        Vector2Int shift;

        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 7) < 5)
            {
                shift = new Vector2Int(Random.Range(-5, 5), Random.Range(-5, 5));
                lsize = Random.Range(0, 2) + 3;

                ratio = Random.Range(0, 100) / 125f + 0.75f;
                GenerateSpot(lakePositions[i]+shift, 4, lsize+2, Distr1, ratio);
                GenerateSpot(lakePositions[i]+shift, 7, lsize, Distr1, ratio);
            }
        }


        Vector2Int center = new Vector2Int(size.x / 2, size.y / 2);
        for(int i = 0;i<6;i++)
        {
            for(int j = 0;j<6; j++)
            {
                if(Mathf.Pow(i,2)+Mathf.Pow(j,2)<36)
                {
                    //if(((Terrain)cells[hexToIndex(center + new Vector2Int(i, j))]).buildable!=3)
                    {
                        Cell cell = Add(center + new Vector2Int(i, j), 0);
                        Swap(cells[hexToIndex(center + new Vector2Int(i, j))], cell);
                    }

                    //if (((Terrain)cells[hexToIndex(center + new Vector2Int(-i, j))]).buildable != 3)
                    {
                        Cell cell = Add(center + new Vector2Int(-i, j), 0);
                        Swap(cells[hexToIndex(center + new Vector2Int(-i, j))], cell);
                    }

                    //if (((Terrain)cells[hexToIndex(center + new Vector2Int(i, -j))]).buildable != 3)
                    {
                        Cell cell = Add(center + new Vector2Int(i, -j), 0);
                        Swap(cells[hexToIndex(center + new Vector2Int(i, -j))], cell);
                    }

                    //if (((Terrain)cells[hexToIndex(center + new Vector2Int(-i, -j))]).buildable != 3)
                    {
                        Cell cell = Add(center + new Vector2Int(-i, -j), 0);
                        Swap(cells[hexToIndex(center + new Vector2Int(-i, -j))], cell);
                    }
                }
            }
        }

        ((Terrain)cells[hexToIndex(new Vector2Int(size.x / 2, size.y / 2))]).distToGen = 0;
        Dijkstra(((Terrain)cells[hexToIndex(new Vector2Int(size.x / 2, size.y / 2))]));

        //Debug.Log($"IS prob func working: {getRandomBool(2)}");
        Debug.Log($"cells Count: {cells.Count}");
    }

    public int hexToIndex(Vector2Int pos)
    {
        return pos.y + pos.x * size.x;
    }

    public void Swap(Cell a, Cell b)
    {
        if (b == null || a == null)
        {
            Debug.Log("In TerrainFactory, Swap: on or more parameters is null");
            return;
        }
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] == a)
            {
                for (int k = 0; k < 6; k++)
                {
                    b.setNeighbour(a.getNeighbour(k), k);
                    a.setNeighbour(null, k);
                }
                cells[i] = b;
                b.isCellShown = true;
                Destroy(a.gameObject);
                break;
            }
        }
    }

    private void GenerateGrid()
    {
        for (int i = 0, c = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++, c++)
            {
                cells.Add(Add(new Vector2Int(i, j), 0));
                cells[c].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 60 * Random.Range(0, 7)));
            }
        }
        Vector2Int neighbour;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    neighbour = getNeighbourGridPos(new Vector2Int(i, j), k);
                    if (neighbour.x > -1 && neighbour.x < size.x && neighbour.y > -1 && neighbour.y < size.y && neighbour.x != -1 && neighbour.y != -1)
                    {
                        cells[hexToIndex(new Vector2Int(i, j))].setNeighbour(cells[hexToIndex(neighbour)], k);
                    }
                }
            }
        }
    }

    void GenerateLine(Vector2Int pos, int dir, int size, int index)
    {
        Cell origin;
        Cell prev = cells[hexToIndex(pos)];
        if (prev == null)
        {
            Debug.Log("In TerrainFactory, GenerateLine: input is null");
            return;
        }
        for (int i = 0; i < size && prev != null; i++)
        {
            pos = prev.pos;
            origin = Add(pos, index);
            prev = cells[hexToIndex(pos)];
            if (prev == null)
            {
                Destroy(origin.gameObject);
                break;
            }
            Swap(prev, origin);
            prev = origin.getNeighbour((dir + Random.Range(0, 3) + 5) % 6);
        }
    }
    void GenerateSpot(Vector2Int pos, int index, float size, distribution distr, float ratio = 1)
    {
        Cell temp;
        Cell origin;
        Vector2Int p;
        float dist;
        for (int i = (int)(pos.x - size); i < pos.x + size; i++)
        {
            for (int j = (int)(pos.y - size * ratio); j < pos.y + size * ratio; j++)
            {
                p = new Vector2Int(i, j);
                dist = Mathf.Pow((j - pos.y) / ratio, 2) + Mathf.Pow(i - pos.x, 2);

                if (distr(p - pos, size, ratio) && checkHexCoords(p))
                {
                    origin = Add(p, index);
                    temp = cells[hexToIndex(p)];

                    //Water over sand condition - to Fix
                    if(temp.ID==7&&origin.ID==4)
                    {
                        continue;
                    }
                    Swap(temp, origin);
                }
            }
        }
    }

    public bool buildBridge(Vector2Int pos)
    {
        Cell cell = Find(pos);
        Cell temp;
        if(cell==null)
        {
            return false;
        }
        if(cell.ID==7)
        {
            temp = Add(pos, 8);
            Swap(cell, temp);
            return true;
        }
        return false;
    }

    void GenerateRocks(int index, int den)
    {
        int xstep, ystep;
        xstep = size.x / 10;
        ystep = size.y / 10;

        Vector2Int pos;
        int density;
        Cell current;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                density = den + Random.Range(0, 3);
                for (int k = 0; k < density; k++)
                {
                    pos = new Vector2Int(Random.Range(0, xstep) + i * xstep, Random.Range(0, ystep) + j * ystep);
                    if (!checkHexCoords(pos))
                    {
                        continue;
                    }
                    current = Add(pos, index);
                    Swap(cells[hexToIndex(pos)], current);
                }
            }
        }
    }

    void Dijkstra(Terrain cell)
    {
        Terrain neighbour;
        for(int i = 0;i<6;i++)
        {
            neighbour = (Terrain)cell.getNeighbour(i);
            if(neighbour!=null)
            {
                if(neighbour.distToGen>cell.distToGen+1&&(neighbour.buildable==0||neighbour.buildable==1))
                {
                    neighbour.distToGen = cell.distToGen+1;
                    Dijkstra(neighbour);
                }
            }
        }
    }

    new Terrain Find(Vector2Int pos)
    {
        if(!checkHexCoords(pos))
        {
            return null;
        }
        if(hexToIndex(pos)>cells.Count-1||hexToIndex(pos)<0)
        {
            return null;
        }
        return (Terrain) cells[hexToIndex(pos)];
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
