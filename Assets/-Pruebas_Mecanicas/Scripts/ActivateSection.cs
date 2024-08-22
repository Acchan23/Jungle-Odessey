using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class ActivateSection : MonoBehaviour
{
    public NavMeshSurface surface;
    private void OnEnable()
    {
        //Debug.Log("baked new navmesh");
        //surface.BuildNavMeshAsync();
    }
}
