using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject hintVictoryCondition;


    private void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inventory.HasEnoughWood())
            {
                GameManager.Instance.Victory();
            }
            else
            {
                hintVictoryCondition.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintVictoryCondition.SetActive(false);
        }
    }
}
