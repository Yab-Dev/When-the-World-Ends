using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldZone : MonoBehaviour, IPointerDownHandler
{
    [Header("Zone Data (DESIGNER ENTER DATA HERE)")]
    public string zoneName;
    public List<WorldZone> connectedZones = new List<WorldZone>();
    public Unit generatedUnit;
    public float generationLength;

    [Header("Zone Data")]
    public List<Unit> stationedUnits = new List<Unit>();
    public int influence;

    [Header("Prefabs")]
    [SerializeField] private GameObject worldZoneInfoWindow;


    public void OnPointerDown(PointerEventData eventData)
    {
        Window window = WindowManager.Instance.CreateWindow(zoneName, worldZoneInfoWindow, new Vector2(-130, -50));
        window.GetWindowContent().GetComponent<WorldZoneInfoWindow>().InitializeUI(this);
    }

    public void StartGame()
    {
        stationedUnits.Add(generatedUnit);
    }
}
