using MC.Core.Unity.Movement;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CameraRelative3DMoveDirectionStrategy",
    menuName = "Core/Movement/CameraRelative3DMoveDirectionStrategy"
    )]
public class CameraRelative3DMoveDirectionStrategy : MoveDirectionStrategySO
{
    public override Vector3 GetDirection(Vector2 input, Camera camera)
    {
        Transform camTransf = camera.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(camTransf.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(camTransf.right, Vector3.up).normalized;
        return (camForward * input.y + camRight * input.x).normalized;
    }
}
