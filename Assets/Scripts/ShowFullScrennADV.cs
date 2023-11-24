using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShowFullScrennADV : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();

    void Start()
    {
        ShowAdv();
    }

    public void OpenAdv()
    {
        Time.timeScale = 0;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.Pause();
        }
    }

    public void CloseAdv()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetString("music") == "No")
        {
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Pause();
            }
        }
        else
        {
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Play();
            }
        }
    }
}
