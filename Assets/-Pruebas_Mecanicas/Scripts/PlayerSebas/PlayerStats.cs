using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public ParticleSystem lifeEffect;
    public Slider thirst;
    public Slider life;
    public Slider armor;
    public Slider hungry;

    [Range(0, 100)] public int thirstCur;
    [Range(0, 100)] public int lifeCur;
    [Range(0, 100)] public int armorCur;
    [Range(0, 100)] public int hungryCur;

    public float timeRecoveryLife = 3;
    private float timeRecoveryLifeCur = 0;

    [Header("")]
    public float timeLostHungry = 10;
    public float timeLostHungryCur = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        thirst.value = thirstCur;
        life.value = lifeCur;
        armor.value = armorCur;
        hungry.value = hungryCur;

        if (hungry.value == 10 && life.value < 10 && timeRecoveryLifeCur < -0)
        {
            Debug.Log("sanado++");
            timeRecoveryLifeCur = timeRecoveryLife;
        }

        if (timeRecoveryLifeCur > 0)
        {
            timeRecoveryLifeCur -= Time.deltaTime;
            if (timeRecoveryLifeCur <= 0)
                lifeCur += 1;
        }

        if (timeLostHungryCur > 0)
        {
            timeLostHungryCur -= Time.deltaTime;
            if (timeLostHungryCur <= 0)
            {
                hungryCur -= 1;
                timeLostHungryCur = timeLostHungry;
            }
        }
    }

    public void AddLife(int amount)
    {
        Instantiate(lifeEffect, transform.position, Quaternion.identity);
        lifeCur += amount;
    }
}
