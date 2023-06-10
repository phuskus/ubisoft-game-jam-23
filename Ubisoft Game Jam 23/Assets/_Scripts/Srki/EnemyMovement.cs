using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] private float StillDelay = 1f;
    private LookAtIK LookAt;
    private NavMeshAgent Agent;

    private static NavMeshTriangulation Triangulation;

    private int playerLayer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
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
            enemy.Animator.SetInteger("Movement", 2); // 2 is for running
            Agent.speed = enemy.RunSpeed;

            if (Vector3.Distance(transform.position, playerCollider[0].transform.position) <= enemy.AttackRange)
            {
                // Attack animation
                enemy.Animator.Play("Cube Attack");

                // Prevent agent from moving
                Agent.isStopped = true;

                //this.enabled = false;
                //Agent.speed = 0;
            }
            else
            {
                Agent.isStopped = false;
            }
        }
    }

    private IEnumerator Roam()
    {
        while (enabled)
        {
            int index = Random.Range(1, Triangulation.vertices.Length);
            Agent.SetDestination(
                Vector3.Lerp(
                    Triangulation.vertices[index - 1],
                    Triangulation.vertices[index],
                    Random.value
                ));

            print(Agent.remainingDistance);

            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Agent.speed = 0;
                enemy.Animator.SetInteger("Movement", 0); // 0 is for idle
                yield return new WaitForSeconds(StillDelay);
            }

            Agent.speed = enemy.WalkSpeed;
            enemy.Animator.SetInteger("Movement", 1); // 1 is for walking
            yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
        }
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        Agent.isStopped = true;
        Agent.enabled = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, enemy.AgroRange);
    //    Gizmos.DrawWireSphere(transform.position, enemy.AttackRange);
    //}

    // called as animation event
    private void EnableMovement()
    {
        this.enabled = true;
    }
}