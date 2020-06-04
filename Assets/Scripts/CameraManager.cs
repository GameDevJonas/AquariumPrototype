using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera normalCam;
    public CinemachineVirtualCamera zoomInCam;
    public CinemachineVirtualCamera freeCam;

    public GameObject otherCanvas;
    public GameObject zoomCanvas;
    public GameObject currentFishInZoom;

    public static GameObject[] fishes;
    public int currentFish;

    public Shop shop;

    public static List<GameObject> fishees = new List<GameObject>();

    public bool isInZoom;
    public bool isInFreeCam;

    public float feedTimerSet = 5f;
    public float feedTimer;
    public float foodValue = 10f;

    [Header("Info for Zoom in cam")]
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI personality;
    public TextMeshProUGUI personalityDesc;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI hunger;
    public TextMeshProUGUI affection;
    public Button feedButton;

    void Start()
    {
        isInFreeCam = false;
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        feedButton.interactable = false;
        feedTimer = 0;
        isInZoom = false;
        currentFish = 0;
        otherCanvas.SetActive(true);
        zoomCanvas.SetActive(false);
        normalCam.Priority = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextFish();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevFish();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isInZoom)
        {
            KillFish();
        }

        if ((currentFishInZoom != null && currentFishInZoom.GetComponent<FishAI>().currentHunger + foodValue <= currentFishInZoom.GetComponent<FishAI>().maxHunger) && feedTimer <= 0)
        {
            feedButton.interactable = true;
        }
        else if(!feedButton.interactable)
        {
            feedTimer -= Time.deltaTime;
        }

        if (currentFishInZoom != null)
        {
            hunger.text = "Hunger: " + Mathf.RoundToInt(currentFishInZoom.GetComponent<FishAI>().currentHunger);
            affection.text = "Affection: " + Mathf.RoundToInt(currentFishInZoom.GetComponent<FishAI>().currentAffection);
        }
    }

    public void NextFish()
    {
        if (currentFish < fishees.Count - 1)
        {
            //target = fishes[currentFish + 1];
            GameObject target = fishees[currentFish + 1];
            currentFish += 1;
            print(currentFish);
            OnZoomIn(target);

        }
        else
        {
            //target = fishes[0];
            currentFish = 0;
            GameObject target = fishees[0];
            OnZoomIn(target);
        }
    }

    public void PrevFish()
    {
        if (currentFish > 0)
        {
            //target = fishes[currentFish + 1];
            GameObject target = fishees[currentFish - 1];
            currentFish -= 1;
            print(currentFish);
            OnZoomIn(target);

        }
        else
        {
            //target = fishes[0];
            currentFish = fishees.Count - 1;
            GameObject target = fishees[fishees.Count - 1];
            OnZoomIn(target);
        }
    }

    public void KillFish()
    {
        shop.moneys += Mathf.RoundToInt(currentFishInZoom.GetComponent<FishAI>().sellPrice += currentFishInZoom.GetComponent<FishAI>().currentAffection * 1.5f);
        currentFishInZoom.GetComponent<FishAI>().OnDie();
        fishees.Remove(currentFishInZoom);

        OnZoomOut();
    }

    public void FeedTheFish()
    {
        if (currentFishInZoom.GetComponent<FishAI>().currentHunger <= currentFishInZoom.GetComponent<FishAI>().maxHunger && currentFishInZoom.GetComponent<FishAI>().currentHunger + foodValue <= currentFishInZoom.GetComponent<FishAI>().maxHunger)
        {
            currentFishInZoom.GetComponent<FishAI>().EatFood(foodValue);
            feedButton.interactable = false;
            feedTimer = feedTimerSet;
        }
    }

    public void OnFreeCam()
    {
        if (!isInFreeCam)
        {
            freeCam.Priority = 11;
        }
        else
        {
            freeCam.Priority = 8;
        }
        isInFreeCam = !isInFreeCam;
    }

    public void OnZoomIn(GameObject target)
    {
        isInZoom = true;
        currentFishInZoom = target;
        UpdateZoomInCanvas(target);
        otherCanvas.SetActive(false);
        zoomCanvas.SetActive(true);
        shop.fishInfoScreen.SetActive(false);
        zoomInCam.LookAt = target.transform;
        zoomInCam.Follow = target.transform;
        zoomInCam.Priority = 11;
    }

    public void OnZoomOut()
    {
        currentFishInZoom = null;
        isInZoom = false;
        otherCanvas.SetActive(true);
        zoomCanvas.SetActive(false);
        zoomInCam.Follow = null;
        zoomInCam.LookAt = null;
        zoomInCam.Priority = 9;
    }

    public void UpdateZoomInCanvas(GameObject fish)
    {
        fishName.text = fish.GetComponentInChildren<TextMeshProUGUI>().text;
        personality.text = fish.GetComponent<FishAI>().myPersonality;
        personalityDesc.text = fish.GetComponent<FishAI>().myPersDesc;
        speed.text = "Speed: " + Mathf.RoundToInt(fish.GetComponent<FishAI>().speed);

    }
}
