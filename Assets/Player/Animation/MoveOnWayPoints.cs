using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayPoints : MonoBehaviour
{
    public List<GameObject> waypoints;
    public float speed = 2;
    int index = 0;
    private bool shouldMove = true; // Flag to control movement

    void Update()
    {
        if (shouldMove && index < waypoints.Count)
        {
            Vector3 destination = waypoints[index].transform.position;
            Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            transform.position = newPos;

            // Debug current position and waypoint position
            Debug.Log("Current Position: " + transform.position);
            Debug.Log("Waypoint Position: " + destination);

            float distance = Vector3.Distance(transform.position, destination);
            if (distance < 0.5f) // Adjusted threshold for waypoint switching
            {
                // Move to the next waypoint
                index++;
            }
        }
    }

    // Stop movement on collision with the player's capsule collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            shouldMove = false;
        }
    }
}
