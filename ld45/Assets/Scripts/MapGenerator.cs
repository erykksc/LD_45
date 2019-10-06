using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public CellFactory cellFactory;
    public CellFactory grassFactory;


    private void Start()
    {
        Debug.Log($"In MapGenerator length of prefabs in grass {grassFactory.cellPrefabs.Length}");
        lists = new List<Cell>[grassFactory.cellPrefabs.Length];
        CreateShape();
        generateSeed(1, 10, 0);
        //for(int i = 0;i<lists[1].Count;i++)
        Filling(1, 0,3, 0, 7);
        Filling(1, 1, 3, 0, 7);
        Filling(1, 2, 3, 0, 7);
        Filling(1, 3, 3, 0, 7);
    }

    Vector2Int[] vertices;
    public int xSize;
    public int ySize;

    List<Cell>[] lists;

    private int pickTile()
    {
        int[] numbers = {0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7};
        int randInt = Random.Range(0, numbers.Length-1);
        return numbers[randInt];
    }

    public float[,] GenerateNoiseMap(int ySize, int xSize, float scale)
    {
        float[,] noiseMap = new float[ySize, xSize];

        for (int yIndex = 0; yIndex < ySize; yIndex++)
        {
            for (int xIndex = 0; xIndex < xSize; xIndex++)
            {
                float sampleX = xIndex / scale;
                float sampleY = yIndex / scale;
                float noise = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[yIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }

    void CreateShape()
    {
        int i = 0;
        vertices = new Vector2Int[(xSize + 1) * (ySize + 1)];
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector2Int(x, y);
                i++;
            }
        }
        factoryAddVerticies();
    }

    private void factoryAddVerticies()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            //Creating a tile on pos Vert[i],setting it's sprtite by Picktile, setting its rotation too.
            Cell cell = grassFactory.Add(vertices[i], 0);
            cell.transform.localRotation = Quaternion.Euler(0,0,60*(int)Random.Range(1,7));
        }
    } 
    void generateSeed(int index,int count,int size)
    {
        Cell a, b;
        lists[index] = new List<Cell>();
        Vector2Int rPos = new Vector2Int(0,0);
        for(int i = 0;i<count;i++)
        {
            
            rPos = new Vector2Int((int)((float)xSize*Random.Range(0f,1f)), (int)((float)ySize * Random.Range(0f, 1f)));
            //Debug.Log($"pos: {rPos} on {i}");
            grassFactory.DestroyCell(rPos);
            lists[index].Add(grassFactory.Add(rPos, index));
        }
    }
    public bool canBuild(Propagateable cell,int index,int min,int max)
    {
        if(cell.getNeighbourCount(index)<min|| cell.getNeighbourCount(index)>max)
        {
            return false;
        }
        for(int i = 0;i<6;i++)
        {
            if (cell.neighbours[i].getNeighbourCount(index) < min-1 || cell.neighbours[i].getNeighbourCount(index) > max-1)
            {
                return false;
            }
        }
        if(cell.terrainType==index)
        {
            return false;
        }
        return true;
    }
    void Filling(int index,int index2,int size,int minNeig,int maxNeig)
    {
        Vector2Int rPos;
        Propagateable current;
        List<Propagateable> available = new List<Propagateable>();
       
        current = lists[index][index2];

        for(int i = 0;i<6;i++)
        {
            available.Add(current.neighbours[i]);
        }
        for(int j = 0;j<size&&available.Count!=0;j++)
        {
            for (int i = 0; i < available.Count; i++)
            {
                if (available[i].terrainType == index)
                {
                    available.RemoveAt(i);
                }
            }
            current = available[(int)((float)(available.Count-1) * Random.Range(0f, 1f))];

            rPos = current.pos;
            available.Remove(current);
            grassFactory.DestroyCell(rPos);
            grassFactory.Add(rPos, index);
            for (int i = 0; i < 6; i++)
            {
                if(!available.Contains(current.neighbours[i]))
                {
                    //available.Add(current.neighbours[i]);
                }
            }
            Debug.Log($"filling index{j}");
            //break;
        }
    }
    
}
