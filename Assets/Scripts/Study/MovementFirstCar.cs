using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFirstCar : MonoBehaviour
{
    public GameObject canvasFirst, secondCar, canvasSecond;
    private bool isFirst;
    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }
    void Update()
    {
        if (transform.position.x < 8f && !isFirst)
        {
            isFirst = true;
            carController.speed = 0;
            canvasFirst.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (!isFirst || transform.position.x > 9)  return;
        carController.speed = 15f;
        canvasFirst.SetActive(false);
        canvasSecond.SetActive(true);
        secondCar.GetComponent<CarController>().speed = 12f;
    }
}
