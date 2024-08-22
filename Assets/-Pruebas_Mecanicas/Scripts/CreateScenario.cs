using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScenario : MonoBehaviour
{
    public GameObject zeroZero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zeroZero.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
