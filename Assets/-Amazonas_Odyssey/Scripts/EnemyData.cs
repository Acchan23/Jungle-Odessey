using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName { FROG, MOSQUITO, JAGUAR, CAPIBARA, SNAKE }


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public int life;
    public int damage;
    public float speed;
    public EnemyName enemyName;
    public int lootQuantity;
}
