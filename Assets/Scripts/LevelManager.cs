using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   

    //static global instance
    public static LevelManager instance;

    //reference to paths
    public GameObject[] path1;
    public GameObject[] path2;
    public GameObject[] BarricadeSet1;
    public GameObject[] BarricadeSet2;


    public GameObject Road1, Road2;

    public int currentPath;


    //virtual cameras
    public GameObject vcam1, vcam2;

    //Finance Management
    public int money; // Total Money you have

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        currentPath = 0;
        foreach (var barricade in BarricadeSet1)
        {
            barricade.gameObject.SetActive(false);
        }

        // foreach (var barricade in BarricadeSet2)
        // {
        //     barricade.gameObject.SetActive(true);
        //}

        //spawn a vehicle
        TruckSpawner.instance.SpawnVehicle();
    }



    //function to set path, currently we only have path1 and path2
    public void SetCurrentPath(int number)
    {
        if (currentPath != number)
        {
            if (number == 0)
            {
                foreach (var barricade in BarricadeSet1)
                {
                    barricade.gameObject.SetActive(false);
                }
                Road2.SetActive(false);
            }
            else
            {
                if (money >= PriceManager.instance.roadPrice)
                {
                    foreach (var barricade in BarricadeSet1)
                    {
                        barricade.gameObject.SetActive(true);
                    }
                    Road2.SetActive(true);
                    currentPath = 1;
                    UIManager.instance.HideVehicleMaxBtn();
                    UIManager.instance.HideShopMaxBtn();
                    vcam2.SetActive(true);
                    vcam1.SetActive(false);
                    //deduct money
                    AddMoney(0 - PriceManager.instance.roadPrice);
                }
                
            }

        }

        
    }

    //function to get next location for vehicle to travel
    public int GetNextLocationIndex(int currentLocation)
    {
        if (currentPath == 0)
        {
            int nextIndex = (currentLocation + 1) % path1.Length;
            return nextIndex;
        }
        else 
        {
            int nextIndex = (currentLocation + 1) % path2.Length;
            return nextIndex;
        }
    }

    public Transform GetNextlocation(int index)
    {
        if (currentPath == 0)
        {
            return path1[index].transform;
        }
        else
        {
            return path2[index].transform;
        }
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
    }
}
