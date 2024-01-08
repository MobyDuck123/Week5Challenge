using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        // Assuming the player's goal is a separate GameObject in the scene
        playerGoal = GameObject.Find("Player Goal");

        if (playerGoal == null)
        {
            Debug.LogError("Player Goal not found. Make sure the GameObject is named 'Player Goal'.");
        }
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        // Check if playerGoal is not null before using it
        if (playerGoal != null)
        {
            // Set enemy direction towards player goal and move there
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal" || (playerGoal != null && other.gameObject == playerGoal))
        {
            Destroy(gameObject);
        }
    }
}