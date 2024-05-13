using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] NoiseGenerator(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0;x<mapWidth; x++)
            {
                float sampleW= x/scale;
                float sampleH= y/scale;

                float perlinValue= Mathf.PerlinNoise(sampleW, sampleH);
                noiseMap[x,y]= perlinValue;
            }
        }

        return noiseMap;
    }
}
