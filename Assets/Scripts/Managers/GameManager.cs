using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay Data")]
    [SerializeField] public float doomsdayTimer;
    [SerializeField] public float survivedTime;
    [SerializeField] public float morale;
    [SerializeField] public List<WorldZone> zones = new List<WorldZone>();

    [Header("Cache")]
    [SerializeField] private GameObject border;
    [SerializeField] private GameObject worldMap;
    [SerializeField] private GameObject windowParent;

    [Header("Prefabs")]
    [SerializeField] private GameObject mainMenuWindowContent;
    [SerializeField] private GameObject battleWindowContent;
    [SerializeField] private GameObject doomsdayTimerWindowContent;
    [SerializeField] private GameObject playerStatsWindowContent;
    [SerializeField] private GameObject playerLoseAnimationWindow;
    [SerializeField] private GameObject playerLoseMenuWindow;

    private bool isGameStarted = false;
    Coroutine timerCoroutine = null;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    private void Start()
    {
        border.SetActive(false);
        worldMap.SetActive(false);

        StartCoroutine(IntroAnimation());
    }

    private void Update()
    {
        if (isGameStarted)
        {
            doomsdayTimer -= Time.deltaTime;
            survivedTime += Time.deltaTime;

            if (doomsdayTimer < 60.0f)
            {
                if (timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(TimerBeep());
                }
            }
            else
            {
                if (timerCoroutine != null)
                {
                    StopCoroutine(timerCoroutine);
                    timerCoroutine = null;
                }
            }

            if (doomsdayTimer < 0.0f)
            {
                doomsdayTimer = 0.0f;
                isGameStarted = false;
                StartCoroutine(Lose());
            }
        }
    }

    public void StartBattle(BattleEvent _battleEvent, List<Unit> _playerUnits, WorldZone _zone)
    {
        Window battleWindow = WindowManager.Instance.CreateWindow("Battle Network", battleWindowContent, new Vector2(0, 35));
        GameBattle gameBattle = battleWindow.GetWindowContent().GetComponent<GameBattle>();

        gameBattle.SetEnemyUnit(_battleEvent.enemyUnit);
        foreach (Unit _playerUnit in _playerUnits)
        {
            gameBattle.AddPlayerUnit(_playerUnit);
        }

        gameBattle.SetRewards(_battleEvent.winReward, _battleEvent.loseReward);
        gameBattle.SetZone(_zone);

        gameBattle.StartBattle();
    }

    public IEnumerator IntroAnimation()
    {
        yield return new WaitForSeconds(1.0f);

        border.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.computerStartupSound);
        yield return new WaitForSeconds(0.5f);

        SoundManager.Instance.StartAmbience();
        yield return new WaitForSeconds(0.5f);

        WindowManager.Instance.CreateWindow("When the World Ends", mainMenuWindowContent, Vector2.zero);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.0f);

        WindowManager.Instance.CreateWindow("Stats", playerStatsWindowContent, new Vector2(-170, 100));
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSound);
        yield return new WaitForSeconds(1.0f);

        isGameStarted = true;
        WindowManager.Instance.CreateWindow("Doomsday Clock", doomsdayTimerWindowContent, new Vector2(150, 80));
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSound);

        foreach (WorldZone zone in zones)
        {
            zone.StartGame();
        }
        BattleEncounterSpawnerManager.Instance.StartGame();

        yield return new WaitForSeconds(1.0f);

        worldMap.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSound);

        yield return new WaitForSeconds(2.0f);

        TutorialManager.Instance.StartTutorial();
    }

    public int GetTotalInfluence()
    {
        int total = 0;
        foreach (WorldZone zone in zones)
        {
            total += zone.Influence;
        }
        return total;
    }

    private IEnumerator TimerBeep()
    {
        while (doomsdayTimer < 60.0f)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSound);

            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator Lose()
    {
        StartCoroutine(LoseErrorSound());
        yield return new WaitForSeconds(5.0f);

        foreach (Transform transform in windowParent.transform)
        {
            Destroy(transform.gameObject);
        }
        worldMap.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        for (int i = 0; i < 100; i++)
        {
            WindowManager.Instance.CreateWindow("The End", playerLoseAnimationWindow, new Vector2(Random.Range(-225, 225), Random.Range(-125, 125)));
            SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSound);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);

        WindowManager.Instance.CreateWindow("When the World Ends", playerLoseMenuWindow, Vector2.zero);
        StopAllCoroutines();
    }

    private IEnumerator LoseErrorSound()
    {
        while (true)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.errorSound);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
