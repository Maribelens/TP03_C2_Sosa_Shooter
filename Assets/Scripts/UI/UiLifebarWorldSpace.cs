using UnityEngine;

public class UiLifebarWorldSpace : MonoBehaviour
{
    [SerializeField] private CameraReferenceSo cameraReference;

    private Transform _targetTransform;
    private Vector3 _localOffset;

    private void Awake()
    {
        _targetTransform = transform.parent;
        _localOffset = transform.position - _targetTransform.position;
    }

    private void LateUpdate()
    {
        if (_targetTransform == null) return;
        if (cameraReference.CameraTransform == null) return;

        transform.position = _targetTransform.position + _localOffset;
        transform.rotation = cameraReference.CameraTransform.rotation;
    }
}
