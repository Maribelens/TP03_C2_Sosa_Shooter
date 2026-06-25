using UnityEngine;

public class CameraRegister : MonoBehaviour
{
    [SerializeField] private CameraReferenceSo cameraReference;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = GetComponent<Camera>();
    }

    private void OnEnable() => cameraReference.Register(mainCamera);
    private void OnDisable() => cameraReference.Unregister();
}
