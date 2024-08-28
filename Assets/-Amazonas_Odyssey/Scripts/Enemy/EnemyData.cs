using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Species { FROG, MOSQUITO, JAGUAR, CAPIBARA, SNAKE, CAIMAN }


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public int life;
    public int damage;
    public int lootQuantity;
    public float speed;
    public float collisionRadius;
    public Species species;
}
