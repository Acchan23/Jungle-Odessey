using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum States { IDLE, MOVING, HIT, INVESTIGATING, DEAD };

public class PlayerController2 : MonoBehaviour
{
    

    private float speed = 7f;
    private float pushbackForce = 10f;
    [SerializeField] private PlayerStats stats;
    private States playerState = States.IDLE;
    public Animator animator;
    [SerializeField] private Rigidbody2D playerRb;
    //public GameObject inventoryPanel;
    //private bool isInventoryOpen = false;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        //inventoryPanel.SetActive(false);
    }

    void Update()
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

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    OpenInventory();
        //}
        if (playerState is States.IDLE || playerState is States.MOVING)
        {
            Movement();
            if (Input.GetButtonDown("Fire1")) Attack();

            //if (Input.GetButtonDown("Fire3")) ReceiveDamage();
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
        if (collision.gameObject.CompareTag("Enemy") && !(playerState is States.HIT))
        {
            ChangePlayerState(States.HIT);
            Debug.Log("character hit");
            stats.LoseLife(1);
            Vector2 distance = transform.position - collision.gameObject.transform.position;
            playerRb.velocity = distance * pushbackForce;
        }

        Invoke(nameof(RegainControl), 0.5f);

    }

    private void RegainControl()
    {
        playerRb.velocity *= 0;
        ChangePlayerState(States.MOVING);
    }
    private void Movement()
    {
        ChangePlayerState(States.MOVING);
        float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float speedY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        animator.SetFloat("movement", speedX * speed);

        if (speedX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (speedX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 position = transform.position;
        transform.position = new Vector3(speedX + position.x, speedY + position.y, position.z);
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void ChangePlayerState(States state)
    {
        playerState = state;
    }
}
