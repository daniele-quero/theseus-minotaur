using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TheseusMovement : MonoBehaviour
{
    private Vector3Int _dir = Vector3Int.zero;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

    private CustomInput _input;
    private WallChecker _wallChecker;
    private BoardCharacter _boardChar;

    void Start()
    {
        _input = GetComponent<CustomInput>();
        _wallChecker = GetComponent<WallChecker>();
        _boardChar = GetComponent<BoardCharacter>();
        _boardChar.SnapIntoTile();
    }

    void Update()
    {
        if (!_boardChar.IsMoving)
        {
            if (_input.WaitInput())
                MinotaurTurn();
            else
                Move();
        }

        if(_input.RestartInput())
        {
            LevelManager.Instance.RestartLevel();
        }
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

        if (_dir != Vector3Int.zero 
            && _wallChecker.IsMovementAllowed(_boardChar.GetCurrentTile(), _boardChar.GetTargetTile(_dir), _dir))
        {
            _boardChar.Move(_boardChar.GetTarget(_dir), _dir);
            MinotaurTurn();
            _boardChar.IsMoving = false;
        }
    }

    private void MinotaurTurn()
    {
        MinotaurMovement.Instance.Move(_boardChar.GetTargetTile(_dir));
    }

}
