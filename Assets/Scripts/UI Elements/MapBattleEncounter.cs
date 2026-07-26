using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapBattleEncounter : MonoBehaviour, IPointerDownHandler
{
    public WorldZone zone;
    public BattleEvent battleEvent;

    [Header("Prefabs")]
    [SerializeField] private GameObject battleStartWindow;

    public static event TutorialManager.TutorialNotify OnEncounterClicked;



    public void OnPointerDown(PointerEventData eventData)
    {
        Window window = WindowManager.Instance.CreateWindow($"Target Found: {zone.name}", battleStartWindow, new Vector2(130, -30));
        BattleStartWindow battleWindow = window.GetWindowContent().GetComponent<BattleStartWindow>();
        battleWindow.InitializeUI(zone, battleEvent);
        battleWindow.OnBattleStart += SelfDestruct;
        OnEncounterClicked?.Invoke();
    }

    private void SelfDestruct()
    {
        try
        {
            Destroy(gameObject);
        }
        catch { }
    }
}
