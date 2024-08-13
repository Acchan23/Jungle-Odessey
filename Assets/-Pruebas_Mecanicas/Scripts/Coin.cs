using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void SumCoins(int coin);
    public static event SumCoins CoinCollected;

    [SerializeField] private int coinValue;
    private BoxCollider2D coinCollider;

    private void Awake()
    {
        coinCollider = GetComponentInChildren<BoxCollider2D>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (CoinCollected == null) return;

    //        AddCoin();
    //        Destroy(this.gameObject);
    //    }
    //}

    private void AddCoin()
    {
        CoinCollected(coinValue);
    }
}
