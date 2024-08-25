using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    //enum EnemyStates { PATROL, WAITING, FOLLOW, ATTACK, FLEE, DIE };

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    //public Transform[] wayPoints;
    //private int currentWayPoint;
    //private float minimumDistance = 0.3f;
    //private float waitTime = 1f;
    ////private float cooldown;
    //private EnemyStates state = EnemyStates.PATROL;
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

    private void OnEnable()
    {
        //wayPoints = GetComponentInParent<PatrolPoints>().wayPointsArray;
        //if (wayPoints.Length > 0)
        //{
        //    PickNewDestination();
        //}
    }

    private void Update()
    {
        //transform.position = new(transform.position.x, transform.position.y, 0);
        agent.speed = enemyStats.Speed;
        //MoveTowardsPlayer();

        switch (enemyStats.enemyName)
        {
            case EnemyName.FROG:
                MoveTowardsPlayer();
                break;
            case EnemyName.MOSQUITO:
                MoveTowardsPlayer();
                break;
            case EnemyName.JAGUAR:
                MoveTowardsPlayer();
                break;
            case EnemyName.CAPIBARA:
                break;
            case EnemyName.SNAKE:
                MoveTowardsPlayer();
                break;
            default:
                break;
        }

        
    }

    //private void PickNewDestination()
    //{
    //    currentWayPoint = Random.Range(0, wayPoints.Length);
    //    agent.SetDestination(wayPoints[currentWayPoint].position);
    //}

    //private void KeepPatrolling()
    //{
    //    agent.SetDestination(wayPoints[currentWayPoint].position);
    //    if (agent.remainingDistance < minimumDistance && !agent.pathPending)
    //    {
    //        StartCoroutine(PatrolWait());
    //    }
    //}

    //private IEnumerator PatrolWait()
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    PickNewDestination();
    //}

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(target.position);
        //if (transform.parent.GetComponent<NavigatorManager>().navigationModifier != null)
        //{
        //}
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
        agent.speed = 0;
        PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
        int damage = enemyStats.Damage;
        Vector2 distance = collision.gameObject.transform.position - transform.position;
        player.TakeHit(distance, damage);
        yield return new WaitForSecondsRealtime(1.5f);
        agent.speed = enemyStats.Speed;
    }
}
