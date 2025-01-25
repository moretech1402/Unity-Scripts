using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages a single flower with nectar</summary>
public class Flower : MonoBehaviour
{
    #region Variables
    [Tooltip("The color when flower is full of nectar")]
    [SerializeField] Color fullNectarColor = new(1f, 0f, .3f);

    [Tooltip("The color when flower is empty of nectar")]
    [SerializeField] Color emptyNectarColor = new(.5f, 0f, 1f);

    float nectarAmount;

    Collider nectarCollider;
    Collider petalsCollider;
    Material petalsMaterial;

    #endregion

    #region Getters & Helpers

    /// <summary>Amount of the nectar remaining in the flower</summary>
    public float NectarAmount => nectarAmount;

    /// <summary>Whether the flower has any nectar remaining</summary>
    public bool HasNectar => nectarAmount > 0f;


    /// <summary>The trigger collider representing nectar</summary>
    public Collider NectarCollider => nectarCollider;

    /// <summary>The solid collider representing petals</summary>
    public Collider PetalsCollider => petalsCollider;

    /// <summary>The flower petals material</summary>
    public Material PetalsMaterial => petalsMaterial;


    /// <summary>A vector pointing straight out of the flower</summary>
    public Vector3 FlowerVectorUp => nectarCollider.transform.up;

    /// <summary>The vector position of the nectar collider</summary>
    public Vector3 FlowerCenterPosition => nectarCollider.transform.position;

    #endregion

    #region Functions

    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">The amount of nectar to remove</param>
    /// <returns>The actual amount successfully removed</returns>
    public float Feed(float amount){
        // Track how much nectar was successfullt taken (cannot take more than is available)
        var nectarTaken = Mathf.Clamp(amount, 0, nectarAmount);

        // Subtract nectar
        nectarAmount -= nectarTaken;

        if(!HasNectar){
            // Disable colliders
            petalsCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            // Change petals color to indicates that not nectar remaining
            petalsMaterial.color = emptyNectarColor;
        }
        return 0;
    }

    #endregion
}
