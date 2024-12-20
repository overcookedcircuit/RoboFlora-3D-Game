using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject txtToDisplay; // Display the UI text
    private bool PlayerInZone; // Check if the player is in trigger

    public GameObject rock;
    private float targetY = 1.9f; // Initial coord is 43.2 and I want to move it to 1.9
    private float duration = 10f; // Time in seconds for the movement

    private bool isMoving = false; // Flag to control movement
    private Vector3 startPosition; // Initial position of the rock
    private Vector3 endPosition; // Final position of the rock
    private float elapsedTime = 0f; // Track time for interpolation

    private void Start()
    {
        PlayerInZone = false; 
        txtToDisplay.SetActive(false);

        if (rock == null)
        {
            Debug.LogError("Rock GameObject is not assigned!");
        }
        else
        {
            Debug.Log("Rock assigned: " + rock.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Button pressed.");
            Debug.Log(PlayerInZone);
            Debug.Log(isMoving);
        }
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F) && !isMoving) // If in zone, press F, and not already moving
        {
            Debug.Log("Button pressed. Starting rock movement.");
            gameObject.GetComponent<Animator>().Play("switch");

            // Initialize movement
            isMoving = true;
            elapsedTime = 0f;
            startPosition = rock.transform.position;
            endPosition = new Vector3(startPosition.x, targetY, startPosition.z);
        }

        if (isMoving)
        {
            MoveRockDown();
        }
    }

    private void MoveRockDown()
    {
        if (rock == null)
        {
            Debug.LogError("No rock assigned for movement!");
            isMoving = false; // Stop moving if no rock is assigned
            return;
        }

        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the new position using Lerp
        rock.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);

        // Check if the movement is complete
        if (elapsedTime >= duration)
        {
            rock.transform.position = endPosition; // Ensure final position is correct
            isMoving = false; // Stop moving
            Debug.Log("Rock movement complete.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered the trigger zone.");
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         PlayerInZone = false;
    //         txtToDisplay.SetActive(false);
    //         Debug.Log("Player exited the trigger zone.");
    //     }
    // }
}
