using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Text references : 
    public Text mergeAmount;
    public Text addVehicleAmount;
    public Text addShopAmount;
    public Text addRoadAmount;
    public Text moneyAmount;


    //Btn Refences :
    public Button mergeButton;
    public Button addVehicleButton;
    public Button addShopButton;
    public Button addRoadButton;

    //max buttons
    public Button addVehicleMaxButton;
    public Button addShopMaxButton;

    private void Awake()
    {
        instance = this;
    }

    //setters
    public void SetMergeAmount(int amount)
    {
        mergeAmount.text = amount.ToString() + "$";
    }

    public void SetAddVehicleAmount(int amount)
    {
        addVehicleAmount.text = amount.ToString() + "$";
    }

    public void SetAddShopAmount(int amount)
    {

        addShopAmount.text = amount.ToString() + "$";

    }

    public void SetAddRoadButtonAmount(int amount)
    {
        addRoadAmount.text = amount.ToString() + "$";
    }

    public void SetMoney(int amount)
    {
        moneyAmount.text = amount.ToString();
    }

    //Getters
    public int GetMergeAmount()
    {
        return int.Parse(mergeAmount.text);
    }

    public int GetAddVehicleAmount()
    {
        return int.Parse(addVehicleAmount.text);
    }

    public int GetAddShopAmount()
    {
        return int.Parse(addShopAmount.text);
    }

    public int GetAddRoadButtonAmount()
    {
        return int.Parse(addRoadAmount.text);
    }


    private void Start()
    {
        //init UI      
        UpdateUI();

    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        //get values from price manager, and display on UI
        //values to display:
        //1. Money
        SetMoney(LevelManager.instance.money);
        //2. merge price
        SetMergeAmount(PriceManager.instance.mergePrice);
        //3. add vehicle price
        SetAddVehicleAmount(PriceManager.instance.vehiclePrice);
        //4. add shop price
        SetAddShopAmount(PriceManager.instance.shopPrice);
        //5. add road price
        SetAddRoadButtonAmount(PriceManager.instance.roadPrice);


        LevelManager lvm = LevelManager.instance;
        PriceManager pm = PriceManager.instance;

        //enable or disable buttons
        //1. add vehicle button
        if (lvm.money >= pm.vehiclePrice)
        {
            addVehicleButton.enabled = true;
        }
        else
        {
            addVehicleButton.enabled = false;
        }

        //2. add shop button
        if (lvm.money >= pm.shopPrice)
        {
            addShopButton.enabled = true;
        }
        else
        {
            addShopButton.enabled = false;
        }

        //3. add road button
        if (lvm.money >= pm.roadPrice)
        {
            addRoadButton.enabled = true;
        }
        else
        {
            addRoadButton.enabled = false;
        }

        // 4. merge button
        if (lvm.money >= pm.mergePrice)
        {
            mergeButton.enabled = true;
        }
        else
        {
            mergeButton.enabled = false;
        }
    }

    public void ShowVehicleMaxBtn()
    {
        addVehicleButton.gameObject.SetActive(false);
        addVehicleMaxButton.gameObject.SetActive(true);
    }

    public void HideVehicleMaxBtn()
    {
        addVehicleButton.gameObject.SetActive(true);
        addVehicleMaxButton.gameObject.SetActive(false);
    }

    public void ShowShopMaxBtn()
    {
        addShopButton.gameObject.SetActive(false);
        addShopMaxButton.gameObject.SetActive(true);
    }

    public void HideShopMaxBtn()
    {
        addShopButton.gameObject.SetActive(true);
        addShopMaxButton.gameObject.SetActive(false);
    }


    public void ShowMergeBtn()
    {
        mergeButton.gameObject.SetActive(true);
    }

    public void HideMergeBtn()
    {
        mergeButton.gameObject.SetActive(false);
    }
}
