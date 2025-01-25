using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages a single flower with nectar</summary>
public class Flower : MonoBehaviour
{
    [Tooltip("The color when flower is full of nectar")]
    [SerializeField] Color fullNectarColor = new(1f, 0f, .3f);

    [Tooltip("The color when flower is empty of nectar")]
    [SerializeField] Color emptyNectarColor = new(.5f, 0f, 1f);

    float nectarAmount;

    Collider nectarCollider;
    Collider petalsCollider;
    Material petalsMaterial;

    /// <summary>Amount of the nectar remaining in the flower</summary>
    public float NectarAmount => nectarAmount;

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
}
