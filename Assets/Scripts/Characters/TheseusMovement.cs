using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TheseusMovement : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;

    private Vector3Int _dir = Vector3Int.zero;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

    private CustomInput _input;

    [SerializeField]
    private float _speed;

    void Start()
    {
        _input = GetComponent<CustomInput>();
    }

    void Update()
    {
        Move();
    }

    private Vector3Int GetCurrentTile()
    {
        return _tilemap.WorldToCell(transform.position);
    }

    private Vector3Int GetTargetTile(Vector3Int dir)
    {
        return GetCurrentTile() + dir;
    }

    private Vector3 GetTarget(Vector3Int dir)
    {
        return _tilemap.CellToWorld(GetTargetTile(_dir)) + _offset;
    }


    private void Move()
    {
        int x = _input.HorizontalInput();
        int y = _input.VerticalInput();

        if (x != 0)
            _dir = Vector3Int.right * x;
        else if (y != 0)
            _dir = Vector3Int.up * y;
        else
            _dir = Vector3Int.zero;

        transform.position = GetTarget(_dir);
    }

    //TODO: use coroutine for a smooth movement
}
