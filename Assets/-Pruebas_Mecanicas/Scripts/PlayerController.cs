using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float speed;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private bool isDead;


    [Header("Combat")]
    [SerializeField] BoxCollider2D attackCollider;
    [SerializeField] UIManager uIManager;
    private readonly float offsetColliderX = 0.8f;
    private readonly float offsetColliderY = -0.1f;
    private int life = 3;


    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        attackCollider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (!isDead) Movement();
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Fire1")) Attack();

            //if (Input.GetButtonDown("Fire3")) ReceiveDamage();
        }
    }

    private void Movement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector2(hInput, vInput) * speed;
        playerAnim.SetFloat("Walking", Mathf.Abs(playerRb.velocity.magnitude));

        if (hInput != 0 || vInput != 0)
        {
            playerSprite.flipX = hInput < 0;
            attackCollider.offset = !playerSprite.flipX ? new Vector2(offsetColliderX, offsetColliderY) : new Vector2(-offsetColliderX, offsetColliderY);
        }
    }

    private void Attack()
    {
        playerAnim.SetTrigger("Attack");
    }

    private void ReceiveDamage()
    {
        if (life > 0)
        {
            life--;
            Debug.Log(life);
            uIManager.DisableHeart(life);
        }

        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        if (life != 0) return;

        playerRb.velocity *= 0;
        isDead = true;
        playerAnim.SetTrigger("Death");
        Invoke(nameof(Die), 1f);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
