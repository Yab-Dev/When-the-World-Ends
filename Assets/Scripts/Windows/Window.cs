using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text windowTitle;
    [SerializeField] private RectTransform windowContent;



    public void SetWindowTitle(string _title)
    {
        windowTitle.text = _title;
    }

    public void SetWindowContent(GameObject _content)
    {
        _content.transform.SetParent(windowContent);
        IWindowInteract windowInteract = _content.GetComponent<IWindowInteract>();
        if (windowInteract != null)
        {
            windowInteract.SetWindowScript(this);
        }
    }

    public GameObject GetWindowContent()
    {
        return windowContent.GetChild(0).gameObject;
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}
