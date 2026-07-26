using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/ChoiceEvent")]
public class ChoiceEvent : Event
{
    [System.Serializable]
    public class Choice
    {
        [Header("General Data")]
        public string label;
        public EventReward reward;

        [Header("Event Specific Data")]
        public bool replacesGeneratedUnit;
        public Unit replacementUnit;
    }

    [Header("Data")]
    public bool isUnique;
    [TextArea]
    public string description;
    public List<Choice> choices = new List<Choice>();
    public List<string> blackListZones = new List<string>();



    public void DisplayEvent(WorldZone _zone)
    {
        WindowManager.Instance.CreateChoiceEventWindow($"Choice: {_zone.name}", new Vector2(-100, 0), this, _zone);
    }
}
