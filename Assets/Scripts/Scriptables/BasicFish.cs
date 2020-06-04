using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fishes", menuName = "Fishes/BasicFish", order = 1)]
public class BasicFish : ScriptableObject
{
    public string fishName;

    public string description;

    public GameObject daFish;

    public int cost;

    public float minSpeed, maxSpeed;
    public float maxHunger;
    public float hungerRate;
    public float affectionRate;

    public int baseSellPrice;

    public enum Personalities { Gay, Creative, Angry, Energetic, Nerdy};
}
