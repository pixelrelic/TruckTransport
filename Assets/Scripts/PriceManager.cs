using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    public int shopPrice;
    public int vehiclePrice;
    public int roadPrice;
    public int mergePrice;

    public int shopPriceUpgradefactor;
    public int vehiclePriceUpgradeFactor;
    public int mergePriceUpgradeFactor;

    public static PriceManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateShopPrice()
    {
        shopPrice += shopPriceUpgradefactor;
        shopPriceUpgradefactor += 5;
    }

    public void UpdateVehiclePrice()
    {
        vehiclePrice += vehiclePriceUpgradeFactor;
        vehiclePriceUpgradeFactor += 2;
    }

    public void UpdateMergePrice()
    {
        mergePrice += mergePriceUpgradeFactor;
        mergePriceUpgradeFactor += 20;
    }
}
