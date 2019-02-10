using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squirrelScript : MonoBehaviour {
    public bool activated;
    public bool moving;
    public float moveSpeed;
    public Vector3 target;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (activated){
            target = new Vector3(objectInteraction.target.x, transform.position.y, objectInteraction.target.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed*Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        if (!activated)
        {
            rend.material.color = Color.white;
        }
	}

    public void clicked()
    {
        if (!activated){
            activated = true;
            objectInteraction.target = transform.position;
        }
        rend.material.color = Color.red;
        Debug.Log(activated);
    }

   
}
