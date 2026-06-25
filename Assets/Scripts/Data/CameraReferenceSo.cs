using UnityEngine;

[CreateAssetMenu(fileName = "CameraReference", menuName = "Config/Camera Reference")]
public class CameraReferenceSo : ScriptableObject
{
    public Transform CameraTransform { get; private set; }

    public void Register(Camera camera) => CameraTransform = camera.transform;
    public void Unregister() => CameraTransform = null;
}
