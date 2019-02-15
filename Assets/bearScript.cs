using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bearScript : MonoBehaviour
{
    public Transform bearSpot;
    public float sphereRadius;
    public float maxSpeed;
    public float minSpeed;
    public float speedDecay;
    public NavMeshAgent agent; 
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius,layerMask);
        int i = 0;
        while (i < hitColliders.Length&&hitColliders.Length>0)
        {
            hunterScript.hunterState = 1;
            print(hunterScript.hunterState);
            hitColliders[i].SendMessage("Lockdown");
            i++;
        }
        if(agent.speed >= minSpeed)
        {
            agent.speed -= speedDecay*Time.deltaTime;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "human")
        {
            other.gameObject.SendMessage("Lockdown");
            
            print("lockdown");
            gameController.hunterActivated = true;
        }
    }
}
