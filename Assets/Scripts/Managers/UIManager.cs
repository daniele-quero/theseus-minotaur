using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    private Text _endLevelText, _endLevelPrompt;
    private string _prompt = "Press - to Proceed to the Next Level";

    public static UIManager Instance
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
    private void Start()
    {
        SetEndLevel("", "");
    }

    public void SetEndLevel(string main, string key)
    {
        _endLevelText.text = main;
        if (string.IsNullOrEmpty(key))
            _endLevelPrompt.text = "";
        else
            _endLevelPrompt.text = _prompt.Replace("-", key);
    }
}
