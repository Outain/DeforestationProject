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
    private Vector3 initialTransform;
    private float dist;
    public Animator anim;
    public float rotationSpeed = 5;
   
    // Start is called before the first frame update
    void Start()
    {
        hunterState = 0;
        initialTransform = transform.position;
        print(initialTransform);
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
            agent.SetDestination(initialTransform);
        }
        if (hunterState ==1 && bear!=null)
        {
            anim.SetBool("aiming", false);
            agent.SetDestination(bear.transform.position);
           
            if (dist < shootingRange)
            {
                hunterState = 2;
                agent.SetDestination(transform.position);
            }
        }

        if(hunterState ==2 && bear != null)
        {
            anim.SetBool("aiming",true);
            Vector3 lookAtPoint = new Vector3(bear.transform.position.x, transform.position.y, bear.transform.position.z);
            transform.LookAt(lookAtPoint);
            
            aimCountDown -= Time.deltaTime;
            if (aimCountDown <= 0)
            {
                anim.SetTrigger("fire");
                print("fire!");
                Destroy(bear);
                anim.SetBool("aiming", false);
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
