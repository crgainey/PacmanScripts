using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text scoreText;
    public Text gameOver;
    //public image something for lives
    public int score;
    public int lives;

    public GameObject restartButton, quitButton;

    //private PlayerMovement playerMovement;


    void Start()
    {
        score = 0;
        lives = 3;
        gameOver.text = "";
        SetGameover();

        restartButton.SetActive(false);
        quitButton.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void PowerUP()
    {

    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "High Score" + score;
        SetGameover();

        //in player script add ints for each object and call back to this script
    }

    public void UpdateLives()
    {

    }

    public void SetGameover()
    {
        //if() level complete amoutn of slices collected

        if(lives == 0)
        {
            gameOver.text = "GAME OVER";
            restartButton.SetActive(true);
            quitButton.SetActive(true);
        }
    }
}
