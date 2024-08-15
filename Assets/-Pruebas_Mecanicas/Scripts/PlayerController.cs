using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerStates { IDLE, MOVING, HIT, INVESTIGATING, DEAD };

    [Header("Stats")]
    [Range(0,100)]
    [SerializeField] private int life;
    [Range(0, 100)]
    [SerializeField] private int hunger;
    [Range(0, 100)]
    [SerializeField] private int thirst;
    private readonly int lifeIncrease = 10; 

    [Header("Movement")]
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float speed;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private PlayerStates playerState = PlayerStates.IDLE;

    [Header("Combat")]
    [SerializeField] private BoxCollider2D attackCollider;
    private UIManager uIManager;
    private readonly float offsetColliderX = 0.8f;
    private readonly float offsetColliderY = -0.1f;
    //private int life = 3;


    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        playerAnim = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        attackCollider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING) Movement();
    }

    private void Update()
    {
        if (playerState is PlayerStates.IDLE || playerState is PlayerStates.MOVING)
        {
            if (Input.GetButtonDown("Fire1")) Attack();

            if (Input.GetButtonDown("Fire3")) ReceiveDamage();
        }
    }

    private void Movement()
    {
        playerState = PlayerStates.MOVING;
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector2(hInput, vInput) * speed;
        playerAnim.SetFloat("Walking", Mathf.Abs(playerRb.velocity.magnitude));

        if (hInput != 0 || vInput != 0)
        {
            playerSprite.flipX = hInput < 0;
            attackCollider.offset = !playerSprite.flipX ? new Vector2(offsetColliderX, offsetColliderY) : new Vector2(-offsetColliderX, offsetColliderY);
        }
        else
        {
            playerState = PlayerStates.IDLE;
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

        if (playerState != PlayerStates.DEAD) StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        if (life != 0) return;

        playerRb.velocity *= 0;
        playerState = PlayerStates.DEAD;
        playerAnim.SetTrigger("Death");
        //Invoke(nameof(Die), 1f);
    }

    //private void Die()
    //{
    //    Destroy(this.gameObject);
    //}

    public void Investigate(bool isDialogueActive)
    {
        if (isDialogueActive)
        {
            playerRb.velocity *= 0;
            playerAnim.SetFloat("Walking", 0);
            playerState = PlayerStates.INVESTIGATING;
        }
        else
        {
            playerState = PlayerStates.IDLE;
        }
    }

    public void Eat(int food)
    {
        life += lifeIncrease;
        hunger += food;
    }

    public void Drink(int water)
    {
        thirst += water;
    }
}
