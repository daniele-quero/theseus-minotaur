using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallChecker : MonoBehaviour
{
    private Tilemap _tilemap;
    private void Start()
    {
        _tilemap = Board.Instance.GetComponent<Tilemap>();    
    }

    public bool IsMovementAllowed(Vector3Int current, Vector3Int target, Vector3Int dir)
    {
        foreach(var w in Board.Instance.walls[_tilemap.GetTile(current)])
        {
            //wall in current tile, in the same direction as the movement
            if (dir == (Vector3Int)w)
                return false;
        }

        foreach (var w in Board.Instance.walls[_tilemap.GetTile(target)])
        {
            //wall in target tile, in the opposite direction as the movement
            if (dir == -(Vector3Int)w)
                return false;
        }

        return true;
    }
}
