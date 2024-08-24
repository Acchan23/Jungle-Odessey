using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private ObjectPooler objectPooler;
    private readonly int minSpawnTime = 3;
    private readonly int maxSpawnTime = 7;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int _spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(_spawnInterval);
            Transform randomSpawnPoint = PickRandomPosition();
            GameObject spawnedEnemy = objectPooler.SpawnFromPool("PoisonFrog", randomSpawnPoint.position, Quaternion.identity);
            spawnedEnemy.transform.SetParent(transform.parent);

        }
    }

    private Transform PickRandomPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];        
    }
}
