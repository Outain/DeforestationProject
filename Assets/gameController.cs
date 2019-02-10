using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    public int turnNumber;
    public static bool playerTurn;
    public bool rabbitSelected;
    public bool unitSelected;
    public static int score;
    private float scoreIncrementer;
    public bool deactivation;
    public float timeElapsed;

    public static int generatorBonus=200;

	// Use this for initialization
	void Start () {
        unitSelected = false;
        score = 0;
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        scoreIncrementer += Time.deltaTime;
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
    }
}
