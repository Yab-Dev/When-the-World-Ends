using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsWIndowUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text moraleText;
    [SerializeField] private TMPro.TMP_Text totalInfluenceText;



    private void Update()
    {
        moraleText.text = $"Troop Morale: {Mathf.RoundToInt(GameManager.Instance.morale * 100)}%";
        totalInfluenceText.text = $"Total Influence: {GameManager.Instance.GetTotalInfluence()}";
    }
}
