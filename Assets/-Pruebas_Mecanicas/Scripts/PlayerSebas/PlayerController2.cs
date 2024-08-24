using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController2 : MonoBehaviour
{
    enum PlayerStates { IDLE, MOVING, ATTACKING, HIT, CHECKING, DEAD };

    private float speed = 6f;
    private PlayerStats stats;
    [SerializeField] private PlayerAttack attackCollider;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private GameObject damagePopupPlayerPrefab;
    private PlayerStates playerState = PlayerStates.IDLE;
    public Animator animator;
    //private SpriteRenderer playerSprite;
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;


    private void Awake()
    {
        //playerSprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
        inventoryPanel.SetActive(false);
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenInventory();
        }
        AdjustSpeedToHealth();

        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING)
        {
            Move();
            if (Input.GetButtonDown("Fire1")) Attack();
        }
    }

    private void OpenInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void AdjustSpeedToHealth()
    {
        if (stats.lifeCur >= 7)
        {
            speed = 6f;
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
    private void Attack()
    {
        playerState = PlayerStates.ATTACKING;
        attackCollider.SetAttackDirection();
        //animator.SetTrigger("attack");
        //animator.SetFloat("AttackX", mouseDirection.x);
        //animator.SetFloat("AttackY", mouseDirection.y);
        playerState = PlayerStates.MOVING;
    }

    private void Move()
    {
        playerState = PlayerStates.MOVING;
        float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float speedY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        animator.SetFloat("MovementX", speedX);
        animator.SetFloat("MovementY", speedY);

        if (speedX != 0 || speedY != 0)
        {
            animator.SetFloat("LastX", speedX);
            animator.SetFloat("LastY", speedY);
        }

        if (speedX == 0 && speedY == 0)
        {
            playerState = PlayerStates.IDLE;
        }
        //if (speedX < 0)
        // {
        //playerSprite.flipX = true;
        //   transform.localScale = new Vector3(-1, 1, 1);
        //}

        //if (speedX > 0)
        //{
        //playerSprite.flipX = false;
        //   transform.localScale = new Vector3(1, 1, 1);
        //}


        Vector3 position = transform.position;
        transform.position = new Vector3(speedX + position.x, speedY + position.y, position.z);
    }
    public void TakeHit(Vector2 distance, int damageTaken)
    {
        float pushbackForce = 7f;
        playerState = PlayerStates.HIT;
        stats.LoseLife(damageTaken);
        playerRb.velocity = distance * pushbackForce;
        CreateDamagePopup(damageTaken * -1);
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        playerRb.velocity *= 0;
        playerState = PlayerStates.MOVING;
    }   

    private void CreateDamagePopup(int damage)
    {
        Vector3 offset = new(0, 0.5f);
        GameObject damagePopupObj = Instantiate(damagePopupPlayerPrefab, transform.position + offset, Quaternion.identity);
        DamagePopupPlayer damagePopup = damagePopupObj.GetComponent<DamagePopupPlayer>();
        damagePopup.Setup(damage);
    }


}
