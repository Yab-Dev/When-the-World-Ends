using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitRewardUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Image unitIcon;



    public void SetIcon(Sprite _icon)
    {
        unitIcon.sprite = _icon;
    }
}
