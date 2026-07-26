using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEventWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text descriptionText;
    [SerializeField] private Button choice1Button;
    [SerializeField] private TMPro.TMP_Text choice1ButtonText;
    [SerializeField] private Button choice2Button;
    [SerializeField] private TMPro.TMP_Text choice2ButtonText;

    private WorldZone zone;
    private Window windowScript;



    public void DisplayChoice(ChoiceEvent _choiceEvent, WorldZone _zone)
    {
        zone = _zone;

        descriptionText.text = _choiceEvent.description;
        choice1ButtonText.text = _choiceEvent.choices[0].label;
        choice2ButtonText.text = _choiceEvent.choices[1].label;

        choice1Button.onClick.AddListener(() => { MakeChoice(_choiceEvent.choices[0]); });
        choice2Button.onClick.AddListener(() => { MakeChoice(_choiceEvent.choices[1]); });
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }

    private void MakeChoice(ChoiceEvent.Choice _choice)
    {
        _choice.reward.RedeemReward(zone);

        if (_choice.replacesGeneratedUnit)
        {
            zone.generatedUnit = _choice.replacementUnit;
        }

        windowScript.CloseWindow();
    }
}
