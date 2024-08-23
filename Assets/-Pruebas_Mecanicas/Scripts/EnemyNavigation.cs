using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    enum EnemyStates { PATROL, FOLLOW, ATTACK, FLEE, DIE };

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    //private float cooldown;
    private EnemyStates state = EnemyStates.PATROL;
    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        switch (state)
        {
            case EnemyStates.PATROL:
                agent.speed = enemyStats.Speed;
                MoveTowardsPlayer();
                break;
            case EnemyStates.FOLLOW:
                agent.speed = enemyStats.Speed;
                MoveTowardsPlayer();
                break;
            case EnemyStates.ATTACK:
                agent.speed = 0;
                break;
            case EnemyStates.FLEE:
                agent.speed = enemyStats.Speed;
                break;
            case EnemyStates.DIE:
                break;
            default:
                break;
        }

        
    }
    private void MoveTowardsPlayer()
    {
        if (transform.parent.GetComponent<NavigatorManager>().navigationModifier != null)
        {
            agent.SetDestination(target.position);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackSequence(collision));
        }
    }

    IEnumerator AttackSequence(Collision2D collision)
    {
        state = EnemyStates.ATTACK;
        PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
        int damage = enemyStats.Damage;
        Vector2 distance = collision.gameObject.transform.position - transform.position;
        player.TakeHit(distance, damage);
        yield return new WaitForSecondsRealtime(1.0f);
        state = EnemyStates.FOLLOW;
    }
}
