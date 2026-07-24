using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldZone : MonoBehaviour
{
    [Header("Zone Data (DESIGNER ENTER DATA HERE)")]
    public string zoneName;
    public List<WorldZone> connectedZones = new List<WorldZone>();
    public Unit generatedUnit;
    public float generationLength;

    [Header("Zone Data")]
    public List<Unit> stationedUnits = new List<Unit>();
    public int influence;
}
