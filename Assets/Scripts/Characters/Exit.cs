using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private static Exit _instance;
    private BoardCharacter _boardChar;

    public static Exit Instance
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

    void Start()
    {
        _boardChar = GetComponent<BoardCharacter>();
        _boardChar.SnapIntoTile();
    }
}
