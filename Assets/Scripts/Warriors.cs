using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warriors : MonoBehaviour
{
    [SerializeField] private AudioSource hiringButtonClickSound;
    [SerializeField] private AudioSource hiringFinishSound;
    [SerializeField] private AudioSource wheatPaymentSound;

    [SerializeField] private Resources resources;
    [SerializeField] private GameParameters gameParameters;

    [SerializeField] private GameObject WheatPaymentTimerLabel;
    private Image WheatPaymentTimerImage;
    private Text WheatPaymentAmountText;

    [SerializeField] private GameObject warriorsTrainingTimer;
    [SerializeField] private Button warriorsHireButton;
    [SerializeField] private Text warriorsPriceText;

    [SerializeField] private float warriorPrice = 16;
    [SerializeField] private int initialWarriorsAmount = 0;

    [SerializeField] private float warriorPaymentPeriod = 2;
    [SerializeField] private float warriorPaymentAmount = 2f;
    [SerializeField] private float warriorTrainingTime = 16;

    private float paymentBarCurrentTime = 0;
    private bool isHiringStarted = false;
    private float trainingTime = 0;
    private Image warriorsTrainingTimerImage;

    void Start()
    {
        FetchTimerImageAndText();

        resources.WarriorsAmount = initialWarriorsAmount;
    }

    void Update()
    {
        WarriorsPayment();

        if (isHiringStarted) HiringProcess();
    }

    public void GameRestart()
    {
        paymentBarCurrentTime = 0;
        trainingTime = 0;
        isHiringStarted = false;
        warriorsHireButton.interactable = true;
        warriorsTrainingTimer.SetActive(false);
    }

    void FetchTimerImageAndText()
    {
        //getting and setting text with wheat payment amount for period
        WheatPaymentAmountText = WheatPaymentTimerLabel.GetComponentInChildren<Text>();
        WheatPaymentAmountText.text = $"{warriorPaymentAmount}";

        //getting and setting image with wheat harvest timer
        foreach (Image FetchedImage in WheatPaymentTimerLabel.GetComponentsInChildren<Image>())
        {
            if (FetchedImage.name == "WheatPaymentTimerImage") WheatPaymentTimerImage = FetchedImage;
        }
        WheatPaymentTimerImage.fillAmount = paymentBarCurrentTime / warriorPaymentPeriod;

        //getting image of farmer training timer
        warriorsTrainingTimerImage = warriorsTrainingTimer.GetComponent<Image>();

        warriorsPriceText.text = warriorPrice.ToString();
    }

    private void WarriorsPayment()
    {
        if (resources.WarriorsAmount > 0) paymentBarCurrentTime += Time.deltaTime;
        else paymentBarCurrentTime = 0;

        if (paymentBarCurrentTime >= warriorPaymentPeriod)
        {
            paymentBarCurrentTime = 0;
            wheatPaymentSound.Play();
            resources.WheatAmount -= warriorPaymentAmount * resources.WarriorsAmount;
        }

        WheatPaymentTimerImage.fillAmount = paymentBarCurrentTime / warriorPaymentPeriod;
    }

    public void HireWarrior()
    {
        if (resources.WheatAmount >= warriorPrice)
        {
            hiringButtonClickSound.Play();

            resources.WheatAmount -= warriorPrice;
            isHiringStarted = true;
            warriorsTrainingTimer.SetActive(true);
            warriorsHireButton.interactable = false;
            trainingTime = 0;
            warriorsTrainingTimerImage.fillAmount = 1;
        }
    }

    private void HiringProcess()
    {
        if (trainingTime <= warriorTrainingTime)
        {
            trainingTime += Time.deltaTime;
            warriorsTrainingTimerImage.fillAmount = (warriorTrainingTime - trainingTime) / warriorTrainingTime;
        }
        else
        {
            hiringFinishSound.Play();
            resources.WarriorsAmount++;
            resources.TotalWarriorsAmount++;

            warriorsHireButton.interactable = true;
            warriorsTrainingTimer.SetActive(false);
            isHiringStarted = false;

        }

    }
}
