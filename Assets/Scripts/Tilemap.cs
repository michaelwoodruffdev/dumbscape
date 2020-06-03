using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    int width;
    int height;

    TileData[,] map;
    bool[,] walkMap;

    GameObject player;
    public int pathLimit;

    WorldLoader worldLoader;

    // World Objects
    public GameObject treePrefab;
    public GameObject rockPrefab;

    void addRock(int x, int z, int rotation)
    {
        map[x, z] = new TileData(false, "rock", rotation);
        walkMap[x, z] = false;
    }

    void addTree(int x, int z, int rotation)
    {
        map[x, z] = new TileData(false, "tree", rotation);
        walkMap[x, z] = false;
    }

    void Start()
    {
        width = 20;
        height = 20;
        player = GameObject.Find("Player");
        this.pathLimit = 45;
        map = new TileData[width, height];
        this.walkMap = new bool[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = new TileData();
                walkMap[i, j] = true;
            }
        }

        this.worldLoader = GetComponent<WorldLoader>();
        worldLoader.intializeWorldData(map, walkMap, width, height);
        worldLoader.loadWorld();
    }

    // Update is called once per frame
    void Update()
    {
        // check for click
        if (Input.GetMouseButtonUp(0))
        {
            // cast ray to see what was clicked (within a range)
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Transform hitTransform = hit.transform;

                // if a tile was hit
                if (hitTransform.ToString().Contains("Ground"))
                {
                    Stack<Vertex> vertexPath = findPath(this.player.transform.position, hit.point);
                    this.player.GetComponent<PlayerScript>().setVertexPath(vertexPath);
                }
            }
        }
    }

    public Stack<Vertex> findPath(Vector3 start, Vector3 destination)
    {
        if (Mathf.Abs(destination.x - start.x) > this.pathLimit / 2 - 2 || Mathf.Abs(destination.z - start.z) > this.pathLimit / 2 - 2)
        {
            return new Stack<Vertex>();
        }
        Vertex endPointToTrace = dijkstras(start, destination);
        Stack<Vertex> vertexPath = new Stack<Vertex>();
        Vertex current = endPointToTrace;
        while (current != null)
        {
            vertexPath.Push(current);
            current = current.previous;
        }
        return vertexPath;
    }

    public Vertex findLeastDistance(List<Vertex> trackedVertices)
    {
        if (trackedVertices.Count == 0)
        {
            return null;
        }
        Vertex minVertex = trackedVertices[0];
        int minIndex = 0;
        int minDistance = minVertex.distance;
        for (int i = 0; i < trackedVertices.Count; i++)
        {
            if (trackedVertices[i].distance < minDistance)
            {
                minVertex = trackedVertices[i];
                minDistance = minVertex.distance;
                minIndex = i;
            }
        }
        trackedVertices.RemoveAt(minIndex);
        return minVertex;
    }

    public class Vertex
    {
        public int x;
        public int z;
        public Vertex previous;
        public int distance;
        public bool walkable;
        public bool visited;
        public int indexI;
        public int indexJ;

        public Vertex(int _x, int _y, bool _walkable)
        {
            this.x = _x;
            this.z = _y;
            this.previous = null;
            this.distance = -1;
            this.walkable = _walkable;
            this.visited = false;
        }

        public Vertex(int _x, int _y, bool _walkable, int _indexI, int _indexJ)
        {
            this.x = _x;
            this.z = _y;
            this.previous = null;
            this.distance = -1;
            this.walkable = _walkable;
            this.visited = false;
            this.indexI = _indexI;
            this.indexJ = _indexJ;
        }
    }

    Vertex dijkstras(Vector3 start, Vector3 destination)
    {
        int destX = (int)Mathf.Floor(destination.x);
        int destZ = (int)Mathf.Floor(destination.z);
        int startX = (int)Mathf.Floor(start.x);
        int startZ = (int)Mathf.Floor(start.z);

        if (startX == destX && startZ == destZ)
        {
            return null;
        }

        // initialize tiles to examine
        Vertex[,] vertices = new Vertex[this.pathLimit, this.pathLimit];
        for (int i = 0; i < this.pathLimit; i++)
        {
            for (int j = 0; j < this.pathLimit; j++)
            {
                int actualXCoord = startX - (this.pathLimit / 2 - 1) + i;
                int actualZCoord = startZ - (this.pathLimit / 2 - 1) + j;
                bool walkable = (actualXCoord >= 0 && actualXCoord < this.width && actualZCoord >= 0 && actualZCoord < this.height) ? this.walkMap[actualXCoord, actualZCoord] : false;
                vertices[i, j] = new Vertex(actualXCoord, actualZCoord, walkable, i, j);
            }
        }

        List<Vertex> vertexList = new List<Vertex>();

        int startTileIndex = this.pathLimit / 2 - 1;
        vertices[startTileIndex, startTileIndex].distance = 0;
        vertices[startTileIndex, startTileIndex].visited = true;
        vertexList.Add(vertices[startTileIndex, startTileIndex]);
        while (vertexList.Count > 0)
        {
            Vertex currentVertex = findLeastDistance(vertexList);
            Vertex currentNeighbor;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((i == 0 && j == 0) || currentVertex.x + i < 0 || currentVertex.x + i > 19 || currentVertex.z + j < 0 || currentVertex.z + j > 19)
                    {
                        continue;
                    }
                    if (currentVertex.indexI + i < 0 || currentVertex.indexI + i >= this.pathLimit || currentVertex.indexJ + j < 0 || currentVertex.indexJ + j >= this.pathLimit)
                    {
                        continue;
                    }   
                    currentNeighbor = vertices[currentVertex.indexI + i, currentVertex.indexJ + j];
                    if (currentNeighbor.visited || !currentNeighbor.walkable)
                    {
                        continue;
                    }
                    currentNeighbor.visited = true;
                    if (i == 0 || j == 0)
                    {
                        currentNeighbor.distance = currentVertex.distance + 3;
                    }
                    else
                    {
                        currentNeighbor.distance = currentVertex.distance + 4;
                    }
                    currentNeighbor.previous = currentVertex;
                    if (currentNeighbor.x == destX && currentNeighbor.z == destZ)
                    {
                        return currentNeighbor; 
                    }
                    vertexList.Add(currentNeighbor);
                }
            }
        }
        return null;
    }
}
