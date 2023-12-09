using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public AudioClip crash;
    public AudioClip[] accelerates;
    public bool rightTurn, leftTurn, moveFromUp;
    public float speed = 15f, force = 500f;
    private Rigidbody carrb;
    private float originRotatonY, rotateMultRight = 6f, rotateMultLeft = 5f;
    private Camera mainCam;
    public LayerMask carsLayer;
    private bool isMovingFast, carCrashed;
    [NonSerialized]public bool carPassed;
    [NonSerialized] public static bool isLose;
    public GameObject turnLeftSignal, turnRightSignal, explosion, exhaust;

    [NonSerialized] public static int countCars;
        
    private void Start()
    {
        mainCam = Camera.main;
        originRotatonY = transform.eulerAngles.y;
        carrb = GetComponent<Rigidbody>();

        if (rightTurn)
        {
            StartCoroutine(TurnSignals(turnRightSignal));
        }
        else if (leftTurn)
        {
            StartCoroutine(TurnSignals(turnLeftSignal));
        }
    }

    IEnumerator TurnSignals(GameObject turnSignal)
    {
        while (!carPassed)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        carrb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
#if UNITY_WEBGL
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount == 0)
            return;
        Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
#endif

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, carsLayer))
        {
            string carName = hit.transform.gameObject.name;

#if UNITY_WEBGL
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name == carName)
            {
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName)
            {
#endif
                GameObject vfx = Instantiate(exhaust, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
                Destroy(vfx, 2f);
                speed *= 2f;
                isMovingFast = true;
                if (PlayerPrefs.GetString("music") != "No")
                {
                    GetComponent<AudioSource>().clip = accelerates[UnityEngine.Random.Range(0, accelerates.Length)];
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car") && !carCrashed) 
        {
            carCrashed = true;
            isLose = true;
            speed = 0;
            other.gameObject.GetComponent<CarController>().speed = 0;
            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfx, 5f);
            if (isMovingFast)
                force *= 1.2f;
            carrb.AddRelativeForce(Vector3.forward * -force);

            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = crash;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carCrashed)
            return;
        if (other.transform.CompareTag("TurnBlockRight") && rightTurn)
        {
            RotateCar(rotateMultRight);
        }
        else if(other.transform.CompareTag("TurnBlockLeft") && leftTurn)
        {
            RotateCar(rotateMultLeft, -1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
            other.GetComponent<CarController>().speed = speed + 5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (carCrashed)
            return;
        if (other.transform.CompareTag("TriggerPass"))
        {
            if (carPassed)
                return;
            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
            {
                col.enabled = true;
                countCars++;
            }
        }

        if (other.transform.CompareTag("TurnBlockRight") && rightTurn)
        {
            carrb.rotation = Quaternion.Euler(0, originRotatonY + 90f, 0);
        }
        else if (other.transform.CompareTag("TurnBlockLeft") && leftTurn)
        {
            carrb.rotation = Quaternion.Euler(0, originRotatonY - 90f, 0);
        }
        else if (other.transform.CompareTag("DeleteTrigger")) 
            Destroy(gameObject);
    }

    private void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed)
            return;
        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotatonY - 90f)  
        {
            return;
        }
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f) 
        {
            return;
        }
        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotetion = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        carrb.MoveRotation(carrb.rotation * deltaRotetion);
    }
}
