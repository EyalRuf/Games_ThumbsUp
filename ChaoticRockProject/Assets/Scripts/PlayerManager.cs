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
    public Transform player0Spawn;
    public Transform player1Spawn;
    public Transform player2Spawn;
    public Transform player3Spawn;

    //player references
    private GameObject player0;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;

    private void Start()
    {
        //spawn players on start
        player0 = Instantiate(player0Prefab, player0Spawn.position, player0Spawn.rotation);
        player1 = Instantiate(player1Prefab, player1Spawn.position, player1Spawn.rotation);
        player2 = Instantiate(player2Prefab, player2Spawn.position, player2Spawn.rotation);
        player3 = Instantiate(player3Prefab, player3Spawn.position, player3Spawn.rotation);
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

        //respawn all players at start positions
        player0 = Instantiate(player0Prefab, player0Spawn.position, player0Spawn.rotation);
        player1 = Instantiate(player1Prefab, player1Spawn.position, player1Spawn.rotation);
        player2 = Instantiate(player2Prefab, player2Spawn.position, player2Spawn.rotation);
        player3 = Instantiate(player3Prefab, player3Spawn.position, player3Spawn.rotation);
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
