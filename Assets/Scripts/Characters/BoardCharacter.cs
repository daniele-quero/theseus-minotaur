using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardCharacter : MonoBehaviour
{
    
    private Tilemap _tilemap;
    private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
    private bool _isMoving = false;
    private WaitForSeconds _wait;

    [SerializeField]
    private float _speed;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    void Start()
    {
        _tilemap = Board.Instance.GetComponent<Tilemap>();
        _wait = new WaitForSeconds(0.01f);
    }

    public void SnapIntoTile()
    {
        transform.position = GetTarget(Vector3Int.zero);
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

    public void Move(Vector3 target, Vector3Int dir)
    {
        StartCoroutine(MoveCoroutine(target, dir));
    }
    private IEnumerator MoveCoroutine(Vector3 target, Vector3Int dir)
    {
        _isMoving = true;
        
        while(transform.position != target)
        {
            transform.Translate((Vector3)dir * 0.01f * _speed);
            yield return _wait;
        }

        if (LevelManager.Instance.isVictory())
            Debug.Log("Victory");
        
        else if (LevelManager.Instance.isGameOver())
            Debug.Log("Game Over");

    }
}
