using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public void MapGenerate()
    {
        float[,] noiseMap = Noise.NoiseGenerator(mapWidth, mapHeight, noiseScale);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawNoise(noiseMap);
    }                                         
}                                             
