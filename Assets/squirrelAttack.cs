using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squirrelAttack : MonoBehaviour {
    generatorScript dh;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "generator")
        {
            //Debug.Log("attacking");
            other.gameObject.SendMessage("Breakdown");
            if (other.gameObject.GetComponent<generatorScript>().brokenDown)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
