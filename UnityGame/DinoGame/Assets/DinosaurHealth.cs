using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;

public class DinosaurHealth : MonoBehaviour
{
    public int vidaMaxima;
    private int vidaActual;

    private Animator myAnim;
    private MeshCollider body;

    private FollowPlayer followPlayer;


    private void Start()
    {
        vidaActual = vidaMaxima; 
        myAnim = GetComponent<Animator>();
        body = GetComponent<MeshCollider>();
        followPlayer = GetComponent<FollowPlayer>();
    }

    public void TakeDamage(int cantidadDanio)
    {
        vidaActual -= cantidadDanio; 

        if (vidaActual <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        followPlayer.StopMoving();
        body.enabled = false;
        myAnim.SetBool("isDead", true);

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
        
        Destroy(gameObject, 5);
    }

}
