using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour
{
    TileData[,] tileMap;
    bool[,] walkMap;
    int width;
    int height;

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
        this.tileMap[3, 5] = new TileData(false, "tree", 1);
        walkMap[3, 5] = false;
        this.tileMap[10, 2] = new TileData(false, "tree", 3);
        walkMap[10, 2] = false;
        this.tileMap[4, 12] = new TileData(false, "tree");
        walkMap[4, 12] = false;
        this.tileMap[13, 15] = new TileData(false, "tree", 2);
        walkMap[13, 15] = false;
        this.tileMap[15, 6] = new TileData(false, "tree", 1);
        walkMap[15, 6] = false;
        this.tileMap[15, 17] = new TileData(false, "tree", 3);
        walkMap[15, 17] = false;
        this.tileMap[7, 15] = new TileData(false, "tree");
        walkMap[7, 15] = false;
        this.tileMap[1, 10] = new TileData(false, "tree", 1);
        walkMap[1, 10] = false;
        this.tileMap[18, 3] = new TileData(false, "tree");
        walkMap[18, 3] = false;
        this.tileMap[12, 11] = new TileData(false, "tree");
        walkMap[12, 11] = false;
        this.tileMap[7, 9] = new TileData(false, "tree", 2);
        walkMap[7, 9] = false;
        this.tileMap[2, 3] = new TileData(false, "rock");
        walkMap[2, 3] = false;
        this.tileMap[7, 10] = new TileData(false, "rock", 3);
        walkMap[7, 10] = false;
        this.tileMap[7, 3] = new TileData(false, "rock", 2);
        walkMap[7, 3] = false;
        this.tileMap[15, 4] = new TileData(false, "rock");
        walkMap[15, 4] = false;
        this.tileMap[15, 14] = new TileData(false, "rock", 2);
        walkMap[15, 14] = false;
        this.tileMap[6, 17] = new TileData(false, "rock");
        walkMap[6, 17] = false;
        this.tileMap[8, 19] = new TileData(false, "rock", 3);
        walkMap[8, 19] = false;
        this.tileMap[8, 18] = new TileData(false, "rock", 3);
        walkMap[8, 18] = false;
        this.tileMap[8, 17] = new TileData(false, "rock", 3);
        walkMap[8, 17] = false;
        this.tileMap[8, 16] = new TileData(false, "rock", 3);
        walkMap[8, 16] = false;
        this.tileMap[8, 15] = new TileData(false, "rock", 3);
        walkMap[8, 15] = false;
        addRock(3, 19, 0);
        addRock(3, 18, 0);
        addRock(3, 17, 0);
        addRock(3, 16, 0);
        addRock(3, 15, 0);
        addRock(3, 14, 0);
        addRock(3, 13, 0);
        addRock(4, 13, 0);
        addRock(5, 13, 0);
        addRock(6, 13, 0);
        addRock(7, 13, 0);
        addRock(8, 13, 0);
        addRock(8, 12, 0);
        addRock(8, 13, 0);
        addRock(8, 14, 0);
        addRock(8, 15, 0);
        addRock(0, 8, 0);
        addRock(1, 8, 0);
        addRock(2, 8, 0);
        addRock(3, 8, 0);
        addRock(4, 8, 0);
        addRock(5, 8, 0);
        addRock(6, 8, 0);
        addRock(7, 8, 0);
        addRock(8, 8, 0);
        addRock(9, 8, 0);
        addRock(10, 8, 0);
        addRock(6, 7, 0);
        addRock(6, 6, 0);
        addRock(6, 5, 0);
        addRock(6, 4, 0);
        addRock(6, 3, 0);
        addRock(6, 2, 0);
        addRock(13, 19, 0);
        addRock(13, 18, 0);
        addRock(13, 17, 0);
        addRock(13, 16, 0);
        addRock(13, 15, 0);
        addRock(13, 14, 0);
        addRock(13, 13, 0);
        addRock(13, 12, 0);
        addRock(13, 11, 0);
        addRock(13, 10, 0);
        addRock(13, 9, 0);
        addRock(13, 8, 0);
        addRock(13, 7, 0);
        addRock(13, 6, 0);
        addRock(13, 5, 0);
        addRock(13, 4, 0);
        addRock(14, 10, 0);
        addRock(15, 10, 0);
        addRock(16, 10, 0);
        addRock(17, 10, 0);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (this.tileMap[i, j].tileObject == "tree")
                {
                    GameObject newTree = Instantiate(treePrefab, new Vector3(i + .5f, 0, j + .5f), Quaternion.Euler(90, 90 * this.tileMap[i, j].rotation, 0));
                    newTree.transform.Translate(0f, 2.2f, 0f, Space.World);
                    newTree.transform.localScale -= new Vector3(.6f, .6f, .6f);
                    this.tileMap[i, j].setGameObject(newTree);
                }
                if (this.tileMap[i, j].tileObject == "rock")
                {
                    GameObject newRock = Instantiate(rockPrefab, new Vector3(i + .5f, 0f, j + .5f), Quaternion.Euler(90, 90 * this.tileMap[i, j].rotation, 0));
                    newRock.transform.localScale -= new Vector3(40f, 70f, 40f);
                    newRock.transform.Translate(0f, -.03f, 0f, Space.World);
                    this.tileMap[i, j].setGameObject(newRock);
                }
            }
        }
    }
}
