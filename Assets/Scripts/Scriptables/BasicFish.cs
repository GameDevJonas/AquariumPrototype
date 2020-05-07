using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fishes", menuName = "Fishes/BasicFish", order = 1)]
public class BasicFish : ScriptableObject
{
    public string name;

    public GameObject daFish;

    public enum Personalities { Gay, Creative, Angry, Energetic, Nerdy};
}
