using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new(0, .68f, -1.35f);

    private void Start() {
        if(!player || !player.gameObject.activeSelf) return;
        
        transform.SetParent(player);
        transform.localPosition = offset;
    }
}
