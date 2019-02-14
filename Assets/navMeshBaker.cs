using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshBaker : MonoBehaviour
{
    public static bool timeToBake;
    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        surface.BuildNavMesh();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToBake)
        {
            surface.BuildNavMesh();
            print("cheech");
            timeToBake = false;
        }
        
    }

}
