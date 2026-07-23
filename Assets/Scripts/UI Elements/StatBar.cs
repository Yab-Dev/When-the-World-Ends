using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Image statBar;



    public void SetStatBar(float _level)
    {
        statBar.fillAmount = _level;
    }
}
