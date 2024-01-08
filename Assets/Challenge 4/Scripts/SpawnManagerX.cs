using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    private bool hasPowerup = false;
   

    private int enemiesPerWave = 1; // Start with 1 enemy per wave
    private int maxEnemiesPerWave = 3; // Set the maximum enemies per wave

    public GameObject player;
    public int waveCount = 1;

    // Update is called once per frame
    void Update()
    {
        if (!hasPowerup && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            SpawnPowerup();
            SpawnEnemyWave(enemiesPerWave);

            // Increase enemiesPerWave up to the maximum
            enemiesPerWave = Mathf.Min(enemiesPerWave + 1, maxEnemiesPerWave);
        }

      
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnPowerup()
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end
        Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}