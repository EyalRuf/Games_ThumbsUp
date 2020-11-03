using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game settings")]
    public int firstTo;
    public float timeBetweenRounds = 2;
    private bool roundOver = false;
    private float roundOverTimer = 0;
    private bool gameOver = false;

    [Header("Scores")]
    public int player0Score;
    public int player1Score;
    public int player2Score;
    public int player3Score;
    private int round;

    [Header("References")]
    public Text winText;
    public Text player0ScoreText;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text player3ScoreText;
    public Text currentRoundDisplay;
    public AudioSource roundOverAuSource;

    private string[] playerNames = { "Red", "Blue", "Yellow", "Green" };
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        roundOverAuSource = GetComponent<AudioSource>();
        round = 1;
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (!roundOver)
            {
                int survivingPlayerIndex;
                if (playerManager.OnePlayerAlive(out survivingPlayerIndex))
                {
                    //add score
                    switch (survivingPlayerIndex)
                    {
                        case 0:
                            player0Score++;
                            break;

                        case 1:
                            player1Score++;
                            break;

                        case 2:
                            player2Score++;
                            break;

                        case 3:
                            player3Score++;
                            break;
                    }

                    roundOver = true;
                    roundOverTimer = timeBetweenRounds;

                    player0ScoreText.text = player0Score.ToString();
                    player1ScoreText.text = player1Score.ToString();
                    player2ScoreText.text = player2Score.ToString();
                    player3ScoreText.text = player3Score.ToString();
                }
            }
            else
            {
                roundOverTimer -= Time.deltaTime;
                if(roundOverTimer < 0)
                {
                    if (!roundOverAuSource.isPlaying)
                    {
                        roundOverAuSource.Play();
                    }

                    int playerWon;
                    if (PlayerWon(out playerWon))
                    {
                        winText.text = playerNames[playerWon] + " has won!";
                        Time.timeScale = 0;
                        gameOver = true;
                    }
                    else
                    {
                        playerManager.RespawnAll();
                        roundOver = false;
                        UpdateRoundUI();
                    }
                }
            }
        }
    }

    private bool PlayerWon(out int playerIndex)
    {
        int playerWon = 0;
        bool gameOver = false;

        if (player0Score >= firstTo)
        {
            gameOver = true;
            playerWon = 0;
        }
        else if (player1Score >= firstTo)
        {
            gameOver = true;
            playerWon = 1;
        }
        else if (player2Score >= firstTo)
        {
            gameOver = true;
            playerWon = 2;
        }
        else if (player3Score >= firstTo)
        {
            gameOver = true;
            playerWon = 3;
        }

        playerIndex = playerWon;

        return gameOver;
    }

    void UpdateRoundUI()
    {
        round++;
        currentRoundDisplay.text = "Round " + round;
    }
}
