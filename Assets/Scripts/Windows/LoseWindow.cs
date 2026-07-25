using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button endGameButton;

    private Window windowScript;



    private void Start()
    {
        float timeInSeconds = GameManager.Instance.survivedTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        float minutes = timeSpan.Minutes;
        float seconds = timeSpan.Seconds;
        float milliseconds = timeSpan.Milliseconds;

        scoreText.text = $"You survived for: {minutes.ToString("00")}:{seconds.ToString("00")}.{milliseconds.ToString("000")}";

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
