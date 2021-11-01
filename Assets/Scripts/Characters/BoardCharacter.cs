using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardCharacter : MonoBehaviour
{
    
    private Tilemap _tilemap;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
    private bool _isMoving = false;
    private Vector3Int _previousPosition;
    private WaitForSeconds _wait;

    [SerializeField]
    private float _speed;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    public Vector3Int PreviousPosition { get => _previousPosition; set => _previousPosition = value; }

    void Start()
    {
        _tilemap = Board.Instance.GetComponent<Tilemap>();
        _wait = new WaitForSeconds(0.02f);
        _previousPosition = GetCurrentTile();
    }

    public void SnapIntoTile()
    {
        transform.position = GetTarget(Vector3Int.zero);
    }

    public void SnapIntoTile(Vector3Int tile)
    {
        transform.position = _tilemap.CellToWorld(tile) + _offset;
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

    public void StopMovement()
    {
        _speed = 0;
    }

    public void Move(Vector3 target, Vector3Int dir)
    {
        StartCoroutine(MoveCoroutine(target, dir));
    }

    private IEnumerator MoveCoroutine(Vector3 target, Vector3Int dir)
    {
        _isMoving = true;
     
        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return _wait;
        }

        _isMoving = false;

        if (LevelManager.Instance.IsVictory())
            LevelManager.Instance.SetEndLevel(true);

        else if (LevelManager.Instance.IsGameOver())
            LevelManager.Instance.SetEndLevel(false);

    }
}
