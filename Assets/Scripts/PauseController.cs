using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;
    private bool paused;

    public void PauseGame()
    {
        this.GetComponent<AudioSource>().Play();

        if (paused)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            this.GetComponent<Image>().sprite = pauseSprite;
        }
        else
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            this.GetComponent<Image>().sprite = playSprite;
        }

        paused = !paused;

    }
}
