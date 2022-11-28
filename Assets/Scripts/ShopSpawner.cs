using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    public static ShopSpawner Instance;

    public GameObject[] Shops; //to store all shop references
    public int noOfShopsRoad1;
    public int noOfShopsRoad2;

    public int enabledShops;

    //effect
    public GameObject spawnEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void EnableShop()
    {
        if (PriceManager.instance.shopPrice <= LevelManager.instance.money)
        {
            if (LevelManager.instance.currentPath == 0)
            {
                if (enabledShops < noOfShopsRoad1)
                {
                    //enable shop and increse count
                    Shops[enabledShops].SetActive(true);
                    //show effect
                    Instantiate(spawnEffect, Shops[enabledShops].gameObject.transform.position, Quaternion.identity);
                    enabledShops += 1;
                    
                    //deduct money
                    LevelManager.instance.AddMoney(0 - PriceManager.instance.shopPrice);
                    //increase shop price
                    PriceManager.instance.UpdateShopPrice();
                }

                if (enabledShops == noOfShopsRoad1)
                {
                    UIManager.instance.ShowShopMaxBtn();
                }
            }
            else
            {
                if (enabledShops < (noOfShopsRoad2 + noOfShopsRoad1))
                {
                    //enable shop and increse count
                    Shops[enabledShops].SetActive(true);
                    //show effect
                    Instantiate(spawnEffect, Shops[enabledShops].gameObject.transform.position, Quaternion.identity);
                    enabledShops += 1;
                    //deduct money
                    LevelManager.instance.AddMoney(0 - PriceManager.instance.shopPrice);
                    //increase shop price
                    PriceManager.instance.UpdateShopPrice();
                }

                if (enabledShops == (noOfShopsRoad1 + noOfShopsRoad2))
                {
                    UIManager.instance.ShowShopMaxBtn();
                }
            }
        }

        else
        {
            Debug.Log("not enough money to add shop");
        }
        
    }


    public bool IsLastShop(int id)
    {
        if((id+1) == enabledShops)
            return true;
        else
            return false;
    }
    
}
