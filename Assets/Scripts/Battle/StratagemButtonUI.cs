using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StratagemButtonUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Button button;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image stratagemIcon;

    private bool isInactive;


    public void SetIcon(Sprite _icon)
    {
        stratagemIcon.sprite = _icon;
    }

    public void SetButtonActive(bool _isActive)
    {
        if (isInactive) { button.interactable = false; return; }

        button.interactable = _isActive;
    }

    public void DeactivateButton()
    {
        isInactive = true;
        SetButtonActive(false);
    }

    public void SetButtonListener(UnityEngine.Events.UnityAction _call)
    {
        button.onClick.AddListener(_call);
    }

    public void SetButtonFillLevel(float _value, float _maxValue)
    {
        fillImage.fillAmount = _value / _maxValue;
        SetButtonActive(_value == _maxValue);
    }
}
