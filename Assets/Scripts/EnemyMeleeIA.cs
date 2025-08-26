using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;

public class EnemyMeleeIA : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Animator animator;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public Vector3 walkPoint;
    bool walkPointSet;
    public bool ded;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectiles;
    public GameObject atackPoint;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool timerStarted = false;

    public float slowDownFactor = 0.2f;
    public float realTimeDuration = 1f;

    public ParticleSystem enemyParticles;

    public UnityEvent particleDed;

    private void Awake()
    {
        player = GameObject.Find("Blitzen").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            animator.SetBool("IsAttacking", false);
            ChasePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (ded == false)
            {
                TriggerSlowMotion();
                TakeDamage(3);
            }
            


        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            timerStarted = false;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            if (!timerStarted)
            {
                StartCoroutine(ResetWalkPointAfterDelay(5f));
                timerStarted = true;
            }
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            timerStarted = false;
        }
    }

    private IEnumerator ResetWalkPointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        walkPointSet = false;
        timerStarted = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectiles, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);
            animator.SetBool("IsAttacking",true);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {

        if (ded == false)
        {
            health -= damage;

            if (health <= 0)
            {
                ded = true;

                particleDed.Invoke();

                Invoke(nameof(DestroyEnemy), 0.5f);
            }
        }
    }

    private void DestroyEnemy()
    {

        Destroy(gameObject);
    }

    public void TriggerSlowMotion()
    {
        StartCoroutine(SlowMotion());
    }

    private IEnumerator SlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(realTimeDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
