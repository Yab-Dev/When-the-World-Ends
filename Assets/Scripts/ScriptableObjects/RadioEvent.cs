using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/RadioEvent")]
public class RadioEvent : Event
{
    [System.Serializable]
    public class TextBox
    {
        [TextArea]
        public string message;
        public string buttonText;
    }

    [Header("Data")]
    public bool hasSkipButton;
    public List<TextBox> textBoxes = new List<TextBox>();



    public RadioEventWindow StartRadio()
    {
        Window window = WindowManager.Instance.CreateRadioWindow("Radio Transmission", new Vector2(0, -80), this);

        return window.GetWindowContent().GetComponent<RadioEventWindow>();
    }
}
