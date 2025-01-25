using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages a collection of flower plants and attached flowers</summary>
public class FlowerArea : MonoBehaviour
{
    // The diameter of the area where the agent and flowers can be used for observing relative distance from agent to flower
    public const float AreaDiameter = 20f;

    // The list of all flower plants in this area (flower plants have multiple flowers)
    List<GameObject> flowerPlants;

    // A lookup Dictionary for looking up a flower from a nectar collider
    Dictionary<Collider, Flower> nectarFlowerDictionary;

    // The list of all flowers in the flower area
    public List<Flower> Flowers {get; private set;}
}
