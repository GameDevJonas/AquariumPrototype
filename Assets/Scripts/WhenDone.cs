using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhenDone : Shop
{
    public GameObject fishSpawner;
    public BasicFish nowfish;

    public Shop shop;

    public Collider2D spawnArea;

    public string[] otherNames;

    void Start()
    {
        fishSpawner = GameObject.FindGameObjectWithTag("FishSpawner");
        spawnArea = fishSpawner.GetComponent<Collider2D>();
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        nowfish = selectedFish;
    }

    public void IAmDone()
    {
        Vector2 spawnPosition = fishSpawner.transform.position;
        spawnPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        GameObject newFeesh = Instantiate(selectedFish.daFish, spawnPosition, Quaternion.identity);
        CameraManager.fishees.Add(newFeesh);
        newFeesh.GetComponent<FishAI>().speed = Random.Range(selectedFish.minSpeed, selectedFish.maxSpeed);
        newFeesh.GetComponent<FishAI>().maxHunger = selectedFish.maxHunger;
        newFeesh.GetComponent<FishAI>().hungerRate = selectedFish.hungerRate;
        newFeesh.GetComponent<FishAI>().affectionRate = selectedFish.affectionRate;
        newFeesh.GetComponent<FishAI>().sellPrice = selectedFish.baseSellPrice;
        newFeesh.GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInChildren<TMP_InputField>().text;
        if(GetComponentInChildren<TMP_InputField>().text == "")
        {
            newFeesh.GetComponentInChildren<TextMeshProUGUI>().text = otherNames[Random.Range(0, otherNames.Length)];
        }
        shop.ExitFishInfo();
        Destroy(gameObject);
    }
}
