using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour
{
    TileData[,] tileMap;
    bool[,] walkMap;
    int width;
    int height;
    float[,] heightMap;

    // World Objects
    public GameObject treePrefab;
    public GameObject rockPrefab;

    public WorldLoader(TileData[,] tileMap, bool[,] walkMap, int width, int height)
    {
        this.tileMap = tileMap;
        this.walkMap = walkMap;
        this.width = width;
        this.height = height;
    }

    public void intializeWorldData(TileData[,] tileMap, bool[,] walkMap, int width, int height)
    {
        this.tileMap = tileMap;
        this.walkMap = walkMap;
        this.width = width;
        this.height = height;
        this.heightMap = GameObject.Find("Terrain").GetComponent<TerrainGenerator>().getHeightMap();
        Debug.Log(this.treePrefab.ToString());
    }

    void addRock(int x, int z, int rotation)
    {
        this.tileMap[x, z] = new TileData(false, "rock", rotation);
        this.walkMap[x, z] = false;
    }

    void addTree(int x, int z, int rotation)
    {
        this.tileMap[x, z] = new TileData(false, "tree", rotation);
        this.walkMap[x, z] = false;
    }


    public void loadWorld()
    {
        addRock(25, 25, 2);
        addRock(26, 25, 3);
        addRock(26, 26, 1);
        addRock(22, 23, 2);
        addRock(22, 24, 1);
        addRock(22, 31, 1);
        addRock(31, 31, 2);
        addRock(25, 30, 2);
        addRock(24, 30, 1);
        addRock(23, 30, 2);
        addRock(22, 30, 0);
        addRock(28, 30, 2);
        addRock(28, 29, 2);
        addRock(28, 28, 1);
        addRock(28, 27, 2);
        addRock(28, 26, 3);

        for (int i = 23; i < 30; i+=1)
        {
            for (int j = 2; j < 18; j+=3)
            {
                addTree(i, j, 1);
            }
        }

        addTree(3, 2, 1);
        addTree(1, 12, 2);
        addTree(14, 3, 0);
        addTree(8, 5, 2);
        addTree(16, 18, 1);
        addTree(14, 16, 2);
        addTree(18, 10, 1);
        addTree(8, 22, 1);
        addTree(17, 25, 1);

        addRock(4, 2, 0);
        addRock(6, 18, 1);
        addRock(10, 12, 0);
        addRock(14, 12, 2);
        addRock(16, 5, 2);
        addRock(10, 5, 0);
        addRock(19, 3, 1);
        addRock(3, 16, 2);




        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (this.tileMap[i, j].tileObject == "tree")
                {
                    GameObject newTree = Instantiate(treePrefab, new Vector3(i + .5f, heightMap[i,j], j + .5f), Quaternion.Euler(90, 90 * this.tileMap[i, j].rotation, 0));
                    Debug.Log("here" + heightMap[i, j]);
                    newTree.transform.Translate(0f, 2.2f, 0f, Space.World);
                    newTree.transform.localScale -= new Vector3(.6f, .6f, .6f);
                    this.tileMap[i, j].setGameObject(newTree);
                }
                if (this.tileMap[i, j].tileObject == "rock")
                {
                    GameObject newRock = Instantiate(rockPrefab, new Vector3(i + .5f, heightMap[i,j], j + .5f), Quaternion.Euler(90, 90 * this.tileMap[i, j].rotation, 0));
                    newRock.transform.localScale -= new Vector3(40f, 70f, 40f);
                    newRock.transform.Translate(0f, -.03f, 0f, Space.World);
                    this.tileMap[i, j].setGameObject(newRock);
                }
            }
        }
    }
}
