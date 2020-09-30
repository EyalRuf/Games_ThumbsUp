using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public TextMesh redScoreUI;
    private float redScore;

    public TextMesh blueScoreUI;
    private float blueScore;

    public TextMesh yellowScoreUI;
    private float yellowScore;

    public TextMesh greenScoreUI;
    private float greenScore;

    public Text winText;
    public float points;
    public float maxPoints;

    //Timer
    public Text timer;
    public float startTime;
    private float currentTime;

    void Start()
    {
        currentTime = startTime;
        //spawn rocks randomly maybe.
    }

    private void Update()
    {
        //Timer counter
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            float minute = currentTime / 60;
            float second = (minute % 1) * 60;
            timer.text = Mathf.Floor(minute) + " : " + Mathf.Floor(second);
        }
        else
            MostPointsCheck();
    }

    public void AddScore(string deliver)
    {
        if (deliver == "BlueDeliver")
            blueScore += points;
        if (deliver == "RedDeliver")
            redScore += points;
        if (deliver == "GreenDeliver")
            greenScore += points;
        if (deliver == "YellowDeliver")
            yellowScore += points;

        UpdateScore();
        CheckWin();
    }

    void UpdateScore()
    {
        redScoreUI.text = redScore + "%";
        blueScoreUI.text = blueScore + "%";
        yellowScoreUI.text = yellowScore + "%";
        greenScoreUI.text = greenScore + "%";
    }

    void CheckWin()
    {
        if (redScore >= 5) {
            winText.text = "Red has won!";
            GameEnded();
        }
            
        else if (blueScore >= maxPoints)
        {
            winText.text = "Blue has won!";
            GameEnded();
        }
            
        else if (yellowScore >= maxPoints)
        {
            winText.text = "Yellow has won!";
            GameEnded();
        }
            
        else if (greenScore >= maxPoints)
        {
            winText.text = "Green has won!";
            GameEnded();
        }
            
    }

    void MostPointsCheck()
    {
        float highScore = Mathf.Max(redScore, blueScore, yellowScore, greenScore);
        timer.text = 0 + " : " + 0;

        if (highScore == redScore)
            winText.text = "Red has won!";
        else if (highScore == blueScore)
            winText.text = "Blue has won!";
        else if (highScore == yellowScore)
            winText.text = "Yellow has won!";
        else if (highScore == greenScore)
            winText.text = "Green has won!";

        GameEnded();
    }

    void GameEnded()
    {
        Time.timeScale = 0;
    }
}
