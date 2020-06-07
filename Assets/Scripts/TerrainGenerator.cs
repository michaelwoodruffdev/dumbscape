using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Terrain terrain;
    int width;
    int height;
    float depth;
    float[,] heights;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        width = 32;
        height = 32;
        depth = 4;
        heights = generateHeights();
        terrain.terrainData.heightmapResolution = 33;
        terrain.terrainData.size = new Vector3(width, depth, height);
        terrain.terrainData.SetHeights(0, 0, invertHeights());
    }

    float[,] generateHeights()
    {
        float[,] newheights = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                newheights[i, j] = .5f;
            }
        }

        for (int i = 6; i < 26; i++)
        {
            newheights[i, 4] = .1f;
        }
        newheights[27, 5] = .1f;
        for (int j = 6; j < 26; j++)
        {
            newheights[4, j] = .1f;
        }
        newheights[27, 27] = .3f;
        for (int i = 6; i < 26; i++)
        {
            newheights[i, 28] = .1f;
        }
        newheights[5, 27] = .3f;
        for (int j = 6; j < 26; j++)
        {
            newheights[28, j] = .1f;
        }
        newheights[5, 5] = .1f;

        for (int i = 12; i < 21; i++)
        {
            for (int j = 14; j < 20; j++)
            {
                newheights[i, j] = .8f;
            }
        }

        return newheights;
    }

    float[,] invertHeights()
    {
        float[,] heightsInverted = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                heightsInverted[i, j] = this.heights[j, i];
            }
        }
        return heightsInverted;
    }

    public float[,] getHeightMap()
    {
        float[,] heightMapInRealUnits = new float[width, height];
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.height; j++)
            {
                heightMapInRealUnits[i, j] = this.heights[i, j] * this.depth;
            }
        }
        return heightMapInRealUnits;
    }


}
