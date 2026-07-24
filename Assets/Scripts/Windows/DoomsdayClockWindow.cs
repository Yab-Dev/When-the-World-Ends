using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomsdayClockWindow : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text clockText;

    private void Update()
    {
        float timeInSeconds = GameManager.Instance.doomsdayTimer;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        float minutes = timeSpan.Minutes;
        float seconds = timeSpan.Seconds;
        float milliseconds = timeSpan.Milliseconds;

        clockText.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}.{milliseconds.ToString("000")}";
    }
}
