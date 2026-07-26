using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private Button exitButton;

    private Window windowScript;



    private void Start()
    {
        exitButton.onClick.AddListener(() => { windowScript.CloseWindow(); });
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }
}
