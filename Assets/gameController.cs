using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public bool gameOver = false;
    public GameObject gameOverText;
    public Text gameOverScore;
    public Text resourceText,timeText,scoreText;
    public Sprite rabbitNormal, rabbitHighlight, bearNormal, bearHighlight;
    public Image rabbitImage, bearImage;
    public static bool bearInPlay;

    public static int generatorBonus=1000;

	// Use this for initialization
	void Start () {
        bearInPlay = false;
        resources = 0;
        hunterActivated = false;
        forestPower = 100;
        unitSelected = false;
        score = 0;
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            timeElapsed += Time.deltaTime;
            scoreIncrementer += Time.deltaTime;
        }
        resources += resourceGainRate;
        slidey.value = forestPower;
        resourceSlider.value = resources;
        if (resources >= 100)
        {
            resources = 100;
        }
        if (scoreIncrementer >= 10)
        {
            
            score += 100;
            scoreIncrementer = 0;
        }

        if(forestPower <= 0)
        {
            gameOver = true;
            gameOverText.SetActive(true);
            gameOverScore.text = ("Score: " + score);
            slidey.gameObject.SetActive(false);
            resourceSlider.gameObject.SetActive(false);
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }

           
        }
        resourceText.text = "Resources:" + " " + Mathf.RoundToInt(resources) + "%";
        timeText.text = "Time:" + " " + Mathf.RoundToInt(timeElapsed);
        scoreText.text = "Score:" + " " + Mathf.RoundToInt(score);
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

     private void OnGUI()
    {
        if (!gameOver)
        {
        //    GUI.Label(new Rect(10, 10, 100, 20), "Time: " + Mathf.RoundToInt(timeElapsed));
        //    GUI.Label(new Rect(10, 40, 100, 20), "Score: " + score);

            //if (rabbitSelected)
            //{
            //    GUI.color = Color.yellow;
            //}
            //if (GUI.Button(new Rect(10, 60, 100, 20), "new rabbit"))
            //{
            //    rabbitSelected = !rabbitSelected;
            //    unitSelected = rabbitSelected;
            //    print("You clicked the button!");
            //}
            //GUI.color = Color.white;
            //if (bearSelected)
            //{
            //    GUI.color = Color.yellow;
            //}
            //if (GUI.Button(new Rect(10, 80, 100, 20), "new bear"))
            //{
            //    bearSelected = !bearSelected;
            //    unitSelected = bearSelected;
            //    print("You clicked the button!");
            //}
        }
    }

    public void SelectRabbit()
    {
        rabbitSelected = !rabbitSelected;
        unitSelected = rabbitSelected;
        //print("You clicked the button!");
        if (rabbitSelected)
        {
            rabbitImage.sprite = rabbitHighlight;

        }
        else
        {
            rabbitImage.sprite = rabbitNormal;
        }
    }

    public void SelectBear()
    {
        if (!bearInPlay)
        {
            bearSelected = !bearSelected;
            unitSelected = bearSelected;

            if (bearSelected)
            {
                bearImage.sprite = bearHighlight;
            }
            else
            {
                bearImage.sprite = bearNormal;
            }
        }
    }
}
