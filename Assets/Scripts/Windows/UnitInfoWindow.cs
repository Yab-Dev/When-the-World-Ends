using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private Image unitIcon;
    [SerializeField] private TMPro.TMP_Text unitNameText;
    [SerializeField] private TMPro.TMP_Text isFlyingText;
    [SerializeField] private TMPro.TMP_Text targetsFlyingText;
    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text damageText;
    [SerializeField] private TMPro.TMP_Text speedText;
    [SerializeField] private Image stratagemIcon;
    [SerializeField] private TMPro.TMP_Text stratagemName;
    [SerializeField] private TMPro.TMP_Text stratagemDescription;
    [SerializeField] private Button exitButton;

    private Window windowScript;



    public void SetInfo(Unit _unit)
    {
        unitIcon.sprite = _unit.icon;
        unitNameText.text = _unit.name;
        isFlyingText.text = _unit.isFlyingUnit ? "Flying Unit" : "Grounded Unit";
        targetsFlyingText.text = _unit.targetsFlyingUnits ? "Focuses Flying" : "Focuses Grounded";
        healthText.text = $"HP:{_unit.health}";
        damageText.text = $"AT:{_unit.damage}";
        speedText.text = $"SP:{_unit.speed}";

        stratagemIcon.sprite = _unit.stratagem.icon;
        stratagemName.text = _unit.stratagem.name;
        stratagemDescription.text = _unit.stratagem.description;

        exitButton.onClick.AddListener(windowScript.CloseWindow);
    }

    public void SetWindowScript(Window _windowScript)
    {
       windowScript = _windowScript;
    }
}
