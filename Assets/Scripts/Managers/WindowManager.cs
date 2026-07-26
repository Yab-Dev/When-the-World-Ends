using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject windowObject;
    [SerializeField] private GameObject rewardsWindow;
    [SerializeField] private GameObject radioWindow;

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

    public Window CreateRewardsWindow(string _windowName, Vector2 _position, EventReward _reward)
    {
        Window newWindow = CreateWindow(_windowName, rewardsWindow, _position);

        newWindow.GetWindowContent().GetComponent<RewardsWindowUI>().DisplayReward(_reward);

        return newWindow;
    }

    public Window CreateRadioWindow(string _windowName, Vector2 _position, RadioEvent _radioEvent)
    {
        Window newWindow = CreateWindow(_windowName, radioWindow, _position);

        newWindow.GetWindowContent().GetComponent<RadioEventWindow>().StartRadio(_radioEvent);

        return newWindow;
    }
}
