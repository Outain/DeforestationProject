using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectInteraction : MonoBehaviour {
    public Camera cam;
    public bool clickedOnce;
    public static Vector3 target;
    public GameObject currentSelected, previousSelected;
    public playerController sq;
    public gameController gameController;
    public GameObject rabbitPrefab,bearPrefab;
    public GameObject pointer;
    

    // Use this for initialization
    void Start() {
        pointer.gameObject.SetActive(false);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (gameController.unitSelected)
        {
            pointer.gameObject.SetActive(true);
            pointer.transform.position = Input.mousePosition;
        }
        else
        {
            pointer.gameObject.SetActive(false);
        }


        if (Input.GetMouseButtonDown(0)) {




            if (!gameController.unitSelected){
                Ray ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                    if (hit.transform.tag == "Interactable")
                    {
                        print(hit.transform.gameObject);
                        if (currentSelected != null)
                            previousSelected = currentSelected;

                        currentSelected = hit.transform.gameObject;
                        print(currentSelected);

                        if (previousSelected != null) {
                            sq = previousSelected.GetComponent<playerController>();
                            sq.activated = false;
                        }


                        hit.transform.gameObject.SendMessage("clicked");
                    }
                    else if (hit.transform.tag == "tree")
                    {

                        target = hit.transform.position;
                        Debug.Log(target);

                    }

                    else if (hit.transform.tag == "generator")
                    {
                        target = new Vector3((hit.transform.position.x + Random.Range(-0.5f, 0.5f)), 1, (hit.transform.position.z - 1));
                        Debug.Log(target);
                    }
            }

            else if (gameController.rabbitSelected)
            {
                Ray ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                    if (hit.transform.tag == "ground")
                    {
                        if (gameController.costPerRabbit <= gameController.resources)
                        {
                            //Vector3 spawnPoint = new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z);
                            Vector3 spawnPoint = hit.point;
                            Instantiate(rabbitPrefab, spawnPoint, Quaternion.identity);
                            gameController.rabbitSelected = false;
                            gameController.unitSelected = false;
                            gameController.resources -= gameController.costPerRabbit;
                        }
                    }
            }

            else if (gameController.bearSelected)
            {
                Ray ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                    if (hit.transform.tag == "ground")
                    {
                        if (gameController.costPerBear <= gameController.resources)
                        {
                            //Vector3 spawnPoint = new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z);
                            Vector3 spawnPoint = hit.point;
                            GameObject bearBoy = Instantiate(bearPrefab, spawnPoint, Quaternion.identity);

                            gameController.bearSelected = false;
                            gameController.unitSelected = false;
                            hunterScript.bear = bearBoy;
                            gameController.resources -= gameController.costPerBear;
                        }
                    }
            }
    }
}

}

