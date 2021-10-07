using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private Resources resources;

    [SerializeField] private GameObject enemyInvasionTimerLabel;
    private Image enemyInavsionTimerImage;
    private Text enemyInavsionTimerText;

    [SerializeField] private float enemyInvasionCycleLength = 10;
    [SerializeField] private int initialEnemyInvasionDelayinCycles = 3; //кол-во циклов игры до волны врагов
    [SerializeField] private int enemyInvasionDelayinCycles; //кол-во циклов игры до волны врагов
    [SerializeField] private int initialAmountOfEnemies = 2; //кол-во врагов в первой волне
    private int amountOfEnemiesInNextWave;

    private int amountofPassedInvasionCycles = 0;
    private float currentTimeOfInvasionCycle = 0;
    private int currentAmountOfCycles = 0;

    void Start()
    {
        FetchTimerImageAndText();

        enemyInvasionDelayinCycles = initialEnemyInvasionDelayinCycles;
        amountOfEnemiesInNextWave = initialAmountOfEnemies;
        resources.EnemiesAmount = amountOfEnemiesInNextWave;
    }


    void Update()
    {
        CountAndUpdateInvasionCycleTime();
    }

    public void GameRestart()
    {
        currentTimeOfInvasionCycle = 0;
        currentAmountOfCycles = 0;
        amountofPassedInvasionCycles = 0;
        enemyInvasionDelayinCycles = initialEnemyInvasionDelayinCycles;
    }

    void CountAndUpdateInvasionCycleTime()
    {
        currentTimeOfInvasionCycle += Time.deltaTime;
        float timeTillInvasion = enemyInvasionCycleLength * enemyInvasionDelayinCycles - currentTimeOfInvasionCycle;

        if (timeTillInvasion <= 0)
        {
            enemyInvasionDelayinCycles = 1;
            currentTimeOfInvasionCycle = 0;
            StartInvasion();
        }
        else
        {
            enemyInavsionTimerText.text = System.Math.Round(timeTillInvasion, 1).ToString();
        }

        enemyInavsionTimerImage.fillAmount = currentTimeOfInvasionCycle / (enemyInvasionDelayinCycles * enemyInvasionCycleLength);
    }


    void FetchTimerImageAndText()
    {
        //
        enemyInavsionTimerText = enemyInvasionTimerLabel.GetComponentInChildren<Text>();
        enemyInavsionTimerText.text = $"{enemyInvasionDelayinCycles  * enemyInvasionCycleLength}";

        //
        foreach (Image FetchedImage in enemyInvasionTimerLabel.GetComponentsInChildren<Image>())
        {
            if (FetchedImage.name == "EnemyInavsionTimerImage") enemyInavsionTimerImage = FetchedImage;
        }
        enemyInavsionTimerImage.fillAmount = currentTimeOfInvasionCycle / (enemyInvasionDelayinCycles * enemyInvasionCycleLength);
    }

    void StartInvasion()
    {
        this.GetComponent<AudioSource>().Play();

        if (resources.WarriorsAmount >= amountOfEnemiesInNextWave)
        {
            resources.WarriorsAmount -= amountOfEnemiesInNextWave;
            amountofPassedInvasionCycles++;
            resources.AmountOfInvasionCycles = amountofPassedInvasionCycles;
        }
        else
        {
            gameParameters.FinishGame(false);
        }
    }
}
