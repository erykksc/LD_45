using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public CellFactory cellFactory;
    public CellFactory grassFactory;


    private void Start()
    {
        CreateShape();

        genRocks(5);
        //genSpot(new Vector2Int(10, 9), new Vector2Int(1, 6), 8);//genRocks(5);
        //genSpot(new Vector2Int(10, 10), new Vector2Int(1, 5), 7);
    }

    Vector2Int[] vertices;
    public int xSize;
    public int ySize;

    private int pickTile()
    {
        int[] numbers = {0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9};
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
            grassFactory.Add(vertices[i], 0).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
            //cell.transform.localRotation = Quaternion.Euler(0,0,60*Random.Range(1,7));
        }
    }
    private void genRocks(int index)
    {
        int xs = xSize / 10;
        int ys = ySize / 10;
        Vector2Int pos;
        for(int i = 0;i<xs;i++)
        {
            for(int j = 0;j<ys;j++)
            {
                for(int k = 0;k<12;k++)
                {
                    pos = new Vector2Int((int)Random.Range(0, 10) + i * 10, (int)Random.Range(0, 10) + j * 10);
                    grassFactory.DestroyCell(grassFactory.Find(pos));
                    grassFactory.Add(pos, index).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
                }
            }
        }
    }
    private void genSpot(Vector2Int pos3, Vector2Int size,int index)
    {
        Vector2Int pos;
        int xoffset = 0;
        for(int i = -2;i<size.y*2+2;i++)
        {
            xoffset = (int)Mathf.Sqrt(Mathf.Pow(size.x+size.y, 2) - Mathf.Pow(i - size.y, 2));
            //Debug.Log($"offset: {xoffset}");
            for(int j = -xoffset;j<size.x*2+xoffset;j++)
            {
                pos = new Vector2Int(pos3.x+j+(pos3.y + i)%2,pos3.y+i);
                grassFactory.DestroyCell(grassFactory.Find(pos));
                grassFactory.Add(pos, index).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
            }
        }
    }
}
