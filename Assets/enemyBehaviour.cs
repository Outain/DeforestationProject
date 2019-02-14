using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehaviour : MonoBehaviour {
    public int behaviourState;
    public float speed;
    public float rotationSpeed = 360f;
    public float loggingSpeed = 5;
    public Transform target, fleeTarget;
    public GameObject currentTree,generator;
    GameObject nearestTree;
    public generatorScript gs;
    public treeScript ts;
    public Rigidbody rb;
    public int pointsLostPerTree = 5;
    public float scaredyTime =5 ;
    private float scareTimeInitial;
    public NavMeshAgent agent;
    private int previousBehaviourInt;
    private Vector3 previousPos;
    public bool scared = false;

    //behaviour state numbers
    //0 is for searching initial tree and reset after tree is destroyed
    //1 is for engaging in chopping down
    //2 is for finding generator
    //3 is for repairing generator;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        scareTimeInitial = scaredyTime;
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
                //Vector3 direction = (target.position - transform.position).normalized;
                //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

                //// Vector3 targetDir = target.position - transform.position;

                //// The step size is equal to speed times frame time.
                //Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                ////Apply the rotation 
                //transform.rotation = rot;

                //// put 0 on the axys you do not want for the rotation object to rotate
                //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                agent.SetDestination(target.position);
            }
        }

        if(behaviourState == 1)
        {
            ts.health -= loggingSpeed * Time.deltaTime;
            if(ts.isAlive == false)
            {
                currentTree.tag = "Untagged";
                Destroy(currentTree);
                gameController.forestPower -= pointsLostPerTree;
                target = FindTarget();
                behaviourState = 0;
                navMeshBaker.timeToBake = true;
            }
        }

        if(behaviourState == 2)
        {
            if(generator != null)
            {
                //Vector3 direction = (generator.transform.position - transform.position).normalized;
                //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                agent.SetDestination(generator.transform.position);
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
            scaredyTime -= Time.deltaTime;
            if(scaredyTime <= 0)
            {
                behaviourState = previousBehaviourInt;
                agent.SetDestination(previousPos);
                scaredyTime = scareTimeInitial;
                scared = false;
                
            }
            //Vector3 direction = (fleeTarget.position - transform.position).normalized;
            //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            //// Vector3 targetDir = target.position - transform.position;

            //// The step size is equal to speed times frame time.
            //Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            ////Apply the rotation 
            //transform.rotation = rot;

            //// put 0 on the axys you do not want for the rotation object to rotate
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            agent.SetDestination(fleeTarget.position);
        }


    }

    public void Lockdown()
    {
        if (scared == false)
        {
            previousPos = agent.destination;
            previousBehaviourInt = behaviourState;
            if (previousBehaviourInt == 1 || previousBehaviourInt == 3)
            {
                previousBehaviourInt--; // this means if the lad is either in the middle of fixing a generator or chopping a treee theyll go back to seeking out what they had, so generators dont get left alone after a bear scare
            }
            behaviourState = 4;
            scared = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        if (collision.collider.CompareTag("treetochop")&&behaviourState ==0)
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
                nearestTree = candidates[i];
            }
        }
        if (nearestTree != null)
        {
            nearestTree.tag = "treetochop";
        }
        return closest;
    }
}
