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
    private float generationTimer;

    [Header("Zone Data")]
    private List<Unit> stationedUnits = new List<Unit>();
    private List<Unit> StationedUnits
    {
        get
        {
            return stationedUnits;
        }
        set
        {
            stationedUnits = value;
            OnWorldZoneUnitUpdate?.Invoke(stationedUnits);
        }
    }
    private int influence;
    public int Influence
    {
        get
        {
            return influence;
        }
        set
        {
            influence = value;
            OnWorldZoneInfluenceUpdate?.Invoke(influence);
        }
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject worldZoneInfoWindow;

    private bool gameStarted;



    public delegate void WorldZoneUnitUpdate(List<Unit> _stationedUnits);
    public event WorldZoneUnitUpdate OnWorldZoneUnitUpdate;

    public delegate void WorldZoneStatUpdate(int _stat);
    public event WorldZoneStatUpdate OnWorldZoneInfluenceUpdate;

    public delegate void WorldZoneUnitLoadUpdate(float _value, float _maxValue);
    public event WorldZoneUnitLoadUpdate OnWorldZoneUnitLoadUpdate;



    private void Awake()
    {
        StationedUnits = new List<Unit>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Window window = WindowManager.Instance.CreateWindow(zoneName, worldZoneInfoWindow, new Vector2(-130, -30));
        window.GetWindowContent().GetComponent<WorldZoneInfoWindow>().InitializeUI(this);
    }

    public void StartGame()
    {
        StationedUnits.Add(generatedUnit);
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            generationTimer += Time.deltaTime;
            if (generationTimer >= generationLength)
            {
                StationedUnits.Add(generatedUnit);
                generationTimer = 0;
            }
            OnWorldZoneUnitLoadUpdate?.Invoke(generationTimer, generationLength);
        }
    }

    public void AddUnit(Unit _unit)
    {
        StationedUnits.Add(_unit);
        OnWorldZoneUnitUpdate?.Invoke(stationedUnits);
    }

    public void RemoveUnit(Unit _unit)
    {
        StationedUnits.Remove(_unit);
        OnWorldZoneUnitUpdate?.Invoke(stationedUnits);
    }

    public List<Unit> GetStationedUnits()
    {
        return StationedUnits;
    }
}
