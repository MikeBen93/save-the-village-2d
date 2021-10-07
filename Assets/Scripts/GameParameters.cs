using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameParameters : MonoBehaviour
{
    [SerializeField] private Enemies enemies;
    [SerializeField] private Warriors warriors;
    [SerializeField] private Farmers farmers;
    [SerializeField] private GameObject gameResultPanel;
    [SerializeField] private Resources gameResources;
    

        
    public void FinishGame(bool isGameWon)
    {
        Text[] textsOnResultPanel = gameResultPanel.GetComponentsInChildren<Text>();

        Time.timeScale = 0;

        foreach (Text outputText in textsOnResultPanel)
        {
            switch (outputText.name)
            {
                case "ResultTitleText":
                    if (isGameWon) outputText.text = "Победа!!!";
                    else outputText.text = "Поражение =(";
                    break;
                case "ResultMainText":
                    outputText.text = PrintResults();
                    break;
            }

        }

        gameResultPanel.SetActive(true);
    }

    private string PrintResults()
    {
         return $"Всего нанято фермеров: {gameResources.FarmersAmount}\n" +
            $"Всего добыто пщеницы: {gameResources.TotalWheatAmount}\n" +
            $"Всего нанято воинов: {gameResources.TotalWarriorsAmount}\n" +
            $"Пройдено волн: {gameResources.AmountOfInvasionCycles}";
    }

    public void RestartGame()
    {
        gameResources.ResetResources();
        enemies.GameRestart();
        farmers.GameRestart();
        warriors.GameRestart();
        gameResultPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
