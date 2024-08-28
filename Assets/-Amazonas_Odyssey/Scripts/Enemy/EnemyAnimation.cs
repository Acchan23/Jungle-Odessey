using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    private void Update()
    {
        // Obtener la dirección de movimiento del agente
        Vector3 moveDirection = agent.velocity.normalized;

        // Configurar los parámetros del Blend Tree
        animator.SetFloat("MovementX", moveDirection.x);
        animator.SetFloat("MovementY", moveDirection.y);

        // Activar siempre la animación de caminar ya que el enemigo siempre está patrullando
        //animator.SetBool("isWalking", true);
    }
}
