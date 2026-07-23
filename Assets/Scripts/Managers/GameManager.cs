using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject testWindowContent;

    private void Start()
    {
        WindowManager.Instance.CreateWindow("When the World Ends", testWindowContent, Vector2.zero);
    }
}
