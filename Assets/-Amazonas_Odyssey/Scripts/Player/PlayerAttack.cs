
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackCollider;
    //[SerializeField] private Camera mainCamera;

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

    //public void SetAttackDirection()
    //{
    //    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    //    mouseWorldPosition.z = 0;
    //    Vector2 mouseDirection = (mouseWorldPosition - transform.position).normalized;
    //    float attackOffset = .65f;
    //    attackCollider.offset = mouseDirection * attackOffset;
    //    StartCoroutine(ResetAttackCollider());
    //}


    //IEnumerator ResetAttackCollider()
    //{
    //    yield return new WaitForSeconds(.25f);
    //    attackCollider.offset = Vector2.zero;        
    //}
}