using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int maxDepth;
    public int maxWidth;
   public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale)
    {
        float[,] noiseMap = new float[maxDepth, maxWidth];

        for (int yIndex = 0; yIndex < mapDepth; yIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                float sampleX = xIndex / scale;
                float sampleY = yIndex / scale;
                float noise = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[yIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }
}
