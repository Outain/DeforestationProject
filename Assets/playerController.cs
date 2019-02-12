using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public bool activated;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cmo = GameObject.FindWithTag("MainCamera");
        cam = cmo.GetComponent<Camera>();
        print(cam);
        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag != "Interactable")
                    {
                        agent.SetDestination(hit.point);
                    }
                }
            }
        }

        if (!activated)
        {
            rend.material.color = Color.white;
        }
    }

    public void clicked()
    {
        if (!activated)
        {
            activated = true;
            objectInteraction.target = transform.position;
        }
        rend.material.color = Color.red;
        Debug.Log(activated);
    }
}
