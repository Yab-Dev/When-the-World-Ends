using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldZoneInfoWindow : MonoBehaviour, IWindowInteract
{
    public static WorldZoneInfoWindow Instance;

    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text influenceText;
    [SerializeField] private UnitSelectorUI unitSelector;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMPro.TMP_Dropdown unitDestinationDropdown;
    [SerializeField] private Button moveUnitsButton;

    private Window windowScript;
    private WorldZone zone;



    private void Awake()
    {
        if (Instance != null)
        {
            Instance.windowScript.CloseWindow();
        }

        Instance = this;
    }

    public void InitializeUI(WorldZone _zone)
    {
        zone = _zone;

        influenceText.text = $"Influence: {_zone.influence}";

        unitSelector.InitializeUnitSelector(_zone.stationedUnits, 8);

        exitButton.onClick.AddListener(windowScript.CloseWindow);

        unitDestinationDropdown.ClearOptions();
        List<string> connectedZones = new List<string>();
        foreach (WorldZone worldZone in _zone.connectedZones)
        {
            connectedZones.Add(worldZone.name);
        }
        unitDestinationDropdown.AddOptions(connectedZones);

        moveUnitsButton.onClick.AddListener(MoveUnits);
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }

    private void MoveUnits()
    {
        if (unitSelector.GetSelectedUnits().Count == 0) { return; }

        WorldZone destinationZone = null;
        foreach (WorldZone worldZone in zone.connectedZones)
        {
            if (worldZone.name == unitDestinationDropdown.options[unitDestinationDropdown.value].text)
            {
                destinationZone = worldZone; break;
            }
        }
        if (destinationZone == null) { return; }

        destinationZone.stationedUnits.AddRange(unitSelector.GetSelectedUnits());
        foreach (Unit unit in unitSelector.GetSelectedUnits())
        {
            zone.stationedUnits.Remove(unit);
        }

        windowScript.CloseWindow();
    }
}
