using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour {
    public bool isAlive;
    public float health = 100;

	// Use this for initialization
	void Start () {
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            isAlive = false;
        }
	}
}
