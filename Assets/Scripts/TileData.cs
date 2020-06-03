using UnityEngine;


// class for storing data about each individual tile on the map
public class TileData
{
    public bool walkable;
    public string tileObject;
    GameObject gameObject;
    public int rotation;

    public TileData()
    {
        this.walkable = true;
    }

    public TileData(bool _walkable, string _tileObject)
    {
        this.walkable = _walkable;
        this.tileObject = _tileObject;
        this.rotation = 0;
    }

    public TileData(bool _walkable, string _tileObject, int _rotation)
    {
        this.walkable = _walkable;
        this.tileObject = _tileObject;
        this.rotation = _rotation;
    }

    public void setGameObject(GameObject _gameObject)
    {
        this.gameObject = _gameObject;
    }
}
