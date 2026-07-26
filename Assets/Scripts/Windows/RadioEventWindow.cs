using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioEventWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text messageText;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMPro.TMP_Text nextButtonText;
    [SerializeField] private Button skipButton;
    [SerializeField] private AudioSource radioStaticSource;

    private int textBoxIndex;
    private Window windowScript;
    private RadioEvent radioEvent;

    public delegate void SkipButtonPressed();
    public event SkipButtonPressed OnSkipButtonPressed;

    public delegate void RadioEventComplete();
    public event RadioEventComplete OnRadioEventComplete;



    public void StartRadio(RadioEvent _radioEvent)
    {
        radioEvent = _radioEvent;

        textBoxIndex = 0;
        skipButton.gameObject.SetActive(_radioEvent.hasSkipButton);
        if (_radioEvent.hasSkipButton )
        {
            skipButton.onClick.AddListener(() => { OnSkipButtonPressed?.Invoke(); });
        }

        nextButton.onClick.RemoveAllListeners();

        StartCoroutine(DisplayTextBox(_radioEvent.textBoxes[textBoxIndex]));
    }

    private IEnumerator DisplayTextBox(RadioEvent.TextBox _textBox)
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.gameObject.SetActive(false);

        messageText.text = "";
        radioStaticSource.Play();

        foreach (char character in _textBox.message)
        {
            messageText.text += character;
            yield return new WaitForSeconds(0.025f);
        }

        if (_textBox.buttonText != "")
        {
            nextButton.gameObject.SetActive(true);
            nextButton.onClick.AddListener(NextTextBox);
            nextButtonText.text = _textBox.buttonText;
        }

        radioStaticSource.Stop();

        if (textBoxIndex == radioEvent.textBoxes.Count -1)
        {
            OnRadioEventComplete?.Invoke();
        }
    }

    private void NextTextBox()
    {
        textBoxIndex++;

        if (textBoxIndex >= radioEvent.textBoxes.Count)
        {
            windowScript.CloseWindow();
        }
        else
        {
            StartCoroutine(DisplayTextBox(radioEvent.textBoxes[textBoxIndex]));
        }
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }

    public void CloseWindow()
    {
        windowScript.CloseWindow();
    }
}
