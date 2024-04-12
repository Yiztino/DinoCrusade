using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform player;
    private NavMeshAgent agent;
    private Animator myAnim;
    private bool isWalking = false; 


    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        myAnim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag " + playerTag);
        }
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on object.");
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;

            Vector3 targetPosition = player.position - directionToPlayer.normalized * 4f;

            agent.SetDestination(targetPosition);

            if (!isWalking && agent.remainingDistance > agent.stoppingDistance)
            {
                isWalking = true;
                myAnim.SetBool("isWalking", true);
                myAnim.SetBool("isAttacking", false);
            }
            else if (isWalking && agent.remainingDistance <= agent.stoppingDistance)
            {
                isWalking = false;
                myAnim.SetBool("isWalking", false);
                myAnim.SetBool("isAttacking", true);
            }
        }
    }

    public void StopMoving()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }
}
