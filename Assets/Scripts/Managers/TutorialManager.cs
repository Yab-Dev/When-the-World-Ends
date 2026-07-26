using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("Data")]
    [SerializeField] private RadioEvent introRadioEvent;
    [SerializeField] private RadioEvent battleScreenOverviewRadioEvent;
    [SerializeField] private RadioEvent worldScreenOverviewRadioEvent;
    [SerializeField] private RadioEvent battlePreambleRadioEvent;
    [SerializeField] private RadioEvent battleRadioEvent;
    [SerializeField] private RadioEvent outroRadioEvent;

    private RadioEventWindow currentWindow;

    public delegate void TutorialNotify();



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    public void StartTutorial()
    {
        RadioEventWindow window = introRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += WaitForBattleEventClick;
        currentWindow.OnSkipButtonPressed += SkipTutorial;
    }

    public void SkipTutorial()
    {
        currentWindow.OnRadioEventComplete -= WaitForBattleEventClick;
        MapBattleEncounter.OnEncounterClicked -= BattleScreenOverviewTutorial;
        currentWindow.CloseWindow();
        GameManager.Instance.TutorialOver();
    }

    private void WaitForBattleEventClick()
    {
        currentWindow.OnRadioEventComplete -= WaitForBattleEventClick;
        MapBattleEncounter.OnEncounterClicked += BattleScreenOverviewTutorial;
    }

    private void BattleScreenOverviewTutorial()
    {
        MapBattleEncounter.OnEncounterClicked -= BattleScreenOverviewTutorial;
        currentWindow.OnSkipButtonPressed -= SkipTutorial;
        currentWindow.CloseWindow();

        RadioEventWindow window = battleScreenOverviewRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += WaitForMapZoneClick;
    }

    private void WaitForMapZoneClick()
    {
        currentWindow.OnRadioEventComplete -= WaitForMapZoneClick;
        WorldZone.OnWorldZoneClicked += WorldScreenOverviewTutorial;
    }

    private void WorldScreenOverviewTutorial()
    {
        WorldZone.OnWorldZoneClicked -= WorldScreenOverviewTutorial;
        currentWindow.CloseWindow();

        RadioEventWindow window = worldScreenOverviewRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += WaitForUnitTransfer;
    }

    private void WaitForUnitTransfer()
    {
        currentWindow.OnRadioEventComplete -= WaitForUnitTransfer;
        UnitTransferWindow.OnTroopTransferComplete += BattlePreambleTutorial;
    }

    private void BattlePreambleTutorial()
    {
        UnitTransferWindow.OnTroopTransferComplete -= BattlePreambleTutorial;
        currentWindow.CloseWindow();

        RadioEventWindow window = battlePreambleRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += WaitForBattleStart;
    }

    private void WaitForBattleStart()
    {
        currentWindow.OnRadioEventComplete -= WaitForBattleStart;
        GameBattle.OnBattleStarted += BattleTutorial;
    }

    private void BattleTutorial()
    {
        GameBattle.OnBattleStarted -= BattleTutorial;
        currentWindow.CloseWindow();

        RadioEventWindow window = battleRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += WaitForBattleEnd;
    }

    private void WaitForBattleEnd()
    {
        currentWindow.OnRadioEventComplete -= WaitForBattleEnd;
        GameBattle.OnBattleEnded += TutorialOutro;
    }

    private void TutorialOutro()
    {
        GameBattle.OnBattleEnded -= TutorialOutro;
        currentWindow.CloseWindow();

        RadioEventWindow window = outroRadioEvent.StartRadio();
        currentWindow = window;
        currentWindow.OnRadioEventComplete += TutorialOver;
    }

    private void TutorialOver()
    {
        GameManager.Instance.TutorialOver();
    }
}
