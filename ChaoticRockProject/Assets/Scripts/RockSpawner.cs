using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Settings")]
    public int currentRocksInScene;
    public int maxRocksInScene;
    public float rockSpawningCooldown;
    public float rockSpawnRotation;

    [Header("References")]
    public GameObject[] rockPrefabs;
    public Transform[] rockSpawnPositions;

    private float rockSpawningTimer;
    private void Update()
    {
        int rocksInScene = GameObject.FindGameObjectsWithTag("Rock").Length;

        if(rocksInScene < currentRocksInScene)
        {
            rockSpawningTimer = rockSpawningCooldown;
        }

        currentRocksInScene = rocksInScene;



        rockSpawningTimer -= Time.deltaTime;

        if(rockSpawningTimer < 0 && currentRocksInScene < maxRocksInScene)
        {
            SpawnRock();
            rockSpawningTimer = rockSpawningCooldown;
        }
    }

    public void SpawnRock()
    {
        Vector3 spawnPos = rockSpawnPositions[Mathf.RoundToInt(Random.Range(0, rockSpawnPositions.Length))].position;

        //spawn a random rock at a random position
        Rigidbody rockRb = Instantiate(rockPrefabs[Mathf.RoundToInt(Random.Range(0, rockPrefabs.Length))], spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
        rockRb.AddTorque(new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1).normalized * rockSpawnRotation, ForceMode.Acceleration);
    }
}
