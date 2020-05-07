using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhenDone : Shop
{
    public GameObject fishSpawner;
    public BasicFish nowfish;

    public Collider2D spawnArea;

    public string[] otherNames;

    void Start()
    {
        fishSpawner = GameObject.FindGameObjectWithTag("FishSpawner");
        spawnArea = fishSpawner.GetComponent<Collider2D>();
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
        newFeesh.GetComponent<FishAI>().speed = Random.Range(0.7f, 2.2f);
        newFeesh.GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInChildren<TMP_InputField>().text;
        if(GetComponentInChildren<TMP_InputField>().text == "")
        {
            print("error");
            newFeesh.GetComponentInChildren<TextMeshProUGUI>().text = otherNames[Random.Range(0, otherNames.Length)];
        }
        Destroy(gameObject);
    }
}
