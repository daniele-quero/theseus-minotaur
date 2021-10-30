using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private WaitForSeconds _wait;

    [SerializeField]
    private BoardCharacter _theseusChar;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No LevelManager Instance");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool isVictory()
    {
        return _theseusChar.GetCurrentTile() == Exit.Instance.BoardChar.GetCurrentTile();                 
    }

    public bool isGameOver()
    {
        return _theseusChar.GetCurrentTile() == MinotaurMovement.Instance.BoardChar.GetCurrentTile();
    }
}
