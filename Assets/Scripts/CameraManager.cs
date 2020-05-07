using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera normalCam;
    public CinemachineVirtualCamera zoomInCam;

    public GameObject otherCanvas;
    public GameObject zoomCanvas;
    public GameObject currentFishInZoom;

    public static GameObject[] fishes;
    public int currentFish;

    public static List<GameObject> fishees = new List<GameObject>();

    public bool isInZoom;

    [Header("Info for Zoom in cam")]
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI personality;
    public TextMeshProUGUI personalityDesc;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI hunger;
    public TextMeshProUGUI affection;

    void Start()
    {
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
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
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

        if(Input.GetKeyDown(KeyCode.Space) && isInZoom)
        {
            currentFishInZoom.GetComponent<FishAI>().OnDie();
            fishees.Remove(currentFishInZoom);
            OnZoomOut();
        }
    }

    public void OnZoomIn(GameObject target)
    {
        isInZoom = true;
        currentFishInZoom = target;
        UpdateZoomInCanvas(target);
        otherCanvas.SetActive(false);
        zoomCanvas.SetActive(true);
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
        speed.text = ("Speed: " + fish.GetComponent<FishAI>().speed);
        //hunger
        //affection
    }
}
