using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }
}
