using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMap : MonoBehaviour
{
    public void ChooseNewMap(int numberMap)
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }

        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().whichMapSelected();
    }
}
