using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //private int coinsTotal;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] private Sprite disabledHeart;
    [SerializeField] private GameObject textPanel;
    public GameObject TextPanel { get { return textPanel; } }
    [SerializeField] private TMP_Text textDialogue;



    //private void Start()
    //{
    //    Carambola.CoinCollected += AddCoins;
    //}

    //private void AddCoins(int coinValue)
    //{
    //    coinsTotal += coinValue;
    //    coinsText.text = coinsTotal.ToString();
    //}

    public void DisableHeart(int index)
    {
        Image heart = hearts[index].GetComponent<Image>();
        heart.sprite = disabledHeart;
    }

    public void ShowTexts(string text)
    {
        textDialogue.text = text.ToString();
    }

}
