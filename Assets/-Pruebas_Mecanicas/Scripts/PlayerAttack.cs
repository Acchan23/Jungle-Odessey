using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private ObjectPooler objectPooler;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Transform lastPosition = collision.transform;
            collision.gameObject.SetActive(false);
            StartCoroutine(SpawnMeat(lastPosition));

        }
    }

    private IEnumerator SpawnMeat(Transform lastPosition)
    {
        Debug.Log("drop loot");
        yield return new WaitForEndOfFrame();
        objectPooler.SpawnFromPool("Bistec", lastPosition.position, lastPosition.rotation);
    }
}
