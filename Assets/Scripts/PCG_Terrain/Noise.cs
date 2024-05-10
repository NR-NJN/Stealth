using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] NoiseGenerator(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        for (int x = 0; x > mapHeight; x++)
        {
            for (int y = 0;y>mapWidth; y++)
            {
                float sampleH= x/scale;
                float sampleW= y/scale;
            }
        }

        return noiseMap;
    }
}
