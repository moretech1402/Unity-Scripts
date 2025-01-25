using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages a collection of flower plants and attached flowers</summary>
public class FlowerArea : MonoBehaviour
{
    // The diameter of the area where the agent and flowers can be used for observing relative distance from agent to flower
    public const float AreaDiameter = 20f;

    // The list of all flower plants in this area (flower plants have multiple flowers)
    List<GameObject> flowerPlants = new();

    // A lookup Dictionary for looking up a flower from a nectar collider
    Dictionary<Collider, Flower> nectarFlowerDictionary = new();

    /// <summary>The list of all flowers in the flower area</summary>
    public List<Flower> Flowers {get; private set;} = new();

    /// <summary>Reset the flowers and flower plants</summary>
    void ResetFlowers(){
        // Roatate each flower around the y axis and subtly around x and z
        foreach(var flowerPlant in flowerPlants){
            var xRotation = Random.Range(-5f, 5f);
            var yRotation = Random.Range(-180f, 180f);
            var zRotation = Random.Range(-5f, 5f);

            flowerPlant.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        // Reset each flower
        foreach(var flower in Flowers){
            flower.ResetFlower();
        }
    }

    /// <summary>
    /// Gets the <see cref="Flower"/> that a nectar collider belongs to
    /// </summary>
    /// <param name="collider">The nectar collider</param>
    /// <returns>The matching flower</returns>
    public Flower GetFlowerFromNectar(Collider collider){
        return nectarFlowerDictionary[collider];
    }

    public Flower[] FindChildFlowers(){
        return new Flower[0];
    }

    private void Start() {
        FindChildFlowers();
    }
}
