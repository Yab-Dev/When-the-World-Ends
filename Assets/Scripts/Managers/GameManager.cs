using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject testWindowContent;
    [SerializeField] private GameObject battleWindowContent;

    private void Start()
    {
        WindowManager.Instance.CreateWindow("When the World Ends", testWindowContent, Vector2.zero);
        WindowManager.Instance.CreateWindow("Battle Network", battleWindowContent, new Vector2(200, 0));
    }
}
