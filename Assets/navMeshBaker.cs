using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshBaker : MonoBehaviour
{
    private bool timeToBake;
    public static int bakeNumber;
    public int bakeFrequency;
    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        bakeNumber = 0;
        surface.BuildNavMesh();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bakeNumber >= bakeFrequency)
        {
            timeToBake = true;
        }
        if (timeToBake)
        {
            surface.BuildNavMesh();
            print("cheech");
            timeToBake = false;
            bakeNumber = 0;
        }
        
    }

}
