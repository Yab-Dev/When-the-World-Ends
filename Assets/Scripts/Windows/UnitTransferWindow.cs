using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTransferWindow : MonoBehaviour, IWindowInteract
{
    [Header("Cache")]
    [SerializeField] private Image loadingBarFill;

    private Window windowScript;
    private float totalLoadingTime;
    private float loadingTime;
    private List<Unit> transferringUnits = new List<Unit>();
    private WorldZone destination;
    private bool isLoading;

    public static event TutorialManager.TutorialNotify OnTroopTransferComplete;



    public void BeginLoadingBar(List<Unit> _transferringUnits, WorldZone _destination, float _timePerUnit)
    {
        transferringUnits = _transferringUnits;
        destination = _destination;
        totalLoadingTime = _timePerUnit * _transferringUnits.Count;
        loadingTime = 0;

        isLoading = true;
    }

    private void Update()
    {
        if (isLoading)
        {
            loadingTime += Time.deltaTime;
            loadingBarFill.fillAmount = loadingTime / totalLoadingTime;
            if (loadingTime >= totalLoadingTime)
            {
                foreach (Unit unit in transferringUnits)
                {
                    destination.AddUnit(unit);
                }
                OnTroopTransferComplete?.Invoke();
                windowScript.CloseWindow();
            }
        }
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }
}
