using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardCharacter : MonoBehaviour
{
    private Tilemap _tilemap;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

    void Start()
    {
        _tilemap = Board.Instance.GetComponent<Tilemap>();
    }

    public void SnapIntoTile()
    {
        transform.position = GetTarget(GetCurrentTile());
    }

    public Vector3Int GetCurrentTile()
    {
        return _tilemap.WorldToCell(transform.position);
    }

    public Vector3Int GetTargetTile(Vector3Int dir)
    {
        return GetCurrentTile() + dir;
    }

    public Vector3 GetTarget(Vector3Int dir)
    {
        return _tilemap.CellToWorld(GetTargetTile(dir)) + _offset;
    }
}
