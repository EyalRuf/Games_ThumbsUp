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
    public List<Transform> playerSpawnPoints;

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
        List<Transform> spawnPoints = new List<Transform>(playerSpawnPoints);

        // Finding random spawn point then removing it from the list and doing the same for next players
        // Will throw Errors if the there aren't 4+ spawn points attached to the object in the inspector
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Transform p0SpawnPoint = spawnPoints[randomSpawnPoint];
        spawnPoints.RemoveAt(randomSpawnPoint);

        randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Transform p1SpawnPoint = spawnPoints[randomSpawnPoint];
        spawnPoints.RemoveAt(randomSpawnPoint);

        randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Transform p2SpawnPoint = spawnPoints[randomSpawnPoint];
        spawnPoints.RemoveAt(randomSpawnPoint);

        randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Transform p3SpawnPoint = spawnPoints[randomSpawnPoint];

        player0 = Instantiate(player0Prefab, p0SpawnPoint.position, p0SpawnPoint.rotation);
        player1 = Instantiate(player1Prefab, p1SpawnPoint.position, p1SpawnPoint.rotation);
        player2 = Instantiate(player2Prefab, p2SpawnPoint.position, p2SpawnPoint.rotation);
        player3 = Instantiate(player3Prefab, p3SpawnPoint.position, p3SpawnPoint.rotation);
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
