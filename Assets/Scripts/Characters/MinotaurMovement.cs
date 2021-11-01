using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement : MonoBehaviour
{
    private int _moves = 2;
    private BoardCharacter _boardChar;
    private WallChecker _wallChecker;
    private static MinotaurMovement _instance;
    private WaitForSeconds _wait;

    public static MinotaurMovement Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No MinotaurMovement Instance");

            return _instance;
        }
    }

    public BoardCharacter BoardChar { get => _boardChar; set => _boardChar = value; }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _wallChecker = GetComponent<WallChecker>();
        _boardChar = GetComponent<BoardCharacter>();
        _boardChar.SnapIntoTile();
        _wait = new WaitForSeconds(0.5f);
    }

    //to be called every Theseus move
    public void Move(Vector3Int theseusTile)
    {
        _moves = 2;
        StartCoroutine(MoveRoutine(theseusTile));
    }

    private IEnumerator MoveRoutine(Vector3Int theseusTile)
    {
        yield return _wait;
        while (_moves > 0)
        {
            Vector3Int hDir = Vector3Int.zero,
                vDir = Vector3Int.zero;

            DirEvaluation(theseusTile, out hDir, out vDir);

            //check if horizontal move is possible (according to game rules)
            if (hDir != Vector3Int.zero && _wallChecker.IsMovementAllowed(_boardChar.GetCurrentTile(), _boardChar.GetTargetTile(hDir), hDir))
            {
                _boardChar.Move(_boardChar.GetTarget(hDir), hDir);
                _moves--;
                yield return _wait;
            }
            //if not, check for possible vertical move
            else if (vDir != Vector3Int.zero && _wallChecker.IsMovementAllowed(_boardChar.GetCurrentTile(), _boardChar.GetTargetTile(vDir), vDir))
            {
                _boardChar.Move(_boardChar.GetTarget(vDir), vDir);
                _moves--;
                yield return _wait;
            }
            //if not skip turn
            else
            {
                _moves = 0;
                yield return _wait;
            }

            _boardChar.IsMoving = false;
        }
    }

    private void DirEvaluation(Vector3Int theseusTile, out Vector3Int hDir, out Vector3Int vDir)
    {
        Vector3Int r = theseusTile - _boardChar.GetCurrentTile();

        if (r.x == 0)
            hDir = Vector3Int.zero;
        else
            hDir = Vector3Int.right * (int)Mathf.Sign(r.x);

        if (r.y == 0)
            vDir = Vector3Int.zero;
        else
            vDir = Vector3Int.up * (int)Mathf.Sign(r.y);

    }
}
