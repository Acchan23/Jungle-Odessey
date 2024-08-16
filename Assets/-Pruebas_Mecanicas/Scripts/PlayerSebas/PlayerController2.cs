using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    enum States { IDLE, MOVING, HIT, INVESTIGATING, DEAD };
    [SerializeField] private float speed = 7f;
    [SerializeField] private PlayerStats stats;
    private States playerState = States.IDLE;
    public Animator animator;
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
            speed = 2.5f;
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

    public void Movement()
    {
        playerState = States.MOVING;
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
}
