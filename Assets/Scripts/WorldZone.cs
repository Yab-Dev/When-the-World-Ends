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



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(zoneName);
    }
}
