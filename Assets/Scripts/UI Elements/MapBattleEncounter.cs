using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapBattleEncounter : MonoBehaviour, IPointerDownHandler
{
    private WorldZone zone;
    private BattleEvent battleEvent;



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Zone: {zone.name} Enemy: {battleEvent.enemyUnit.name}");
    }
}
