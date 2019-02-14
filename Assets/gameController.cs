using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    public int turnNumber;
    public static bool playerTurn;
    public bool rabbitSelected, bearSelected;
    public static bool unitSelected;
    public static int score;
    public static int forestPower;
    public static bool hunterActivated;
    private float scoreIncrementer;
    public bool deactivation;
    public float timeElapsed;
    public Slider slidey,resourceSlider;
    public static float resources;
    public float resourceGainRate;
    public float costPerRabbit, costPerBear;

    public static int generatorBonus=200;

	// Use this for initialization
	void Start () {
        resources = 10;
        hunterActivated = false;
        forestPower = 100;
        unitSelected = false;
        score = 0;
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        scoreIncrementer += Time.deltaTime;
        resources += resourceGainRate;
        slidey.value = forestPower;
        resourceSlider.value = resources;
        if (scoreIncrementer >= 10)
        {
            
            score += 100;
            scoreIncrementer = 0;
        }
	}

     private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Time: " + Mathf.RoundToInt(timeElapsed));
        GUI.Label(new Rect(10, 40, 100, 20), "Score: " + score);

        if (rabbitSelected)
        {
            GUI.color = Color.yellow;
        }
        if (GUI.Button(new Rect(10, 60, 100, 20), "new rabbit"))
        {
            rabbitSelected = !rabbitSelected;
            unitSelected = rabbitSelected;
            print("You clicked the button!");
        }
        GUI.color = Color.white;
        if (bearSelected)
        {
            GUI.color = Color.yellow;
        }
        if (GUI.Button(new Rect(10, 80, 100, 20), "new bear"))
        {
            bearSelected = !bearSelected;
            unitSelected = bearSelected;
            print("You clicked the button!");
        }
    }
}
