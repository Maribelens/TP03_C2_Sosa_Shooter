using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FSMManager))]
[RequireComponent(typeof(EnemyDetector))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePoint;

    [Header("Patrol:")]
    [SerializeField] private float patrolRadius = 10f;

    [Header("Combat:")]
    [SerializeField] private EnemyAttackType attackType;
    [SerializeField] private float launchForce = 10f;

    public EnemyDetector Detector { get; private set; }
    public FSMManager FSM { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform FirePoint => firePoint;
    public float PatrolRadius => patrolRadius;

    public EnemyAttackType AttackType => attackType;
    public float LaunchForce => launchForce;

    protected virtual void Awake()
    {
        Detector = GetComponent<EnemyDetector>();
        FSM = GetComponent<FSMManager>();
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        List<StateBase> states = new List<StateBase>
        {
            new IdleState(),
            new AimingState(),
            new ShootingState(),
            new WalkingState(),
            new HurtState(),
            new DeathState()
        };
        FSM.Initialize(animator, this, states);
        //Debug.Log($"FSM inicializado - CurrentState: {FSM.CurrentState?.enemyStateType}");
    }

    protected virtual void Update()
    {
        FSM.Update();
    }
}
