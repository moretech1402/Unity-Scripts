using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] Transform berriesContainer;

    int berriesNumber = 5;
    readonly HashSet<GameObject> selectedBerries = new();

    /// <summary>
    /// Choose random number beetween 0 and max berries number
    /// </summary>
    /// <returns>Random number</returns>
    int ChooseRandomNumber() => Random.Range(0, berriesContainer.childCount);

    /// <summary>Assign berries that will be showed</summary>
    void AssignRenderBerries(){
        // Choose random berries locations
        while(selectedBerries.Count < berriesNumber){
            // Choose random berry
            int berryIndex = ChooseRandomNumber();
            var berryGO = berriesContainer.GetChild(berryIndex).gameObject;

            // Add berry to set
            selectedBerries.Add(berryGO);
        }
    }

    #region Render

    /// <summary>Render active berries</summary>
    void RenderBerries(){
        // First disable all berries
        foreach(Transform child in berriesContainer.transform)
            child.gameObject.SetActive(false);
        
        // Active selected berries
        foreach(var berry in selectedBerries)
            berry.SetActive(true);
    }
    
    /// <summary>Set berries number and location of these for this bush</summary>
    void SetBerries(){        
        // Calculate number of berries
        berriesNumber = ChooseRandomNumber();

        // Set visual berries
        AssignRenderBerries();
        RenderBerries();
    }

    #endregion

    #region Life Cycle

    private void Awake() {
        SetBerries();
    }

    #endregion
}
