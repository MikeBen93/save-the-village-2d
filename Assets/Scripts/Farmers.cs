using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farmers : MonoBehaviour
{
    [SerializeField] private AudioSource hiringButtonClickSound;
    [SerializeField] private AudioSource hiringFinishSound;
    [SerializeField] private AudioSource wheatHarvestSound;

    [SerializeField] private Resources resources;
    [SerializeField] private GameParameters gameParameters;

    [SerializeField] private GameObject WheatHarvestTimerLabel;
    private Image WheatHarvestTimerImage;
    private Text WheatHarvestAmountText;

    [SerializeField] private GameObject farmerTrainingTimer;
    [SerializeField] private Button farmerHireButton;
    [SerializeField] private Text farmerPriceText;

    [SerializeField] private float farmerPrice = 8;
    [SerializeField] private int initialFarmersAmount = 1;

    [SerializeField] private float wheatHarvestPeriod = 4;
    [SerializeField] private float wheatAmountHarvestForPeriod = 2;
    [SerializeField] private float farmerTrainingTime = 8;

    private float wheatBarCurrentTime = 0;
    private bool isHiringStarted = false;
    private float trainingTime = 0;
    private Image farmerTrainingTimerImage;

    void Start()
    {
        FetchTimerImageAndText();
        
        resources.FarmersAmount = initialFarmersAmount;
    }

    void Update()
    {
        WheatHarvest();

        if (isHiringStarted) HiringProcess();
    }

    public void GameRestart()
    {
        wheatBarCurrentTime = 0;
        trainingTime = 0;
        isHiringStarted = false;
        farmerHireButton.interactable = true;
        farmerTrainingTimer.SetActive(false);
    }

    void FetchTimerImageAndText()
    {
        //getting and setting text with wheat amount harvest for period
        WheatHarvestAmountText = WheatHarvestTimerLabel.GetComponentInChildren<Text>();
        WheatHarvestAmountText.text = $"{wheatAmountHarvestForPeriod}";

        //getting and setting image with wheat harvest timer
        foreach (Image FetchedImage in WheatHarvestTimerLabel.GetComponentsInChildren<Image>())
        {
            if (FetchedImage.name == "WheatHarvestTimerImage") WheatHarvestTimerImage = FetchedImage;
        }
        WheatHarvestTimerImage.fillAmount = wheatBarCurrentTime / wheatHarvestPeriod;

        //getting image of farmer training timer
        farmerTrainingTimerImage = farmerTrainingTimer.GetComponent<Image>();

        farmerPriceText.text = farmerPrice.ToString();
    }

    void WheatHarvest()
    {
        wheatBarCurrentTime += Time.deltaTime;
        if (wheatBarCurrentTime >= wheatHarvestPeriod)
        {
            wheatBarCurrentTime = 0;
            resources.WheatAmount += wheatAmountHarvestForPeriod * resources.FarmersAmount;
            resources.TotalWheatAmount += wheatAmountHarvestForPeriod * resources.FarmersAmount;

            wheatHarvestSound.Play();
        }

        WheatHarvestTimerImage.fillAmount = wheatBarCurrentTime / wheatHarvestPeriod;
    }

    public void HireFarmer()
    {   
        if (resources.WheatAmount >= farmerPrice)
        {
            hiringButtonClickSound.Play();
            resources.WheatAmount -= farmerPrice;
            isHiringStarted = true;
            farmerTrainingTimer.SetActive(true);
            farmerHireButton.interactable = false;
            trainingTime = 0;
            farmerTrainingTimerImage.fillAmount = 1;
        }
    }

    void HiringProcess()
    {
        
        if (trainingTime <= farmerTrainingTime)
        {
            trainingTime += Time.deltaTime;
            farmerTrainingTimerImage.fillAmount = (farmerTrainingTime - trainingTime) / farmerTrainingTime;
        }
        else
        {
            hiringFinishSound.Play();
            resources.FarmersAmount++;

            farmerHireButton.interactable = true;
            farmerTrainingTimer.SetActive(false);
            isHiringStarted = false;
        }
    }

}
