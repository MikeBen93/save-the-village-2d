using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Sprite muteOnSprite;
    [SerializeField] private Sprite muteOffSprite;
    [SerializeField] private GameObject[] audioSources;
    private bool muted;


    public void MuteSound()
    {
        this.GetComponent<AudioSource>().Play();

        if (muted)
        {
            this.GetComponent<Image>().sprite = muteOffSprite;

            foreach (GameObject audio in audioSources)
            {
                audio.GetComponent<AudioSource>().mute = false;
            }
        }
        else
        {
            this.GetComponent<Image>().sprite = muteOnSprite;
             
            foreach (GameObject audio in audioSources)
            {
                audio.GetComponent<AudioSource>().mute = true;
            }
        }

        muted = !muted;

    }
}
