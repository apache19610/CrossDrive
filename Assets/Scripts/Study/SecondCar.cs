using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SecondCar : MonoBehaviour
{
    private void OnDestroy()
    {       
        PlayerPrefs.SetString("FirstGame", "No");
        SceneManager.LoadScene("Game");
    }
}
