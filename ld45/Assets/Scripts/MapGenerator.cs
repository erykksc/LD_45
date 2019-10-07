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

        //genPatch(new Vector2Int(20, 20), 7,50, 0);

        genRocks(5);

        //genRocks();

        int size;
        Vector2Int pos = new Vector2Int(0,0);
        Vector2Int delta = new Vector2Int(1, -4);
        Vector2Int gPos = new Vector2Int(xSize / 2, ySize / 2);
        for (int i = 0; i < 1 + Random.Range(0, 2); i++)
        {
            size = 1 + Random.Range(0, 3);
            pos.x = Random.Range(0, xSize);
            pos.y = Random.Range(0, ySize);
            genLooseSpot(pos, new Vector2Int(1, size),9);
            genLooseSpot(pos, new Vector2Int(1, size), 9);
        }
        for (int i = 0;i<1+Random.Range(0,2);i++)
        {
            do
            {
                size = 2 + Random.Range(0, 4);
                pos.x = Random.Range(0, xSize);
                pos.y = Random.Range(0, ySize);
            } while (Mathf.Abs(pos.x - gPos.x) < (size + 5)|| Mathf.Abs(pos.y - gPos.y) < 3);

            genSpot(pos+delta, new Vector2Int(1, size+4), 8);
            
            genSpot(pos, new Vector2Int(2, size), 7);
            if(Random.Range(0,2)==1)
            {
                genPatch(pos, 7, xSize + ySize, Random.Range(0, 7));
            }
        }
        
        for(int i = 0;i<5;i++)
        {
            for(int j = 0;j<5;j++)
            {
                if(Mathf.Pow(i,2)+Mathf.Pow(j,2)<25)
                {
                    // Water condition
                    pos = new Vector2Int(i,j);
                    grassFactory.DestroyCell(grassFactory.Find(gPos + pos));
                    grassFactory.Add(gPos+pos, Random.Range(0, 2)).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));

                    pos.x = -pos.x;
                    grassFactory.DestroyCell(grassFactory.Find(gPos + pos));
                    grassFactory.Add(gPos+pos, Random.Range(0, 2)).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));

                    pos.y = -pos.y;
                    grassFactory.DestroyCell(grassFactory.Find(gPos + pos));
                    grassFactory.Add(gPos+pos, Random.Range(0, 2)).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));

                    pos.x = -pos.x;
                    grassFactory.DestroyCell(grassFactory.Find(gPos + pos));
                    grassFactory.Add(gPos+pos, Random.Range(0, 2)).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
                }
            }
        }
        //genSpot(new Vector2Int(10, 9), new Vector2Int(1, 6), 8);//genRocks(5);
    }
    // to add : river/patch
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
            grassFactory.Add(vertices[i], Random.Range(0,2)).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
            //cell.transform.localRotation = Quaternion.Euler(0,0,60*Random.Range(1,7));
        }
    }
    private void genRocks(int index)
    {
        int xs = xSize / 10;
        int ys = ySize / 10;
        int count;
        Vector2Int pos;
        for(int i = 0;i<xs;i++)
        {
            for(int j = 0;j<ys;j++)
            {
                count = 3 + Random.Range(0, 10);
                for(int k = 0;k<count;k++)
                {
                    pos = new Vector2Int((int)Random.Range(0, 10) + i * 10, (int)Random.Range(0, 10) + j * 10);
                    grassFactory.DestroyCell(grassFactory.Find(pos));
                    grassFactory.Add(pos, index).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
                }
            }
        }
    }
    private void genSpot(Vector2Int pos3, Vector2Int size,int index,bool random = false)
    {
        Vector2Int pos;
        int xoffset = 0;
        int minx, maxx;
        minx = maxx = 0;
        for(int i = -2;i<size.y*2+2;i++)
        {
            xoffset = (int)Mathf.Sqrt(Mathf.Pow(size.x+size.y, 2) - Mathf.Pow(i - size.y, 2));
            if(random)
            {
                minx = Random.Range(-3, 1);
                maxx = Random.Range(0, 4);
            }
            Debug.Log($"offseton: {xoffset}");
            for(int j = -xoffset+minx;j<size.x*2+xoffset+maxx;j++)
            {
                pos = new Vector2Int(pos3.x+j+(pos3.y + i)%2,pos3.y+i);
                grassFactory.DestroyCell(grassFactory.Find(pos));
                grassFactory.Add(pos, index).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
            }
        }
    }
    private void genLooseSpot(Vector2Int pos3, Vector2Int size, int index)
    {
        Vector2Int pos;
        int xoffset = 0;
        for (int i = -2; i < size.y * 2 + 2; i++)
        {
            xoffset = (int)Mathf.Sqrt(Mathf.Pow(size.x + size.y, 2) - Mathf.Pow(i - size.y, 2));
            Debug.Log($"offset: {xoffset}");
            for (int j = -xoffset; j < size.x * 2 + xoffset; j++)
            {
                pos = new Vector2Int(pos3.x + (j + (pos3.y + i) % 2)*2+Random.Range(0,2), pos3.y + i*2 + Random.Range(0, 2));
                grassFactory.DestroyCell(grassFactory.Find(pos));
                grassFactory.Add(pos, index).transform.localRotation = Quaternion.Euler(0, 0, 60 * Random.Range(1, 7));
            }
        }
    }
    private void genPatch(Vector2Int iPos,int index,int size,int dir = 0)
    {
        Propagateable cell = grassFactory.Find(iPos);
        Vector2Int pos = new Vector2Int(0, 0);
        for(int i = 0;i<size;i++)
        {
            pos = cell.pos;
            grassFactory.DestroyCell(cell.pos);
            cell = grassFactory.Add(pos, index);
            cell = cell.neighbours[(dir +Random.Range(0,3)+5) % 6];
        }
        return;
    }
    private void shapeCorrection(Vector2Int p1,Vector2Int p2,int indexf,int indext,int cMin,int cMax)
    {
        for (int i = p1.x; i < p2.x; i++) 
        {
            for (int j = p1.y; j < p2.y; j++) 
            {
                int count = 0;
                for(int k = 0;k<6;k++)
                {
                    //if (grassFactory.Find(new Vector2Int(i, j)).neighbours[k].)

                }
                //if(grassFactory.Find(new Vector2Int(i,j)))
            }
        }
    }
}
