using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInteraction : MonoBehaviour {
    public Camera cam;
    public bool clickedOnce;
    public static Vector3 target;
    public GameObject currentSelected, previousSelected;
    public squirrelScript sq;
    

    // Use this for initialization
    void Start() {
        
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) { 
            
                
               
            

        Ray ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));
        RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
                if (hit.transform.tag == "Interactable")
                {
                    print(hit.transform.gameObject);
                    if(currentSelected!=null)
                    previousSelected = currentSelected;

                    currentSelected = hit.transform.gameObject;
                    print(currentSelected);

                    if (previousSelected != null) {
                        sq = previousSelected.GetComponent<squirrelScript>();
                        sq.activated = false;
                    }

                    
                    hit.transform.gameObject.SendMessage("clicked");
                }
                else if(hit.transform.tag == "tree")
                {
                    
                    target = hit.transform.position;
                    Debug.Log(target);
                    
                }

            else if(hit.transform.tag == "generator")
                {
                    target = new Vector3((hit.transform.position.x+Random.Range(-0.5f,0.5f)),1,(hit.transform.position.z-1));
                    Debug.Log(target);
                }
    }
}

}

