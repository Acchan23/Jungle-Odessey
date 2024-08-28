using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private GameObject damagePopupPrefab;
    private ObjectPooler objectPooler;
    private Rigidbody2D enemyRb;
    private Collider2D enemyCollider;

    private int damage;
    private float speed;
    private float life;
    private int lootQuantity;
    private float collisionRadius; 
    public Species species;

    public float Speed { get { return speed; } }
    public int Damage { get { return damage; } }
    public float CollisionRadius { get { return collisionRadius; } }

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        SetEnemyStats();
    }

    void Start()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void SetEnemyStats()
    {
        damage = data.damage;
        speed = data.speed;
        life = data.life;
        collisionRadius = data.collisionRadius;
        species = data.species;
        lootQuantity = data.lootQuantity;
        
    }

    public void TakeHit(Vector2 direction, int damageTaken)
    {
        life -= damageTaken;

        CreateDamagePopup(damageTaken);

        if (life > 0)
        {
            GetPushedBack(direction);
        }
        else
        {
            StartCoroutine(SpawnLoot("Bistec"));
        }
    }

    public void GetPushedBack(Vector2 direction)
    {
        float pushbackForce = 5f;
        speed = 0;
        enemyCollider.enabled = false;
        enemyRb.velocity = direction * pushbackForce;
        StartCoroutine(Recover());
    }

    public void GetPushedBack(Vector2 distance, float recoil)
    {
        speed = 0;
        enemyCollider.enabled = false;
        enemyRb.velocity = -distance * recoil;
        StartCoroutine(Recover());
    }

    private IEnumerator SpawnLoot(string loot)
    {
        Vector3 droppingSite = transform.position;
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < lootQuantity; i++)
        {
            objectPooler.SpawnFromPool(loot, droppingSite, transform.rotation);
            droppingSite += new Vector3(0.2f,0,0); 
            
        }
        gameObject.SetActive(false);
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        enemyRb.velocity = Vector2.zero;
        speed = data.speed;
        enemyCollider.enabled = true;
    }

    private void CreateDamagePopup(int damage)
    {
        GameObject damagePopupObj = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupObj.GetComponent<DamagePopup>();
        damagePopup.Setup(damage);
    }
}
