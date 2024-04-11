using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform player;
    private NavMeshAgent agent;
    private Animator myAnim;
    private bool isWalking = false; // Nuevo booleano para indicar si el objeto está caminando

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
            // Establecer la posición de destino del agente
            agent.SetDestination(player.position);

            // Activar el booleano isWalking
            if (!isWalking && agent.remainingDistance > agent.stoppingDistance)
            {
                isWalking = true;
                // Activar la animación de caminar
                myAnim.SetBool("isWalking", true);
            }
            else if (isWalking && agent.remainingDistance <= agent.stoppingDistance)
            {
                isWalking = false;
                // Desactivar la animación de caminar
                myAnim.SetBool("isWalking", false);
            }
        }
    }
}
