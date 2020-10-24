using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //POSSIBLE BETTER SETUP:
    //Create custom class that holds all variables within it which we reference from an array so we have control over the amount of players in game 

    [Header("Player Prefabs")]
    public GameObject player0Prefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;

    [Header("SpawnPoints")]
    public Transform[] playerSpawnPoints;

    //player references
    private GameObject player0;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;

    private void Start()
    {
        //spawn players on start
        SpawnInRandomPositions();
    }

    void SpawnInRandomPositions ()
    {
        int randomSpawnPoint = Random.Range(0, playerSpawnPoints.Length);
        int p0SpawnPoint = randomSpawnPoint;
        int p1SpawnPoint = randomSpawnPoint;
        int p2SpawnPoint = randomSpawnPoint;
        int p3SpawnPoint = randomSpawnPoint;

        while (p1SpawnPoint == p0SpawnPoint)
        {
            p1SpawnPoint = Random.Range(0, playerSpawnPoints.Length);
        }
        while (p2SpawnPoint == p0SpawnPoint || p2SpawnPoint == p1SpawnPoint)
        {
            p2SpawnPoint = Random.Range(0, playerSpawnPoints.Length);
        }
        while (p3SpawnPoint == p0SpawnPoint || p3SpawnPoint == p1SpawnPoint || p3SpawnPoint == p2SpawnPoint)
        {
            p3SpawnPoint = Random.Range(0, playerSpawnPoints.Length);
        }

        player0 = Instantiate(player0Prefab, playerSpawnPoints[p0SpawnPoint].position, playerSpawnPoints[p0SpawnPoint].rotation);
        player1 = Instantiate(player1Prefab, playerSpawnPoints[p1SpawnPoint].position, playerSpawnPoints[p1SpawnPoint].rotation);
        player2 = Instantiate(player2Prefab, playerSpawnPoints[p2SpawnPoint].position, playerSpawnPoints[p2SpawnPoint].rotation);
        player3 = Instantiate(player3Prefab, playerSpawnPoints[p3SpawnPoint].position, playerSpawnPoints[p3SpawnPoint].rotation);
    }

    public void RespawnAll()
    {
        //despawn any left over players
        if (player0 != null)
        {
            Destroy(player0);
        }
        if (player1 != null)
        {
            Destroy(player1);
        }
        if (player2 != null)
        {
            Destroy(player2);
        }
        if (player3 != null)
        {
            Destroy(player3);
        }

        //respawn all players
        SpawnInRandomPositions();
    }

    public bool OnePlayerAlive(out int lastPlayerIndex)
    {
        int playersAlive = (player0 == null ? 0 : 1) + (player1 == null ? 0 : 1) + (player2 == null ? 0 : 1) + (player3 == null ? 0 : 1);

        if (player0 != null)
        {
            lastPlayerIndex = 0;
        }
        else if (player1 != null)
        {
            lastPlayerIndex = 1;
        }
        else if (player2 != null)
        {
            lastPlayerIndex = 2;
        }
        else
        {
            lastPlayerIndex = 3;
        }

        return playersAlive <= 1;
    }
}
