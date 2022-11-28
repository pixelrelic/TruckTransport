using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuManager : MonoBehaviour
{
    public Text amountUIElement;
    public Text vehiclePriceUIElement;
    public Text roadPriceUIElement;
    public Text shopPriceUIElement;
    public Text mergePriceUIElement;

    public void AddMoney()
    {
        int number;
        if (int.TryParse(amountUIElement.text, out number))
        {
            LevelManager.instance.money += number;
        }
    }

    public void ChangeAddVehiclePrice()
    {
        int number;
        if (int.TryParse(vehiclePriceUIElement.text, out number))
        {
            PriceManager.instance.vehiclePrice = number;
        }
        
    }

    public void ChangeAddShopPrice()
    {
        int number;
        if (int.TryParse(shopPriceUIElement.text, out number))
        {
            PriceManager.instance.shopPrice = number;
        }
    }

    public void ChangeAddRoadPrice()
    {
        int number;
        if (int.TryParse(roadPriceUIElement.text, out number))
        {
            PriceManager.instance.roadPrice = number;
        }
    }

    public void ChangeMergeVehiclePrice()
    {
        int number;
        if (int.TryParse(mergePriceUIElement.text, out number))
        {
            PriceManager.instance.mergePrice = number;
        }
    }
}
