using UnityEngine;
using Cinemachine;
using System;
using System.Diagnostics;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player;                        // Reference to the Player GameObject
    public GameObject bird;                          // Reference to the Bird GameObject
    public GameObject rhino;                        // Reference to the Rhino GameObject

    public CinemachineVirtualCamera playerCamera;    // Reference to the Player Virtual Camera
    public CinemachineVirtualCamera birdCamera;      // Reference to the Bird Virtual Camera
    public CinemachineVirtualCamera rhinoCamera;      // Reference to the Bird Virtual Camera

    private String currentCharacter = "player";              // Start with the player as the active character

    void Start()
    {
        // Initialize by setting the player active and bird inactive
        ActivatePlayer();
    }

    void Update()
    {
         if (Input.anyKeyDown)
        {
            switch (Input.inputString) // Get the pressed key as a string
            {
                case "1":
                    ActivatePlayer();
                    break;

                case "2":
                    ActivateBird();
                    break;

                case "3":
                    ActivateRhino();
                    break;
            }
        }
    }

    void ActivatePlayer()
    {
        // Enable Player and Player Camera, disable Bird and Bird Camera
        player.SetActive(true);
        bird.SetActive(false);
        rhino.SetActive(false);

        playerCamera.Priority = 10;  // Set high priority for Player Camera
        birdCamera.Priority = 0;     // Set low priority for Bird Camera
        rhinoCamera.Priority = 0;   // Set low priority for Player Camera

        currentCharacter = "player";
    }

    void ActivateBird()
    {
        // Enable Bird and Bird Camera, disable Player and Player Camera
        bird.SetActive(true);
        player.SetActive(false);
        rhino.SetActive(false);

        birdCamera.Priority = 10;    // Set high priority for Bird Camera
        playerCamera.Priority = 0;   // Set low priority for Player Camera
        rhinoCamera.Priority = 0;   // Set low priority for Player Camera

        currentCharacter = "bird";
    }

    void ActivateRhino()
    {
        // Enable Player and Player Camera, disable Bird and Bird Camera
        rhino.SetActive(true);
        player.SetActive(false);
        bird.SetActive(false);

        rhinoCamera.Priority = 10;   // Set low priority for Player Camera
        playerCamera.Priority = 0;  // Set high priority for Player Camera
        birdCamera.Priority = 0;     // Set low priority for Bird Camera

        currentCharacter = "rhino";
    }
}
