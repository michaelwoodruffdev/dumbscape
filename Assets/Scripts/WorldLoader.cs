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
    public GameObject firecapiaStatuePrefab;
    public GameObject castleWall;
    public GameObject castleWallCorner;

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

    void addFirecapeStatue(int x, int z, int rotation)
    {
        this.tileMap[x, z] = new TileData(false, "firecapestatue", rotation);
        this.walkMap[x, z] = false;
        for (int i = -2; i < 4; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                this.walkMap[x + i, z + j] = false;
            }
        }
    }

    void addCastleWall(int x, int z, int rotation)
    {
        this.tileMap[x, z] = new TileData(false, "castlewall", rotation);
        this.walkMap[x, z] = false;
    }

    void addCastleWallCorner(int x, int z, int rotation)
    {
        this.tileMap[x, z] = new TileData(false, "castlewallcorner", rotation);
        this.walkMap[x, z] = false;
    }


    public void loadWorld()
    {
        addFirecapeStatue(15, 16, 0);
        for (int i = 6; i < 26; i++)
        {
            if (i != 15 && i != 16)
            {
                addCastleWall(i, 6, 0);
            }
        }
        addCastleWallCorner(26, 6, 0);
        for (int i = 6; i < 26; i++)
        {
            //if (i != 15 && i != 16)
            //{
                addCastleWall(i, 26, 0);
            //}
        }
        addCastleWallCorner(26, 26, 3);
        for (int i = 7; i < 26; i++)
        {
            //if (i != 15 && i != 16)
            //{
                addCastleWall(26, i, 1);
            //}
        }
        addCastleWallCorner(5, 6, 1);
        for (int i = 7; i < 26; i++)
        {
            //if (i != 15 && i != 16)
            //{
                addCastleWall(5, i, 3);
            //}
        }
        addCastleWallCorner(5, 26, 2);

        addTree(10, 10, 0);
        addRock(15, 17, 0);


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (this.tileMap[i, j].tileObject == "tree")
                {
                    GameObject newTree = Instantiate(treePrefab, new Vector3(i + .5f, heightMap[i,j], j + .5f), Quaternion.Euler(90, 90 * this.tileMap[i, j].rotation, 0));
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
                if (this.tileMap[i, j].tileObject == "firecapestatue")
                {
                    GameObject newStatue = Instantiate(firecapiaStatuePrefab, new Vector3(i + .35f, heightMap[i, j], j + .5f), Quaternion.Euler(0, 90 * this.tileMap[i, j].rotation, 0));
                    this.tileMap[i, j].setGameObject(newStatue);
                }
                if (this.tileMap[i, j].tileObject == "castlewall")
                {
                    GameObject newWall = Instantiate(castleWall, new Vector3(i + .5f, heightMap[i, j], j + .5f), Quaternion.Euler(-90, 90 * this.tileMap[i, j].rotation, 0));
                    newWall.transform.localScale -= new Vector3(.5f, .5f, .5f);
                    this.tileMap[i, j].setGameObject(newWall);
                }
                if (this.tileMap[i, j].tileObject == "castlewallcorner")
                {
                    GameObject newWallCorner = Instantiate(castleWallCorner, new Vector3(i + .5f, heightMap[i, j], j + .5f), Quaternion.Euler(-90, 90 * this.tileMap[i, j].rotation, 0));
                    newWallCorner.transform.localScale -= new Vector3(49.5f, 49.5f, 49.5f);
                    this.tileMap[i, j].setGameObject(newWallCorner);
                }
            }
        }
    }
}
