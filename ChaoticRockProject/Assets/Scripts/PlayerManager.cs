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

    [Header("Cooldowns")]
    public float respawnCooldown;

    private float player0Cooldown;
    private bool player0respawning;

    private float player1Cooldown;
    private bool player1respawning;

    private float player2Cooldown;
    private bool player2respawning;

    private float player3Cooldown;
    private bool player3respawning;

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

    private void Update()
    {
        //PLAYER 0
        //if player has died/been destroyed
        if (player0 == null)
        {
            if (player0respawning)
            {
                //if player is respawning
                if(player0Cooldown < 0)
                {
                    //if cooldown has worn off spawn player back in
                    player0 = Instantiate(player0Prefab, player0Spawn.position, player0Spawn.rotation);
                    player0respawning = false;
                }
                else
                {
                    //otherwise wear off cooldown
                    player0Cooldown -= Time.deltaTime;
                }
            }
            else
            {
                //otherwise player is respawning and cooldown is set
                player0respawning = true;
                player0Cooldown = respawnCooldown;
            }
        }

        //PLAYER 1
        //if player has died/been destroyed
        if (player1 == null)
        {
            if (player1respawning)
            {
                //if player is respawning
                if (player1Cooldown < 0)
                {
                    //if cooldown has worn off spawn player back in
                    player1 = Instantiate(player1Prefab, player1Spawn.position, player1Spawn.rotation);
                    player1respawning = false;
                }
                else
                {
                    //otherwise wear off cooldown
                    player1Cooldown -= Time.deltaTime;
                }
            }
            else
            {
                //otherwise player is respawning and cooldown is set
                player1respawning = true;
                player1Cooldown = respawnCooldown;
            }
        }

        //PLAYER 2
        //if player has died/been destroyed
        if (player2 == null)
        {
            if (player2respawning)
            {
                //if player is respawning
                if (player2Cooldown < 0)
                {
                    //if cooldown has worn off spawn player back in
                    player2 = Instantiate(player2Prefab, player2Spawn.position, player2Spawn.rotation);
                    player2respawning = false;
                }
                else
                {
                    //otherwise wear off cooldown
                    player2Cooldown -= Time.deltaTime;
                }
            }
            else
            {
                //otherwise player is respawning and cooldown is set
                player2respawning = true;
                player2Cooldown = respawnCooldown;
            }
        }

        //PLAYER 3
        //if player has died/been destroyed
        if (player3 == null)
        {
            if (player3respawning)
            {
                //if player is respawning
                if (player3Cooldown < 0)
                {
                    //if cooldown has worn off spawn player back in
                    player3 = Instantiate(player3Prefab, player3Spawn.position, player3Spawn.rotation);
                    player3respawning = false;
                }
                else
                {
                    //otherwise wear off cooldown
                    player3Cooldown -= Time.deltaTime;
                }
            }
            else
            {
                //otherwise player is respawning and cooldown is set
                player3respawning = true;
                player3Cooldown = respawnCooldown;
            }
        }
    }
}
