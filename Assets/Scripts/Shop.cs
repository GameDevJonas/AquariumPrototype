using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject newFishCanvas;

    public static string nameOfNewFish;

    public BasicFish[] fishes;
    public static BasicFish selectedFish;

    void Start()
    {
        nameOfNewFish = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFish()
    {
        GameObject newFish = Instantiate(newFishCanvas, new Vector3(274.5f, 154.5f, 0), Quaternion.identity);
        nameOfNewFish = newFish.GetComponentInChildren<TMP_InputField>().text;
    }

    #region FISHES

    public void FishOneSelected()
    {
        selectedFish = fishes[0];
    }

    #endregion
}
