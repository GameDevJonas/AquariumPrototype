using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishAI : MonoBehaviour
{
    public bool spawned;

    public Vector2 point;

    public enum WhatKindOfFish { basic, not_basic };
    public WhatKindOfFish typeOfFish;

    public Collider2D swimArea;
    public Collider graveYard;

    public float speed;
    public float counter;

    public GameObject graveStone;
    public GameObject[] fishesICanBe;
    public int fishIAm;

    public string[] personalities;
    public string[] personalityDescription;

    public string myPersonality, myPersDesc;

    public CameraManager cameraMan;

    public float hungerRate, affectionRate, maxHunger, currentHunger, currentAffection, sellPrice;
    public int newSellPrice;
    public float affectionTimer, hungerTimer;


    void Start()
    {
        graveYard = GameObject.FindGameObjectWithTag("Graveyard").GetComponent<Collider>();
        int randNumb = Random.Range(0, fishesICanBe.Length);
        fishIAm = randNumb;
        Instantiate(fishesICanBe[randNumb], transform.position, Quaternion.Euler(-90, 0, 90), transform);
        counter = 2f;
        swimArea = GameObject.FindGameObjectWithTag("FishSpawner").GetComponent<Collider2D>();
        cameraMan = GameObject.FindObjectOfType<CameraManager>();
        int rando = Random.Range(0, personalities.Length);
        myPersonality = personalities[rando];
        myPersDesc = personalityDescription[rando];
        currentAffection = 0;
        currentHunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
        switch (typeOfFish)
        {
            case WhatKindOfFish.basic:
                BasicFish();
                break;
            case WhatKindOfFish.not_basic:
                NotBasicFish();
                break;
            default:
                break;
        }
        HungerFunc();
        AffectionFunc();
    }

    void HungerFunc()
    {
        if (currentHunger <= 0)
        {
            OnDie();
            cameraMan.OnZoomOut();
        }

        if (hungerTimer <= 0)
        {
            currentHunger--;
            hungerTimer = hungerRate;
        }
        else
        {
            hungerTimer -= Time.deltaTime;
        }
    }

    void AffectionFunc()
    {
        if (currentHunger >= (maxHunger / 2))
        {
            if (affectionTimer <= 0)
            {
                currentAffection++;
                affectionTimer = affectionRate;
            }
            else
            {
                affectionTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (affectionTimer <= 0)
            {
                currentAffection--;
                affectionTimer = affectionRate;
            }
            else
            {
                affectionTimer -= Time.deltaTime;
            }
        }
    }

    public void EatFood(float food)
    {
        currentHunger += food;
        currentAffection += 2f;
    }

    public void BasicFish()
    {
        if (!spawned)
        {
            point.x = Random.Range(swimArea.bounds.min.x, swimArea.bounds.max.x);
            point.y = Random.Range(swimArea.bounds.min.y, swimArea.bounds.max.y);
        }

        spawned = true;

        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, point, step);
        if (transform.position.x == point.x && transform.position.y == point.y)
        {
            float counterSet = Random.Range(1f, 3f);
            if (counter <= 0)
            {
                counter = counterSet;
                point.x = Random.Range(swimArea.bounds.min.x, swimArea.bounds.max.x);
                point.y = Random.Range(swimArea.bounds.min.y, swimArea.bounds.max.y);
            }
            else
            {
                counter -= Time.deltaTime;
            }
        }

    }

    public void NotBasicFish()
    {

    }

    public void OnDie()
    {
        Vector3 gravePoint;
        gravePoint.x = Random.Range(graveYard.bounds.min.x, graveYard.bounds.max.x);
        gravePoint.y = -6.93f;
        gravePoint.z = Random.Range(graveYard.bounds.min.z, graveYard.bounds.max.z);

        GameObject myGrave = Instantiate(graveStone, gravePoint, Quaternion.identity);
        myGrave.GetComponentInChildren<GraveStoneName>().GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInChildren<TextMeshProUGUI>().text;
        myGrave.GetComponentInChildren<GraveStonePersonality>().GetComponentInChildren<TextMeshProUGUI>().text = myPersonality;
        myGrave.GetComponentInChildren<GraveStoneAffection>().GetComponentInChildren<TextMeshProUGUI>().text = "Affection: " + currentAffection;
        myGrave.GetComponent<MomentumPicker>().momentums[fishIAm].SetActive(true);
        if (currentHunger <= 0)
        {
            myGrave.GetComponentInChildren<GraveStoneReason>().GetComponentInChildren<TextMeshProUGUI>().text = "Died of starvation.";
        }
        else
        {
            myGrave.GetComponentInChildren<GraveStoneReason>().GetComponentInChildren<TextMeshProUGUI>().text = "Died of capitalism.";
        }
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        print("clicked " + GetComponentInChildren<TextMeshProUGUI>().text);
        cameraMan.OnZoomIn(gameObject);
    }
}
