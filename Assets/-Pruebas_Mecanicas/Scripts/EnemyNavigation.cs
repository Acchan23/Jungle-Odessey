using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void LateUpdate()
    {
        agent.SetDestination(target.position);
    }
}
