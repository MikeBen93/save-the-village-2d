using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;

    private float wheatAmount;
    private int farmersAmount;
    private int warriorsAmount;
    private int enemiesAmount;
    private float totalWheatAmount = 0;
    private int totalWarriorsAmount = 0;
    private int totalEnemiesAmountt = 0;
    private int amountOfInvasionCycles = 0;

    [SerializeField] private float initialAmountofWheat;
    [SerializeField] private float wheatAmountToWin = 100;
    [SerializeField] private int farmersAmountToWin = 10;


    private void Start()
    {
        wheatAmount = initialAmountofWheat;
    }

    public float WheatAmount
    {
        set
        {
            wheatAmount = value;
            UpdateResourceText();
            CheckWinResult();
        }
        get
        {
            return wheatAmount;
        }
    }

    public int FarmersAmount
    {
        set
        {
            farmersAmount = value;
            UpdateResourceText();
            CheckWinResult();
        }
        get
        {
            return farmersAmount;
        }
    }

    public int WarriorsAmount
    {
        set
        {
            warriorsAmount = value;
            UpdateResourceText();
        }
        get
        {
            return warriorsAmount;
        }
    }

    public int EnemiesAmount
    {
        set
        {
            enemiesAmount = value;
            UpdateResourceText();
        }
        get
        {
            return enemiesAmount;
        }
    }


    public float TotalWheatAmount
    {
        set { totalWheatAmount = value; }
        get { return totalWheatAmount; }
    }

    public int TotalWarriorsAmount
    {
        set { totalWarriorsAmount = value; }
        get { return totalWarriorsAmount; }
    }

    public int AmountOfInvasionCycles
    {
        set {
            amountOfInvasionCycles = value;
            UpdateResourceText();
        }
        get { return amountOfInvasionCycles; }
    }


    private void UpdateResourceText()
    {
        Text resourcesAmountIndicator = this.GetComponent<Text>();
        resourcesAmountIndicator.text = $"\n" +
            $"Пщеница: {wheatAmount}\n" +
            $"Фермеров: {farmersAmount}\n" +
            $"Воинов: {warriorsAmount}\n" +
            $"\n" +
            $"Врагов в следующей волне: {enemiesAmount}\n" +
            $"Пройдено волн: {amountOfInvasionCycles}\n" +
            $"\n" +
            $"\n" +
            $"Условия победы:\n" +
            $"Нанять {farmersAmountToWin} фермеров\n" +
            $"Иметь в запасе {wheatAmountToWin} пщеницы";
    }

    private void CheckWinResult()
    {
        if (wheatAmount >= wheatAmountToWin && farmersAmount >= farmersAmountToWin)
        {
            gameParameters.FinishGame(true);
        }
    }

    public void ResetResources()
    {
        wheatAmount = 0;
        farmersAmount = 1;
        warriorsAmount = 0;
        totalWheatAmount = 0;
        totalWarriorsAmount = 0;
        totalEnemiesAmountt = 0;
        amountOfInvasionCycles = 0;
        UpdateResourceText();
    }
}
