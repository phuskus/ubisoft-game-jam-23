using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Animator Animator;

    [SerializeField] private float StillDelay = 1f;
    private LookAtIK LookAt;
    private NavMeshAgent Agent;

    //private const string IsWalking = "IsWalking";

    private static NavMeshTriangulation Triangulation;

    private int playerLayer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        LookAt = GetComponent<LookAtIK>();
        if (Triangulation.vertices == null || Triangulation.vertices.Length == 0)
        {
            Triangulation = NavMesh.CalculateTriangulation();
        }
    }

    private void Start()
    {
        StartCoroutine(Roam());

        //if (enemy.AttackSO == null || enemy.AgroSO == null) print("Needed SOs missing!");


        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        //Animator.SetInteger("Movement", 1); // 1 is for walking

        if (LookAt != null)
        {
            LookAt.lookAtTargetPosition = Agent.steeringTarget + transform.forward;
        }

        Collider[] playerCollider = Physics.OverlapSphere(transform.position, enemy.AgroRange, 1 << playerLayer);

        if (playerCollider.Length != 0
            && Agent.enabled
            )
        {
            Agent.destination = playerCollider[0].transform.position;

            // run towards the player
            Animator.SetInteger("Movement", 2); // 2 is for running
            Agent.speed = enemy.RunSpeed;

            if (Vector3.Distance(transform.position, playerCollider[0].transform.position) <= enemy.AttackRange)
            {
                // attack animation
                Animator.Play("Attacking");

                // prevent agent from moving
                this.enabled = false;
                //Agent.speed = 0;
            }
        }
    }

    private IEnumerator Roam()
    {
        WaitForSeconds wait = new WaitForSeconds(StillDelay);

        while (enabled)
        {
            int index = Random.Range(1, Triangulation.vertices.Length);
            Agent.SetDestination(
                Vector3.Lerp(
                    Triangulation.vertices[index - 1],
                    Triangulation.vertices[index],
                    Random.value
                ));

            Agent.speed = enemy.WalkSpeed;
            Animator.SetInteger("Movement", 1); // 1 is for walking

            yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
            yield return wait;
        }
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        Agent.isStopped = true;
        Agent.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.AgroRange);
        Gizmos.DrawWireSphere(transform.position, enemy.AttackRange);
    }

    // called as animation event
    private void EnableMovement()
    {
        this.enabled = true;
    }    
}