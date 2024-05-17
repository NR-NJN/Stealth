using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] NoiseGenerator(int mapWidth, int mapHeight, float scale, float lacunarity, float persistance, int octaves)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0;x<mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleW = x / scale*frequency;
                    float sampleH = y / scale*frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleW, sampleH) * 2 - 1; // Value between -1.0 and 1.0.
                    noiseHeight += perlinValue*amplitude;
                }
                noiseMap[y, x] = noiseHeight;
            }
        }

        return noiseMap;
    }
}
