using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController2 : MonoBehaviour
{
    enum PlayerStates { IDLE, MOVING, ATTACKING, HIT, CHECKING, DEAD };

    private readonly float speed = 3;
    private PlayerStats stats;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private GameObject damagePopupPlayerPrefab;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private AudioClip attackSound;
    private PlayerStates playerState = PlayerStates.IDLE;
    public Animator animator;
    private bool isInventoryOpen = false;
    private Vector2 lastMovementDirection = Vector2.zero;


    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        inventoryPanel = GameObject.FindGameObjectWithTag("Inventory");
        inventoryPanel.SetActive(isInventoryOpen);
    }

    void Update()
    {
        //AdjustSpeedToHealth();

        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenInventory();
        }

        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING)
        {
            Move();
            if (Input.GetButtonDown("Fire1")) Attack();
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        Time.timeScale = isInventoryOpen ? 0f : 1f;
    }

    //private void AdjustSpeedToHealth()
    //{
    //    if (stats.lifeCur >= 7)
    //    {
    //        speed = 5f;
    //    }
    //    else if (stats.lifeCur <= 3)
    //    {
    //        speed = 3.5f;
    //    }
    //    else
    //    {
    //        speed = 4f;
    //    }
    //}
    private void Attack()
    {
        AudioManager2.Instance.PlaySFX(attackSound);

        playerState = PlayerStates.ATTACKING;
        animator.SetBool("isAttacking", true);
        animator.SetFloat("AttackX", lastMovementDirection.x);
        animator.SetFloat("AttackY", lastMovementDirection.y);
        float attackOffset = 0.2f;
        if (Mathf.Abs(lastMovementDirection.x) > Mathf.Abs(lastMovementDirection.y))
        {
            attackCollider.offset = new Vector2(lastMovementDirection.x > 0 ? attackOffset : -attackOffset, 0);
        }
        else
        {
            attackCollider.offset = new Vector2(0, lastMovementDirection.y > 0 ? attackOffset : -attackOffset);
        }
        //animator.SetTrigger("attack");
        //animator.SetFloat("AttackX", mouseDirection.x);
        //animator.SetFloat("AttackY", mouseDirection.y);
        //playerState = PlayerStates.MOVING;
        StartCoroutine(EndAttack());
    }

    private IEnumerator EndAttack()
    {
        // Ajusta el tiempo según sea necesario para que la animación de ataque dure lo suficiente
        yield return new WaitForSeconds(0.5f); // Ajusta el tiempo según la duración de la animación
        playerState = PlayerStates.MOVING;
        animator.SetBool("isAttacking", false); // Desactiva el estado de ataque
        attackCollider.offset = Vector2.zero;   // Restablece el offset del collider
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
            lastMovementDirection = new Vector2(speedX, speedY);
            animator.SetFloat("LastX", speedX);
            animator.SetFloat("LastY", speedY);
        }

        if (speedX == 0 && speedY == 0)
        {
            playerState = PlayerStates.IDLE;
            animator.SetBool("isMoving", false);
        }
        else
        {
            playerState = PlayerStates.MOVING;
            animator.SetBool("isMoving", true);
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
        float pushbackForce = 5f;
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
