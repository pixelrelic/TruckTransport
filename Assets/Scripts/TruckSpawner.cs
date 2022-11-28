using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour
{   
    public static TruckSpawner instance;

    public Transform spawningPosition;
    public int maxNumberOfvehicles = 5;
    public int vehiclesOnRoad1 = 5;
    public int vehiclesOnRoad2 = 5;
    public Vehicle vehicle;
    public int currentVehicleNumber = 0;

    //effect
    public GameObject spawnEffect;

    public List<Vehicle> allVehicles; //store all spawned vehicles

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        bool canmerge = CanMergeHappen();
        if (canmerge)
        {
            UIManager.instance.ShowMergeBtn();
        }
        else
        {
            UIManager.instance.HideMergeBtn();
        }
    }
    public void SpawnVehicle()
    {
        if (LevelManager.instance.money >= PriceManager.instance.vehiclePrice)
        {
            if (LevelManager.instance.currentPath == 0)
            {
                if (currentVehicleNumber < vehiclesOnRoad1)
                {
                    Vehicle temp = Instantiate(vehicle, spawningPosition.position, Quaternion.identity);
                    allVehicles.Add(temp);
                    currentVehicleNumber++;
                    //show effect
                    Instantiate(spawnEffect, spawningPosition.position, Quaternion.identity);
                    //deduct money                   
                    LevelManager.instance.AddMoney(0 - PriceManager.instance.vehiclePrice);
                    //update vehicle price
                    PriceManager.instance.UpdateVehiclePrice();
                }

                if (currentVehicleNumber == vehiclesOnRoad1)
                {
                    UIManager.instance.ShowVehicleMaxBtn();
                }
            }
            else
            {
                if (currentVehicleNumber < (vehiclesOnRoad1 + vehiclesOnRoad2))
                {
                    Vehicle temp = Instantiate(vehicle, spawningPosition.position, Quaternion.identity);
                    allVehicles.Add(temp);
                    currentVehicleNumber++;
                    //show effect
                    Instantiate(spawnEffect, spawningPosition.position, Quaternion.identity);
                    //deduct money
                    LevelManager.instance.AddMoney(0 - PriceManager.instance.vehiclePrice);
                    //update vehicle price
                    PriceManager.instance.UpdateVehiclePrice();
                }

                if (currentVehicleNumber == (vehiclesOnRoad1 + vehiclesOnRoad2))
                {
                    UIManager.instance.ShowVehicleMaxBtn();
                }
            }
        }
        
    }


    public void MergeVehicles()
    {
        //Merge criteria :
        //2 small vehicle will become 1 medium vehicle
        //2 medium vehicle will become 1 large vehicle

        //logic :
        //first look for 2 small vehicles
        //if found upgrade first one to medium and remove other 
        //if not found look for 2 medium vehicles
        //if found upgarade 1st to large and remove other 
        //else do nothing
        if (LevelManager.instance.money >= PriceManager.instance.mergePrice)
        {
            bool mergerHappened = false;
            int count = 0;
            List<int> temp = new List<int>();
            foreach (Vehicle v in allVehicles)
            {
                if (v.activeVehicle == 0)
                {
                    temp.Add(count); //stored index of level 0 vehicles
                }
                count++;
            }

            if (temp.Count > 1)
            {
                //we can merge level 0 cars
                //upgrade 1st car
                allVehicles[temp[0]].UpdateVehicle();
                //show effect
                Instantiate(spawnEffect, allVehicles[temp[0]].gameObject.transform.position, Quaternion.identity);
                //delete 2nd  car
                Vehicle todestory = allVehicles[temp[1]];

                allVehicles.RemoveAt(temp[1]);
                currentVehicleNumber--;
                Destroy(todestory.gameObject);
                mergerHappened = true;

                UIManager.instance.HideVehicleMaxBtn();

                //deduct money
                LevelManager.instance.AddMoney(0 - PriceManager.instance.mergePrice);
                //update merge price
                PriceManager.instance.UpdateMergePrice();
                
            }

            if (!mergerHappened)
            {
                count = 0;
                temp = new List<int>();
                foreach (Vehicle v in allVehicles)
                {
                    if (v.activeVehicle == 1)
                    {
                        temp.Add(count); //stored index of level 0 vehicles
                    }
                    count++;
                }

                if (temp.Count > 1)
                {
                    //we can merge level1 cars
                    //upgrade 1st car
                    allVehicles[temp[0]].UpdateVehicle();
                    Instantiate(spawnEffect, allVehicles[temp[0]].gameObject.transform.position, Quaternion.identity);
                    //delete 2nd  car
                    Vehicle todestory = allVehicles[temp[1]];

                    allVehicles.RemoveAt(temp[1]);
                    currentVehicleNumber--;
                    Destroy(todestory.gameObject);
                    mergerHappened = true;

                    UIManager.instance.HideVehicleMaxBtn();

                    //deduct money
                    LevelManager.instance.AddMoney(0 - PriceManager.instance.mergePrice);
                    //update merge price
                    PriceManager.instance.UpdateMergePrice();
                }

            }
        }
        

    
    }

    public bool CanMergeHappen()
    {
        bool mergerHappened = false;
        int count = 0;
        List<int> temp = new List<int>();
        foreach (Vehicle v in allVehicles)
        {
            if (v.activeVehicle == 0)
            {
                temp.Add(count); //stored index of level 0 vehicles
            }
            count++;
        }

        if (temp.Count > 1)
        {
            //we can merge level 0 cars
            return true;
        }

        if (!mergerHappened)
        {
            count = 0;
            temp = new List<int>();
            foreach (Vehicle v in allVehicles)
            {
                if (v.activeVehicle == 1)
                {
                    temp.Add(count); //stored index of level 0 vehicles
                }
                count++;
            }

            if (temp.Count > 1)
            {
                //we can merge level 1 cars
                return true;
            }
        }
        return false;
    }
}
