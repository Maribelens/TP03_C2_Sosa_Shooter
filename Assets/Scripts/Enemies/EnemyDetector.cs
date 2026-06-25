using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("Detection:")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask playerLayer;

    public event Action onPlayerEnterRange;
    public event Action onPlayerExitRange;

    public Transform PlayerTransform { get; private set; }
    public bool PlayerInRange { get; private set; }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectionRadius,
            playerLayer
        );

        //Debug.Log($"Hits: {hits.Length} | PlayerLayer value: {playerLayer.value} | PlayerInRange: {PlayerInRange}");

        bool wasInRange = PlayerInRange;
        PlayerInRange = hits.Length > 0;

        if (PlayerInRange)
            PlayerTransform = hits[0].transform;

        if (!wasInRange && PlayerInRange)
            onPlayerEnterRange?.Invoke();
        else if (wasInRange && !PlayerInRange)
            onPlayerExitRange?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
