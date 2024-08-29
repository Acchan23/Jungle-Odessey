using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public Slider thirst;
    public Slider life;
    public Slider hungry;

    [Range(0, 10)] public int thirstCur;
    [Range(0, 10)] public int lifeCur;
    [Range(0, 10)] public int hungryCur;

    private float timeRecoveryLife = 2;
    private float timeRecoveryLifeCur = 0;

    private float timeLostHungry = 10;
    private float timeLostHungryCur = 0;

    private float timeLostThirst = 10;
    private float timeLostThirstCur = 0;

    private float timeLostLife = 10;
    private float timeLostLifeCur = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateStats();
        timeLostHungryCur = timeLostHungry;
        timeLostThirstCur = timeLostThirst;
        timeLostLifeCur = timeLostLife;
    }

    void Update()
    {
        UpdateStats();

        // Lógica para recuperar vida si el hambre está llena
        if (hungry.value == 10 && life.value < 10 && timeRecoveryLifeCur <= 0)
        {
            timeRecoveryLifeCur = timeRecoveryLife;
        }

        if (timeRecoveryLifeCur > 0)
        {
            timeRecoveryLifeCur -= Time.deltaTime;
            if (timeRecoveryLifeCur <= 0)
                lifeCur += 1;
        }

        // Lógica para disminuir el hambre
        if (timeLostHungryCur > 0)
        {
            timeLostHungryCur -= Time.deltaTime;
            if (timeLostHungryCur <= 0)
            {
                if (hungryCur > 0)
                {
                    hungryCur -= 1;
                }
                timeLostHungryCur = timeLostHungry;
            }
        }

        // Lógica para disminuir la sed si el hambre es cero
        if (hungryCur == 0 && timeLostThirstCur > 0)
        {
            timeLostThirstCur -= Time.deltaTime;
            if (timeLostThirstCur <= 0)
            {
                if (thirstCur > 0)
                {
                    thirstCur -= 1;
                }
                timeLostThirstCur = timeLostThirst;
            }
        }

        // Lógica para disminuir la vida si el hambre y la sed son cero
        if (hungryCur == 0 && thirstCur == 0 && timeLostLifeCur > 0)
        {
            timeLostLifeCur -= Time.deltaTime;
            if (timeLostLifeCur <= 0)
            {
                if (lifeCur > 0)
                {
                    lifeCur -= 1;
                }
                timeLostLifeCur = timeLostLife;
            }
        }
    }

    private void LateUpdate()
    {
        if (lifeCur <= 0) GameManager.Instance.GameOver();
    }

    private void UpdateStats()
    {
        thirst.value = thirstCur;
        life.value = lifeCur;
        hungry.value = hungryCur;
    }

    public void AddLife(int amount)
    {
        lifeCur += amount;
    }

    public void AddHungry(int amount)
    {
        hungryCur += amount;
    }

    public void AddThirst(int amount)
    {
        thirstCur += amount;
    }

    public void LoseLife(int amount)
    {
        lifeCur -= amount;
    }
}
