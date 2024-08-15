using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private void Awake()
    {
        instance = this;
    }

    public enum ItemType
    {
        none,
        log,
        rock,
        meat
    }

    public void EmptySlot()
    {

    }
}
