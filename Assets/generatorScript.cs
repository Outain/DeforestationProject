using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generatorScript : MonoBehaviour {
    public Slider slidey;
    //public GameObject sliderToggle;
    public float health =100f;
    public bool brokenDown;
    public float breakdownRate,repairRate;
    public GameObject nearestHuman;
    public enemyBehaviour eb;
    //public GameObject sliderPrefab;

	// Use this for initialization
	void Start () {
        //slidey = GetComponent<Slider>();
        slidey.value = 100;
	}
	
	// Update is called once per frame
	void Update () {
        slidey.value = health;
        if(health <= 0 && brokenDown==false)
        {
            brokenDown = true;
            gameController.score += gameController.generatorBonus;
            health = 0;
            slidey.gameObject.SetActive(false);
            nearestHuman = FindHuman();
            print(nearestHuman);
            eb = nearestHuman.GetComponent<enemyBehaviour>();
            eb.behaviourState = 2;
            eb.generator = this.gameObject;
        }
	}

    public void Breakdown()
    {
        if (!brokenDown)
        {
            health -= breakdownRate * Time.deltaTime;
        }
    }

    public void Repair()
    {
        if (brokenDown)
        {
            health += repairRate * Time.deltaTime;
        }
        if(health >= 100)
        {
            brokenDown = false;
            slidey.gameObject.SetActive(true);
            health = 100;
        }
    }

    public GameObject FindHuman()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("human");
        float minDistance = Mathf.Infinity;
        GameObject closest;

        if (candidates.Length == 0)
            return null;

        closest = candidates[0];
        for (int i = 1; i < candidates.Length; ++i)
        {
            float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                closest = candidates[i];
                minDistance = distance;
            }
        }
        return closest;
    }
}
