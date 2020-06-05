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

        for (int i = 20; i < width; i++)
        {
            for (int j = 20; j < height; j++)
            {
                newheights[i, j] = 0f;
            }
        }

        for (int i = 4; i < 10; i++)
        {
            for (int j = 10; j < 16; j++)
            {
                newheights[i, j] = .7f;
            }
        }

        for (int i = 15; i < 19; i++)
        {
            for (int j = 4; j < 10; j++)
            {
                newheights[i, j] = .4f;
            }
        }

        for (int i = 1; i < 12; i++)
        {
            for (int j = 25; j < 32; j++)
            {
                newheights[i, j] = .2f;
            }
        }

        for (int i = 21; i < 31; i++)
        {
            for (int j = 1; j < 19; j++)
            {
                newheights[i, j] = .65f;
            }
        }

        for (int i = 2; i < 20; i++)
        {
            for (int j = 2; j < 8; j++)
            {
                newheights[i, j] = .6f;
            }
        }

        for (int i = 17; i < 20; i++)
        {
            for (int j = 19; j < 24; j++)
            {
                newheights[i, j] = .7f;
            }
        }

        for (int i = 3; i < 14; i++)
        {
            for (int j = 21; j < 24; j++)
            {
                newheights[i, j] = .45f;
            }
        }

        for (int i = 0; i < 19; i++)
        {
            newheights[i, 31] = .5f;
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
