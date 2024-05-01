using UnityEngine;
using UnityEngine.AI;

public class DinoInterpreter : MonoBehaviour
{
    public DinoData dinoData; 
    private NavMeshAgent agent;
    private Transform playerTransform;
    private bool isPlayerInRange;
    private float currentStamina;
    private Animator myAnim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentStamina = dinoData.Stamina;
        myAnim = GetComponent<Animator>();
        agent.speed = dinoData.Velocity;

        GetComponent<SphereCollider>().radius = dinoData.DetectionZone;

        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            agent.SetDestination(playerTransform.position);
            myAnim.SetBool("isWalking", true);
            print("CAMINANDO");
        }

        currentStamina -= Time.deltaTime;

        if (currentStamina <= 0)
        {
            agent.isStopped = true;
            myAnim.SetBool("isWalking", false);
            print("ALTO");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            myAnim.SetBool("isWalking", true);
            print("CAMINANDO");
        }

        foreach (GameObject target in dinoData.HostileTowards)
        {
            if (other.gameObject == target)
            {
                NavMeshAgent targetAgent = target.GetComponent<NavMeshAgent>();
                if (targetAgent != null)
                {
                    targetAgent.SetDestination(transform.position);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
