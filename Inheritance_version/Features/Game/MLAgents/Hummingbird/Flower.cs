using UnityEngine;

/// <summary>Manages a single flower with nectar</summary>
public class Flower : MonoBehaviour
{
    #region Variables

    [Header("Colors")]
    [Tooltip("The color when flower is full of nectar")]
    [SerializeField] Color fullNectarColor = new(1, 0, .3f);

    [Tooltip("The color when flower is empty of nectar")]
    [SerializeField] Color emptyNectarColor = new(.5f, 0f, 1f);

    [Header("Colliders")]
    [Tooltip("The solid collider representing petals")]
    [SerializeField] Collider petalsCollider;

    [Tooltip("The trigger collider representing nectar")]
    [SerializeField] Collider nectarCollider;


    float nectarAmount;


    Material petalsMaterial;

    #endregion

    #region Getters & Helpers

    /// <summary>Amount of the nectar remaining in the flower</summary>
    public float NectarAmount => nectarAmount;

    /// <summary>Whether the flower has any nectar remaining</summary>
    public bool HasNectar => nectarAmount > 0f;


    public Collider NectarCollider => nectarCollider;
    public Collider PetalsCollider => petalsCollider;

    /// <summary>The flower petals material</summary>
    public Material PetalsMaterial => petalsMaterial;


    /// <summary>A vector pointing straight out of the flower</summary>
    public Vector3 FlowerVectorUp => nectarCollider.transform.up;

    /// <summary>The vector position of the nectar collider</summary>
    public Vector3 FlowerCenterPosition => nectarCollider.transform.position;

    #endregion

    #region Functions

    void ActiveColliders(bool active = true){
        petalsCollider.gameObject.SetActive(active);
        nectarCollider.gameObject.SetActive(active);
    }

    void UpdatePetalsColor(){
        Color color = HasNectar ? fullNectarColor : emptyNectarColor;
        petalsMaterial.SetColor("_BaseColor", color);
    }

    void UpdateFlowerState(){
        ActiveColliders(HasNectar);
        UpdatePetalsColor();
    }

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

        UpdateFlowerState();

        return nectarTaken;
    }

    /// <summary>Resets the flower</summary>
    public void ResetFlower(){
        // Refill the nectar
        nectarAmount = 1f;

        UpdateFlowerState();
    }

    private void Awake() {
        // Get material
        var meshRenderer = GetComponent<MeshRenderer>();
        petalsMaterial = meshRenderer.material;
    }

    #endregion
}
