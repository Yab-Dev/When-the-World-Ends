using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsWindowUI : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text timeRewardText;
    [SerializeField] private TMPro.TMP_Text moraleRewardText;
    [SerializeField] private TMPro.TMP_Text influenceRewardText;
    [SerializeField] private RectTransform unitRewardsContent;
    [SerializeField] private Button confirmButton;

    [Header("Prefab")]
    [SerializeField] private GameObject unitRewardObject;

    private Window windowScript;



    public void DisplayReward(EventReward _reward)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_reward.timeReward);

        float minutes = timeSpan.Minutes;
        float seconds = timeSpan.Seconds;
        timeRewardText.text = $"Time: {minutes.ToString("00")}:{seconds.ToString("00")}";
        moraleRewardText.text = $"Morale: {_reward.moraleReward * 100.0f}%";
        influenceRewardText.text = $"Influence: {_reward.influenceReward}";

        foreach (Unit unit in _reward.unitReward)
        {
            GameObject unitReward = Instantiate(unitRewardObject, unitRewardsContent);
            unitReward.GetComponent<UnitRewardUI>().SetIcon(unit.icon);
        }
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
        confirmButton.onClick.AddListener(windowScript.CloseWindow);
    }
}
