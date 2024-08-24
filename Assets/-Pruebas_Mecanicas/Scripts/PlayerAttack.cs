using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator animator;

    
   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetAttackDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy got hit");
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            Vector2 distance = collision.gameObject.transform.position - transform.position;
            enemy.TakeHit(distance, 3);
        }
    }

    public void SetAttackDirection()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector2 mouseDirection = (mouseWorldPosition - transform.position).normalized;

        animator.SetFloat("AttackX", mouseDirection.x);
        animator.SetFloat("AttackY", mouseDirection.y);

    // Configura la posición del collider de ataque según la dirección
    float attackOffset = 0.65f;
    if (Mathf.Abs(mouseDirection.x) > Mathf.Abs(mouseDirection.y))
    {
        attackCollider.offset = new Vector2(mouseDirection.x > 0 ? attackOffset : -attackOffset, 0);
    }
    else
    {
        attackCollider.offset = new Vector2(0, mouseDirection.y > 0 ? attackOffset : -attackOffset);
    }

        StartCoroutine(ResetAttackCollider());
    }


    private IEnumerator ResetAttackCollider()
    {
        yield return new WaitForSeconds(0.1f); // Ajusta el tiempo según sea necesario
        attackCollider.offset = Vector2.zero; // Restablece el offset del collider
    }

}
