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

    void Start()
    {
        //spawn rocks randomly maybe.
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
        if (redScore >= maxPoints)
            winText.text = "Red has won!";
        else if (blueScore >= maxPoints)
            winText.text = "Blue has won!";
        else if (yellowScore >= maxPoints)
            winText.text = "Yellow has won!";
        else if (greenScore >= maxPoints)
            winText.text = "Green has won!";
    }

    public void RespawnRock(GameObject rock)
    {
        Vector3 position = new Vector3(Random.Range(-13.0f, 13.0f), 2, Random.Range(-13.0f, 13.0f));
        Instantiate(rock, position, Quaternion.identity);
    }
}
