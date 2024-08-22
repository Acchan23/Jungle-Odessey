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
    public float Speed { get { return speed; } }

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
    }

    public void TakeHit(Vector2 distance, int damageTaken)
    {
        life -= damageTaken;

        CreateDamagePopup(damageTaken * -1);
        
        if (life > 0)
        {
            GetPushedBack(distance);
        }
        else
        {
            StartCoroutine(SpawnLoot("Bistec"));
        }
    }

    private IEnumerator SpawnLoot(string loot)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log($"drop {loot}");
        objectPooler.SpawnFromPool(loot, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void GetPushedBack(Vector2 distance)
    {
        float pushbackForce = 7f;
        enemyCollider.enabled = false;
        speed = 0;
        enemyRb.velocity = distance * pushbackForce;
        StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        enemyRb.velocity *= 0;
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
