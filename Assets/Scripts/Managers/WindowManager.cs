using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject windowObject;

    private RectTransform windowsParent;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }

        windowsParent = GameObject.FindGameObjectWithTag("WindowParent").GetComponent<RectTransform>();
    }

    public Window CreateWindow(string _windowName, GameObject _windowContent, Vector2 _position)
    {
        GameObject newWindow = Instantiate(windowObject, windowsParent);
        Window windowScript = newWindow.GetComponent<Window>();

        windowScript.SetWindowTitle(_windowName);
        windowScript.SetWindowContent(Instantiate(_windowContent, windowsParent));

        newWindow.transform.localPosition = _position;

        return windowScript;
    }
}
