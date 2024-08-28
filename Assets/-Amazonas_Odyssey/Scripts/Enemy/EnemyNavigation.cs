using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    protected enum EnemyStates { PATROL, WAITING, CHASE, ATTACK, FLEE, DIE };
    

    protected EnemyStats enemyStats;
    protected Transform target;
    protected NavMeshAgent agent;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 originalPos;
    [SerializeField] protected Transform[] wayPoints;
    protected int currentWayPoint;
    protected float minimumDistance;
    protected EnemyStates state;
    protected Transform player;    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = GetComponent<EnemyStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPos = transform.position;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.radius = enemyStats.CollisionRadius;
        minimumDistance = enemyStats.CollisionRadius;
    }

    private void OnEnable()
    {
        state = EnemyStates.PATROL;
        transform.position = originalPos;
        wayPoints = GetComponentInParent<PatrolPoints>().wayPointsArray;
        PickNewWayPoint();
        //if (wayPoints.Length > 0)
        //{
        //    PickNewDestination();
        //}
    }

    private void Update()
    {
        //transform.position = new(transform.position.x, transform.position.y, 0);        
        NextMove();


    }

   
    protected void NextMove()
    {
        switch (state)
        {
            case EnemyStates.PATROL:
                Patrol();
                break;
            case EnemyStates.CHASE:
                ChasePlayer();
                break;
            case EnemyStates.DIE:
                break;
            default:
                break;
        }
    }

    protected void Patrol()
    {
        target = wayPoints[currentWayPoint];

        agent.speed = enemyStats.Speed;
        agent.SetDestination(target.position);

        if (agent.remainingDistance < minimumDistance)
        {
            PickNewWayPoint();
            Turn();
        }
    }
    protected void PickNewWayPoint()
    {
        int lastWayPoint = currentWayPoint;

        while (lastWayPoint == currentWayPoint)
        {
            currentWayPoint = Random.Range(0, wayPoints.Length);
        }
    }
    public void ChasePlayer()
    {
        target = player;

        if (state != EnemyStates.CHASE)
        {
            state = EnemyStates.CHASE;
        }

        agent.speed = enemyStats.Speed;
        agent.SetDestination(target.position);
        Turn();

        //if (transform.parent.GetComponent<NavigatorManager>().navigationModifier != null){}
    }

    private void Turn()
    {
        if (enemyStats.Species == Species.CAPIBARA) return;

        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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
        agent.speed = 0;
        float recoil = 3.5f;
        int damage = enemyStats.Damage;
        Vector2 distance = collision.gameObject.transform.position - transform.position;
        PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();

        player.TakeHit(distance, damage);
        enemyStats.GetPushedBack(distance, recoil);

        state = EnemyStates.CHASE;

        yield return new WaitForSecondsRealtime(0.75f);
        agent.speed = enemyStats.Speed;
    }


}
