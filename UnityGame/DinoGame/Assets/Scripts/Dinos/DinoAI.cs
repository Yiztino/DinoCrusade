using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class DinoAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    private Animator myAnim;

    // Colliders/triggers
    public MeshCollider body;
    public BoxCollider attackCollider;

    // Patrullaje
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Estados
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    // Evento para cuando el Dino muere
    public delegate void DinoKilled(int points);
    public static event DinoKilled OnDinoKilled;
    private bool isDead = false;

    // Points
    public int Points;

    // Kill Counter
    private DinosaurCounter dinosaurCounter;

    // Audio
    public AudioClip destroySound;
    private AudioSource audioSource;
    private Coroutine patrolCoroutine;

    //Speed
    public float patrolSpeed;
    public float chaseSpeed;


    private void Awake()
    {
        body = GetComponent<MeshCollider>();
        myAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        dinosaurCounter = FindObjectOfType<DinosaurCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSight && !playerInAttackRange)
        {
            Chasing();
        }

        if (playerInAttackRange && playerInSight)
        {
            Attacking();
        }
    }


    private void Patroling()
    {
        myAnim.SetBool("isRunning", false);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", true);

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet && patrolCoroutine == null)
        {
            agent.speed = patrolSpeed; 
            agent.SetDestination(walkPoint);
            patrolCoroutine = StartCoroutine(CheckIfArrived());
        }

        Vector3 distanceToWalk = transform.position - walkPoint;

        if (distanceToWalk.magnitude < 1)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        myAnim.SetBool("isWalking", true);
        myAnim.SetBool("isRunning", false);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isRunning", true);
        agent.speed = chaseSpeed; 
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isRunning", false);
        myAnim.SetBool("isAttacking", true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyDino();
        }
    }

    private void DestroyDino()
    {
        if (!isDead)
        {
            isDead = true;
            agent.isStopped = true;
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isAttacking", false);
            myAnim.SetBool("isDead", true);
            print("im dying");
            body.enabled = false;

            OnDinoKilled?.Invoke(Points);

            int currentPoints = PlayerPrefs.GetInt("TotalPoints", 0);
            currentPoints += Points;
            PlayerPrefs.SetInt("TotalPoints", currentPoints);
            PlayerPrefs.Save();

            Transform mouthTransform = transform.Find("DinosaurMouth");
            if (mouthTransform != null)
            {
                GameObject mouthObject = mouthTransform.gameObject;
                BoxCollider mouthCollider = mouthObject.GetComponent<BoxCollider>();
                if (mouthCollider != null)
                {
                    mouthCollider.enabled = false;
                }
            }

            if (dinosaurCounter != null)
            {
                dinosaurCounter.IncrementKillsCount();
            }

            if (destroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            Destroy(gameObject, 3);
        }
    }

    private IEnumerator CheckIfArrived()
    {
        float waitTime = 6f;
        float elapsedTime = 0f;

        while (elapsedTime < waitTime)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                walkPointSet = false;
                patrolCoroutine = null;
                yield break; 
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        walkPointSet = false;
        SearchWalkPoint();
        patrolCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
