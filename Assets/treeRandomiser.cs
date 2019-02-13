using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeRandomiser : MonoBehaviour
    
{
    public float randomHeightModifier;
    public float randomRotation;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(-90, transform.rotation.y, transform.rotation.z + (Random.Range(0, 360)));
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + (Random.Range(-randomHeightModifier, randomHeightModifier)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
