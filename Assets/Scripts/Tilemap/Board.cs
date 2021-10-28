using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{

    //TODO: make it singleton

    private static Board _instance;

    public Dictionary<TileBase, List<Vector2Int>> walls = new Dictionary<TileBase, List<Vector2Int>>();
    public List<TileBase> tiles = new List<TileBase>();
    public BoundsInt boardBounds;
    private Tilemap _tilemap;

    public static Board Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No Board Instance");
        
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        InitWalls();
        //DebugWallsOfTile(tiles[8]);
    }

    private void InitWalls()
    {
        foreach (var t in tiles)
            walls.Add(t, new List<Vector2Int>());

        int i = 0;
        walls[tiles[i++]].Add(Vector2Int.up);
        walls[tiles[i++]].Add(Vector2Int.right);
        walls[tiles[i++]].Add(Vector2Int.down);
        walls[tiles[i++]].Add(Vector2Int.left);

        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.up, Vector2Int.left });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.up, Vector2Int.right });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.down, Vector2Int.right });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.down, Vector2Int.left });

        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.up, Vector2Int.left, Vector2Int.down });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.up, Vector2Int.left, Vector2Int.right });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.down, Vector2Int.right, Vector2Int.up });
        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.down, Vector2Int.left, Vector2Int.right });

        walls[tiles[i++]].AddRange(new List<Vector2Int>() { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up });
    }

    private void DebugWallsOfTile(TileBase t)
    {
        if (walls.ContainsKey(t))
            foreach (var w in walls[t])
                Debug.Log(w);
    }

}
