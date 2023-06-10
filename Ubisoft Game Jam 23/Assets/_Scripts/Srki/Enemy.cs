using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public EnemyHealth Health;
    public EnemyMovement Movement;

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [Space(10)]
    [Header("Enemy SOs")]
    [SerializeField] private float agroRange = 20f;
    public float AgroRange { get => agroRange; }

    [SerializeField] private float attackRange;
    public float AttackRange { get => attackRange; }

    [SerializeField] private int attackDamage;
    public int AttackDamage { get => attackDamage; }

    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get => walkSpeed; }

    [SerializeField] private float runSpeed;
    public float RunSpeed { get => runSpeed; }

    private void Awake()
    {
        Health = GetComponent<EnemyHealth>();
        Movement = GetComponent<EnemyMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    //public void SetAnimatorParameter(string parameter, int value)
    //{
    //    animator.SetInteger(parameter, value);
    //}

    ////private void Die(Vector3 Position)
    ////{
    ////    Movement.StopMoving();
    ////    PainResponse.HandleDeath();
    ////}

    //private void OnDisable()
    //{
    //    Health.OnTakeDamage -= PainResponse.HandlePain;
    //    //Health.OnDeath -= Die;
    //}
}
