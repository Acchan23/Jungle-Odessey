using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    enum EnemyStates { PATROL, FOLLOW, ATTACK, FLEE, DIE};

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        agent.speed = enemyStats.Speed;
        agent.SetDestination(target.position);
    }
}
