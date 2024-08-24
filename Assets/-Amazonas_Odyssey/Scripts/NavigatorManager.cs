using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class NavigatorManager : MonoBehaviour
{
    public NavMeshSurface surface;
    public NavMeshModifier navigationModifier;
    public List<NavMeshModifier> obstaclesInFloor = new();
    [SerializeField] GameObject ground;

    private void Awake()
    {
        surface = transform.GetChild(0).gameObject.GetComponent<NavMeshSurface>();
    }


    private void OnEnable()
    {
        navigationModifier = ground.AddComponent<NavMeshModifier>();
        navigationModifier.overrideArea = true;
        SearchObstacles();
        surface.BuildNavMeshAsync();
    }
    private void OnDisable()
    {
        Destroy(ground.GetComponent<NavMeshModifier>());
        DeleteObstacles();
    }

    private void LateUpdate()
    {
        surface.UpdateNavMesh(surface.navMeshData);
    }

    private void SearchObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        if (obstacles.Length == 0) return;

        foreach (GameObject obstacle in obstacles)
        {
            NavMeshModifier temporalReference;
            if (obstacle.activeInHierarchy)
            {
                temporalReference = obstacle.AddComponent<NavMeshModifier>();
                temporalReference.overrideArea = true;
                temporalReference.area = 1;
                obstaclesInFloor.Add(temporalReference);
            }
        }
    }

    private void DeleteObstacles()
    {
        if (obstaclesInFloor.Count == 0) return;
        for (int i = 0; i < obstaclesInFloor.Count; i++)
        {
            Destroy(obstaclesInFloor[i]);
        }
        obstaclesInFloor.Clear();
    }
}
