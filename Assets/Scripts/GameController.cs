using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetToLeaderBoard(int value);
    public GameObject[] maps;
    public bool isMainScene;
    public GameObject[] cars;
    public GameObject canvasLosePanel;
    public float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    public int countCars = 0;
    private Coroutine bottomCars, leftCars, rightCars, upCars;
    private bool isLoseOnce;
    public Text nowScore, topScore, coinsCount;
    int TopScore;
    public GameObject horn;
    public AudioSource turnSignal;

    void Start()
    {
        if (PlayerPrefs.GetInt("NowMap") == 2) 
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);
        }else if(PlayerPrefs.GetInt("NowMap") == 3)
        {
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }
        else 
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
            
        }
        CarController.isLose = false;
        CarController.countCars = 0;
        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }

        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());

        StartCoroutine(CreateHorn());
    }


    private void Update()
    {
        if (CarController.isLose && !isLoseOnce)
        {
            StopCoroutine(bottomCars);
            StopCoroutine(leftCars);
            StopCoroutine(rightCars);
            StopCoroutine(upCars);
            nowScore.text = "<color=#FF0B00>Ñ÷¸ò:</color> " + CarController.countCars.ToString();
            if (PlayerPrefs.GetInt("Score")< CarController.countCars)
            
                PlayerPrefs.SetInt("Score", CarController.countCars);
            TopScore = PlayerPrefs.GetInt("Score");
                topScore.text = "<color=#FF0B00>Ëó÷øèé ñ÷¸ò:</color> " + TopScore;
            SetToLeaderBoard(TopScore);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
                coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
            
            canvasLosePanel.SetActive(true);
            isLoseOnce = true;
        }
    }

    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-1f, -0.39f, -26.3f), 180f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-68.9f, -0.39f, 3.7f), 270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(30.5f, -0.39f, 10.6f), 90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-8f, -0.39f, 66.2f), 0f, true);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    void SpawnCar(Vector3 pos, float rotY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotY, 0)) as GameObject;
        newObj.name = "Car - " + ++countCars;

        int random = isMainScene == true ? 1 : Random.Range(1, 4);
        if (isMainScene)
        {
            newObj.GetComponent<CarController>().speed = 10f;
        }
        switch (random)
        {
            case 1:
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying) 
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                break;
            case 2:
                newObj.GetComponent<CarController>().leftTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                if (isMoveFromUp)
                {
                    newObj.GetComponent<CarController>().moveFromUp = true;
                }
                break;
            case 3:
                //Move toward
                break;
        }
    }

    void StopSound()
    {
        turnSignal.Stop();
    }

    IEnumerator CreateHorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 9));
            if (PlayerPrefs.GetString("music") != "No")
            {
                Instantiate(horn, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
