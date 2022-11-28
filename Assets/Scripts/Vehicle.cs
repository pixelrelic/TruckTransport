using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public int currentLocationIndex;
    Transform nextlocation;
    public float speed = 5f;

    //sound effect
    public AudioClip otherClip;

    //score effect
    public GameObject scoreEffect;
    public GameObject spawnPoint;
    //public GameObject scoreCanvas;

    //variables for finance
    public GameObject[] vehicles; //type of vehicles
    public int activeVehicle; // this is index, index represents level of vehicle
                              // 2 level 1(0) can combine to make 1 level 2(1)
                              // and 2 level 2(1) can combine to make 1 level 3

    public int[] incomePerVehicle; // how much money will it make as per level of vehicle
    public GameObject[] loads; //reference of loads of vehicle

    void Start()
    {
        //currentLocationIndex = 0;
        currentLocationIndex = LevelManager.instance.GetNextLocationIndex(currentLocationIndex);
        nextlocation = LevelManager.instance.GetNextlocation(currentLocationIndex);
    }

    void Update()
    {
        Move();
        Rotate();
        //transform.LookAt(nextlocation);
    }

    private void Rotate()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = nextlocation.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = 5 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Move()
    {
        
        //move towards that location
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, nextlocation.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, nextlocation.position) < 0.001f)
        {
            //get next location
            currentLocationIndex = LevelManager.instance.GetNextLocationIndex(currentLocationIndex);
            nextlocation = LevelManager.instance.GetNextlocation(currentLocationIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            //Debug.Log("Entered in a shop");
            LevelManager.instance.AddMoney(incomePerVehicle[activeVehicle]);
            //play sound
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(otherClip);

            //show effect
            //other.GetComponent<Shop>().ShowScorePopup(incomePerVehicle[activeVehicle]);
            var go = Instantiate(scoreEffect, spawnPoint.transform.position, Quaternion.Euler(new Vector3(42, -180, -360)));
            go.GetComponent<TextMesh>().text =  incomePerVehicle[activeVehicle] + "$";
            //check if its last shop
            if (ShopSpawner.Instance.IsLastShop(other.GetComponent<Shop>().GetId()))
                HideLoad();
        }
        else if (other.CompareTag("Warehouse"))
        {
            ShowLoad();
        }
       
    }

    public void UpdateVehicle()
    {
        vehicles[activeVehicle].SetActive(false);
        activeVehicle++;
        vehicles[activeVehicle].SetActive(true);
    }


    public void ShowLoad()
    { 
        loads[activeVehicle].SetActive(true);
    }

    public void HideLoad()
    { 
        loads[(activeVehicle)].SetActive(false);
    }
}
