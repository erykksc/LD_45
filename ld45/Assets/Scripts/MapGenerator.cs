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
        //for(int i = 0;i<)
    }
    
}
