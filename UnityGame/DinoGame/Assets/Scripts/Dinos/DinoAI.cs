using UnityEngine.AI;
using UnityEngine;

public class DinoAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    private Animator myAnim;

    //Colliders/triggers
    public MeshCollider body;
    //public GameObject attackPoint;
    public BoxCollider attackCollider;

    //Patrullaje
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    
    //Estados
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    private void Awake()
    {
        //attackCollider = attackPoint.GetComponent<BoxCollider>();
        //attackPoint = GetComponent<GameObject>();
        body = GetComponent<MeshCollider>();
        myAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSight&& !playerInAttackRange)
        {
            Chasing();
        }

        if (playerInAttackRange && playerInSight)
        {
            Attacking();
        }
    }

    public void Patroling()
    {
        myAnim.SetBool("isWalking", true);
        print("Patrolling");

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);
        print("Searchingwalkpoint");

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z+randomZ);

        if (Physics.Raycast(walkPoint, - transform.up, 2, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        myAnim.SetBool("isWalking", true);
        agent.SetDestination(player.position);
        print("Chasing");
    }

    private void Attacking() //no se como usar esto jajajaj xd
    {
        agent.SetDestination(transform.position);
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isAttacking", true);
        print("Attacking");
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
        agent.isStopped = true;
        Destroy(gameObject, 3);
        myAnim.SetBool("isDead", true);
        body.enabled = false;
        print("Dead");

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
