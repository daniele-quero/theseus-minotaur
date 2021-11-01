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

    private bool _canPlay = true;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No LevelManager Instance");

            return _instance;
        }
    }

    public bool CanPlay { get => _canPlay; }

    private void Awake()
    {
        _instance = this;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        if (current + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(current + 1);
    }

    public bool IsVictory()
    {
        return _theseusChar.GetCurrentTile() == Exit.Instance.BoardChar.GetCurrentTile();
    }

    public bool IsGameOver()
    {
        return _theseusChar.GetCurrentTile() == MinotaurMovement.Instance.BoardChar.GetCurrentTile();
    }

    public void SetEndLevel(bool victory)
    {
        MinotaurMovement.Instance.BoardChar.StopMovement();
        MinotaurMovement.Instance.BoardChar.IsMoving = false;

        _canPlay = false;
        string main = "GAME OVER";
        string key = "R";

        if (victory)
        {
            main = "VICTORY";
            key = "ENTER";
        }

        UIManager.Instance.SetEndLevel(main, key);
    }
}
