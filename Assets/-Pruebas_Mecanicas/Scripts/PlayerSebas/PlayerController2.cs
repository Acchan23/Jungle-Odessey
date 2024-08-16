using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    enum PlayerStates { IDLE, MOVING, HIT, INVESTIGATING, DEAD };
    public float speed = 7f;
    private PlayerStates playerState = PlayerStates.IDLE;
    public Animator animator;
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenInventory();
        }
        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING)
        {
            Movement();
            if (Input.GetButtonDown("Fire1")) Attack();

            //if (Input.GetButtonDown("Fire3")) ReceiveDamage();
        }
    }

    private void OpenInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        /*if (isInventoryOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }*/
    }

    public void Movement()
    {
        float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float speedy = Input.GetAxis("Vertical") * Time.deltaTime * speed;
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
        transform.position = new Vector3(speedX + position.x, speedy + position.y, position.z);
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }
}
