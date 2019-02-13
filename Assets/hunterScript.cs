using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class hunterScript : MonoBehaviour
{
    // public gameController gc;
    public static int hunterState;
    public NavMeshAgent agent;
    public static GameObject bear;
    public float shootingRange;
    public float aimCountDown;
    private float aimCountDownInitial;
    private Transform initialTransform;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        hunterState = 0;
        initialTransform = transform;
        aimCountDownInitial = aimCountDown;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (bear != null)
        {
            dist = Vector3.Distance(bear.transform.position, transform.position);
        }
        if (hunterState == 0)
        {
            agent.SetDestination(initialTransform.position);
        }
        if (hunterState ==1 && bear!=null)
        {
            agent.SetDestination(bear.transform.position);
           
            if (dist < shootingRange)
            {
                hunterState = 2;
            }
        }

        if(hunterState ==2 && bear != null)
        {
            agent.SetDestination(transform.position);
            aimCountDown -= Time.deltaTime;
            if (aimCountDown <= 0)
            {
                Destroy(bear);
                aimCountDown = aimCountDownInitial;
                hunterState = 0;
            }
            if (dist > shootingRange)
            {
                hunterState = 1;
                aimCountDown = aimCountDownInitial;
                
            }
        }

       
    }

  
}
