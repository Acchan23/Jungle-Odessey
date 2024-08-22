using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController2 : MonoBehaviour
{
    enum PlayerStates { IDLE, MOVING, ATTACKING, HIT, CHECKING, DEAD };
    
    private float speed = 7f;
    private PlayerStats stats;
    [SerializeField] private PlayerAttack attackCollider;
    [SerializeField] private Rigidbody2D playerRb;
    private PlayerStates playerState = PlayerStates.IDLE;
    public Animator animator;
    private SpriteRenderer playerSprite;
    //public GameObject inventoryPanel;
    //private bool isInventoryOpen = false;

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        //inventoryPanel.SetActive(false);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    OpenInventory();
        //}
        AdjustSpeedToHealth();

        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING)
        {
            Move();
            if (Input.GetButtonDown("Fire1")) Attack();
        }
    }

    //private void OpenInventory()
    //{
    //    isInventoryOpen = !isInventoryOpen;
    //    inventoryPanel.SetActive(isInventoryOpen);

    //    /*if (isInventoryOpen)
    //    {
    //        Time.timeScale = 0f;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1f;
    //    }*/
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !(playerState is PlayerStates.HIT))
        {
            TakeHit(collision);
            StartCoroutine(Recover());
        }

    }

    private void TakeHit(Collision2D collision)
    {
        playerState = PlayerStates.HIT;

        float pushbackForce = 8f;
        stats.LoseLife(1);
        Vector2 distance = transform.position - collision.gameObject.transform.position;
        playerRb.velocity = distance * pushbackForce;
        Debug.Log("character hit");
    }

    private void Attack()
    {
        playerState = PlayerStates.ATTACKING;
        //attackCollider.SetAttackDirection();
        animator.SetTrigger("attack");
        playerState = PlayerStates.MOVING;
    }

    private void Move()
    {
        playerState = PlayerStates.MOVING;
        float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float speedY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        animator.SetFloat("movement", speedX * speed);

        if (speedX < 0)
        {
            //playerSprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (speedX > 0)
        {
            //playerSprite.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
        }


        Vector3 position = transform.position;
        transform.position = new Vector3(speedX + position.x, speedY + position.y, position.z);
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        playerRb.velocity *= 0;
        playerState = PlayerStates.MOVING;
    }

    private void AdjustSpeedToHealth()
    {
        if (stats.lifeCur >= 7)
        {
            speed = 7f;
        }
        else if (stats.lifeCur <= 3)
        {
            speed = 3.5f;
        }
        else
        {
            speed = 5f;
        }
    }

    
}
