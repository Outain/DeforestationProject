using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {
    public int behaviourState;
    public float speed;
    public float rotationSpeed = 360f;
    public float loggingSpeed = 5;
    public Transform target, fleeTarget;
    public GameObject currentTree,generator;
    public generatorScript gs;
    public treeScript ts;
    public Rigidbody rb;
    public int pointsLostPerTree = 5;

    //behaviour state numbers
    //0 is for searching initial tree and reset after tree is destroyed
    //1 is for engaging in chopping down
    //2 is for finding generator
    //3 is for repairing generator;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindWithTag("cabin");
        fleeTarget = go.transform;
        rb = GetComponent<Rigidbody>();
        target = FindTarget();
        behaviourState = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null)
        {
            target = FindTarget();
        }
	}

    private void FixedUpdate()
    {
        if (behaviourState == 0)
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

                // Vector3 targetDir = target.position - transform.position;

                // The step size is equal to speed times frame time.
                Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                //Apply the rotation 
                transform.rotation = rot;

                // put 0 on the axys you do not want for the rotation object to rotate
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }

        if(behaviourState == 1&&!gameController.hunterActivated)
        {
            ts.health -= loggingSpeed * Time.deltaTime;
            if(ts.isAlive == false)
            {
                currentTree.tag = "Untagged";
                Destroy(currentTree);
                gameController.forestPower -= pointsLostPerTree;
                target = FindTarget();
                behaviourState = 0;
            }
        }

        if(behaviourState == 2)
        {
            if(generator != null)
            {
                Vector3 direction = (generator.transform.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }

            float distance = (generator.transform.position - transform.position).sqrMagnitude;
            if (distance <= 1.5f)
            {
                gs = generator.GetComponent<generatorScript>();
                behaviourState = 3;
            }
        }

        if(behaviourState == 3)
        {
            gs.Repair();
            if(gs.brokenDown == false)
            {
                
                behaviourState = 0;
            }
        }

        if(behaviourState == 4)
        {
            Vector3 direction = (fleeTarget.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            // Vector3 targetDir = target.position - transform.position;

            // The step size is equal to speed times frame time.
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            //Apply the rotation 
            transform.rotation = rot;

            // put 0 on the axys you do not want for the rotation object to rotate
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }


    }

    public void Lockdown()
    {
        behaviourState = 4;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        if (collision.collider.CompareTag("tree")&&behaviourState ==0)
        {
            behaviourState = 1;
            //collision.collider.gameObject.tag = "Untagged"; // Remove the tag so that FindTarget won't return it
            currentTree = collision.collider.gameObject;
            ts = currentTree.GetComponent<treeScript>();
            //Destroy(collision.collider.gameObject);
            //target = FindTarget();
        }

        else if (collision.collider.CompareTag("generator"))
        {
            gs = generator.GetComponent<generatorScript>();
            behaviourState = 3;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public Transform FindTarget()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("tree");
        float minDistance = Mathf.Infinity;
        Transform closest;

        if (candidates.Length == 0)
            return null;

        closest = candidates[0].transform;
        for (int i = 1; i < candidates.Length; ++i)
        {
            float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                closest = candidates[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }
}
