using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private GameBattle gameBattle;
    [SerializeField] private Image enemyIcon;
    [SerializeField] private StatBar enemyHealthBar;
    [SerializeField] private StatBar enemyAttackTimerBar;
    [SerializeField] private RectTransform battleUnitContent;
    [SerializeField] private RectTransform stratagemButtonContent;
    [SerializeField] private GameObject battleOverPopup;
    [SerializeField] private TMPro.TMP_Text battleOverText;
    [SerializeField] private Button battleOverButton;

    [Header("Prefabs")]
    [SerializeField] private GameObject battleUnitDisplayObject;
    [SerializeField] private GameObject stratagemButtonObject;



    private void Awake()
    {
        gameBattle.OnGameBattleStart += InitializeUI;
    }

    private void InitializeUI()
    {
        enemyIcon.sprite = gameBattle.enemyUnit.baseUnit.icon;
        gameBattle.enemyUnit.OnBattleUnitHealthChange += SetEnemyHealthBar;
        gameBattle.enemyUnit.OnBattleUnitAttackTimerChange += SetEnemyAttackTimerBar;

        foreach (BattleUnit playerUnit in gameBattle.playerUnits)
        {
            GameObject battleUnitDisplay = Instantiate(battleUnitDisplayObject, battleUnitContent);
            BattleUnitDisplayUI battleUnitDisplayUI = battleUnitDisplay.GetComponent<BattleUnitDisplayUI>();

            battleUnitDisplayUI.SetIcon(enemyIcon);
            playerUnit.OnBattleUnitHealthChange += battleUnitDisplayUI.SetHealthBar;
            playerUnit.OnBattleUnitAttackTimerChange += battleUnitDisplayUI.SetAttackTimerBar;

            GameObject stratagemButton = Instantiate(stratagemButtonObject, stratagemButtonContent);
        }

        battleOverPopup.SetActive(false);

        gameBattle.OnGameBattleWin += ShowWinPopup;
        gameBattle.OnGameBattleLose += ShowLosePopup;
        battleOverButton.onClick.AddListener(() => { gameBattle.windowScript.CloseWindow(); });
    }

    private void SetEnemyHealthBar(float _value, float _maxValue)
    {
        enemyHealthBar.SetStatBar(_value / _maxValue);
    }

    private void SetEnemyAttackTimerBar(float _value, float _maxValue)
    {
        enemyAttackTimerBar.SetStatBar(1.0f - (_value / _maxValue));
    }

    private void ShowWinPopup()
    {
        battleOverPopup.SetActive(true);
        battleOverText.text = "Battle Won";
    }

    private void ShowLosePopup()
    {
        battleOverPopup.SetActive(true);
        battleOverText.text = "Battle Lost";
    }
}
