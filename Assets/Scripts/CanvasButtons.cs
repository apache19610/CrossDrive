using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{


    public Sprite buttonReleased, buttonPressed, musicOn, musicOff;
    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        if (gameObject.name == "MusicButton")
        {
            if (PlayerPrefs.GetString("music") == "No")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
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

    public void MusicButton()
    {
        if (PlayerPrefs.GetString("music") == "No")
        {
            PlayerPrefs.SetString("music", "Yes");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Play();
            }
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Pause();
            }
        }

        PlayButtonSound();
    }

    public void ShopScene()
    {
        StartCoroutine(LoadScene("Shop"));
        PlayButtonSound();
    }
    public void ExitShopScene()
    {
        StartCoroutine(LoadScene("Main"));
        PlayButtonSound();
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetString("FirstGame") == "No") 
        StartCoroutine(LoadScene("Game"));
        else
        {
            StartCoroutine(LoadScene("Study"));
        }
        PlayButtonSound();
    }
    
    public void RestartGame()
    {
        StartCoroutine(LoadScene("Game"));
        PlayButtonSound();
    }

    public void SetPressedButton()
    {
        buttonImage.sprite = buttonPressed;
        transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
    }

    public void SetReleasedbutton()
    {
        buttonImage.sprite = buttonReleased;
        transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
    }

    IEnumerator LoadScene(string name)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    public void PlayButtonSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
