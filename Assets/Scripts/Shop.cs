using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject newFishCanvas;

    public static string nameOfNewFish;

    public BasicFish[] fishes;
    public static BasicFish selectedFish;

    public Button buyButton;
    public Button[] fishesInShop;

    public TextMeshProUGUI moneyText;
    public int moneys;

    private float moneyTimerSet = 5f;
    public float moneyTimer;

    [Header("Fish Info Screen")]
    public GameObject fishInfoScreen;
    public TextMeshProUGUI fishName, fishCost, fishDesc;

    void Start()
    {
        moneys = 100;
        nameOfNewFish = null;
        fishInfoScreen.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnableDisableBuyButton();
        KeepTrackOfMoney();
        MoneyTimer();
    }

    void EnableDisableBuyButton()
    {
        if (selectedFish == null)
        {
            buyButton.interactable = false;
        }
        else if (selectedFish != null && moneys >= selectedFish.cost)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    void MoneyTimer()
    {
        if(moneyTimer <= 0)
        {
            moneys++;
            moneyTimer = moneyTimerSet;
        }
        else
        {
            moneyTimer -= Time.deltaTime;
        }
    }

    void KeepTrackOfMoney()
    {
        moneyText.text = "FishGold: " + moneys;
    }

    public void BuyFish()
    {
        buyButton.gameObject.SetActive(false);
        GameObject newFish = Instantiate(newFishCanvas, new Vector3(274.5f, 154.5f, 0), Quaternion.identity);
        nameOfNewFish = newFish.GetComponentInChildren<TMP_InputField>().text;
        moneys -= selectedFish.cost;
    }

    #region FISHES

    public void OpenFishInfo(BasicFish fish)
    {
        fishInfoScreen.SetActive(true);
        buyButton.gameObject.SetActive(true);

        fishName.text = fish.fishName;
        fishCost.text = fish.cost + " FG";
        fishDesc.text = fish.description;
    }

    public void ExitFishInfo()
    {
        fishName.text = null;
        fishCost.text = null;
        fishDesc.text = null;
        fishInfoScreen.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    public void FishOneSelected()
    {
        selectedFish = fishes[0];
    }

    #endregion
}
