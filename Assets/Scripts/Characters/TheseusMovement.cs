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
        if (!_boardChar.IsMoving && !MinotaurMovement.Instance.BoardChar.IsMoving)
        {
            if (_input.WaitInput())
                MinotaurTurn();
            else if (LevelManager.Instance.CanPlay)
                Move();
            else if (_input.NextLevelInput())
                LevelManager.Instance.NextLevel();
        }

        if (_input.RestartInput())
        {
            LevelManager.Instance.RestartLevel();
        }
        else if (_input.UndoInput())
        {
            UndoMove();
        }
    }

    private void Move()
    {
        int x = 0;
        int y = 0;

        if ((x = _input.HorizontalInput()) != 0)
            _dir = Vector3Int.right * x;
        else if ((y = _input.VerticalInput()) != 0)
            _dir = Vector3Int.up * y;
        else
            _dir = Vector3Int.zero;

        if (_dir != Vector3Int.zero
            && _wallChecker.IsMovementAllowed(_boardChar.GetCurrentTile(), _boardChar.GetTargetTile(_dir), _dir))
        {
            _boardChar.PreviousPosition = _boardChar.GetCurrentTile();
            _boardChar.Move(_boardChar.GetTarget(_dir), _dir);
            MinotaurTurn();
        }
    }

    private void MinotaurTurn()
    {
        MinotaurMovement.Instance.BoardChar.PreviousPosition = MinotaurMovement.Instance.BoardChar.GetCurrentTile();
        MinotaurMovement.Instance.Move(_boardChar.GetTargetTile(_dir));
    }

    private void UndoMove()
    {
        _boardChar.SnapIntoTile(_boardChar.PreviousPosition);
        MinotaurMovement.Instance.BoardChar.SnapIntoTile(MinotaurMovement.Instance.BoardChar.PreviousPosition);
    }

}
