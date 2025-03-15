using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages a collection of flower plants and attached flowers</summary>
public class FlowerArea : MonoBehaviour
{
    /// <summary>The diameter of the area where the agent and flowers can be used for observing relative distance from agent to flower</summary>
    public const float AreaDiameter = 20f;

    [SerializeField] string flowerPlantTag = "FlowerPlant";

    // The list of all flower plants in this area (flower plants have multiple flowers)
    List<GameObject> flowerPlants = new();

    // A lookup Dictionary for looking up a flower from a nectar collider
    Dictionary<Collider, Flower> nectarFlowerMap = new();

    /// <summary>The list of all flowers in the flower area</summary>
    public List<Flower> Flowers {get; private set;} = new();

    /// <summary>Reset the flowers and flower plants</summary>
    public void ResetFlowers(){
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
        return nectarFlowerMap[collider];
    }

    /// <summary>Recursevily finds all children flowers and flower plants</summary>
    public Flower[] FindChildFlowers(Transform transform){
        for(int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);

            if(child.CompareTag(flowerPlantTag)){
                // Found a flower plant, add to list
                flowerPlants.Add(child.gameObject);

                // Look for flower plants within this flower plant
                FindChildFlowers(child);
            } else {
                // Not a flower plant, look for flower component
                
                if(child.TryGetComponent<Flower>(out var flower))
                {
                    // Found a flower, add it to the flowers list
                    Flowers.Add(flower);

                    // Map nectar collider to flower
                    nectarFlowerMap.Add(flower.NectarCollider, flower);

                    // Note: there are not flowers that are children of other flowers
                } else {
                    // Flower component not found, so check children
                    FindChildFlowers(child);
                }
            }
        }

        return new Flower[0];
    }

    private void Start() {
        FindChildFlowers(transform);
    }
}
