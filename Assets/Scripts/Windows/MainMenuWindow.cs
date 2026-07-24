using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private Button beginGameButton;
    [SerializeField] private Button endGameButton;

    private Window windowScript;



    private void Start()
    {
        beginGameButton.onClick.AddListener(() => 
        { 
            GameManager.Instance.StartCoroutine(GameManager.Instance.StartGame()); 
            windowScript.CloseWindow();
        });
        endGameButton.onClick.AddListener(() => 
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }
}
