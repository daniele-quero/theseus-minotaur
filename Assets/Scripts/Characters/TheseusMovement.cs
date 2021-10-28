using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TheseusMovement : MonoBehaviour
{
    private Tilemap _tilemap;

    private Vector3Int _dir = Vector3Int.zero;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

    private CustomInput _input;
    private WallChecker _wallChecker;
    private BoardCharacter _boardChar;

    [SerializeField]
    private float _speed;

    void Start()
    {
        _tilemap = Board.Instance.GetComponent<Tilemap>();
        _input = GetComponent<CustomInput>();
        _wallChecker = GetComponent<WallChecker>();
        _boardChar = GetComponent<BoardCharacter>();
        _boardChar.SnapIntoTile();
    }

    void Update()
    {
        Move();
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

        if (_wallChecker.IsMovementAllowed(_boardChar.GetCurrentTile(), _boardChar.GetTargetTile(_dir), _dir))
            transform.position = _boardChar.GetTarget(_dir);
    }

    //TODO: use coroutine for a smooth movement
}
