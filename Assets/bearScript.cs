using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearScript : MonoBehaviour
{
    public Transform bearSpot;
    public float sphereRadius;
    // Start is called before the first frame update
    void Start()
    {
        
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
