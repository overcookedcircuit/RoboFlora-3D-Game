using UnityEngine;
using Cinemachine;
using System;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player;                        // Reference to the Player GameObject
    public GameObject bird;                          // Reference to the Bird GameObject
    public GameObject rhino;                         // Reference to the Rhino GameObject

    public CinemachineVirtualCamera playerCamera;    // Reference to the Player Virtual Camera
    public CinemachineVirtualCamera birdCamera;      // Reference to the Bird Virtual Camera
    public CinemachineVirtualCamera rhinoCamera;     // Reference to the Rhino Virtual Camera

    private string currentCharacter = "player";      // Start with the player as the active character

    void Start()
    {
        // Initialize by setting the player active and others inactive
        ActivatePlayer();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            switch (Input.inputString) // Get the pressed key as a string
            {
                case "1":
                    SwitchCharacter("player");
                    break;

                case "2":
                    SwitchCharacter("bird");
                    break;

                case "3":
                    SwitchCharacter("rhino");
                    break;
            }
        }
    }

    void SwitchCharacter(string newCharacter)
    {
        // Get the current active character's transform
        Transform currentTransform = null;

        if (currentCharacter == "player") currentTransform = player.transform;
        else if (currentCharacter == "bird") currentTransform = bird.transform;
        else if (currentCharacter == "rhino") currentTransform = rhino.transform;

        // Determine which character to activate
        if (newCharacter == "player")
        {
            SetPositionAndActivate(player, currentTransform);
            playerCamera.Priority = 10;
            birdCamera.Priority = 0;
            rhinoCamera.Priority = 0;
        }
        else if (newCharacter == "bird")
        {
            SetPositionAndActivate(bird, currentTransform);
            birdCamera.Priority = 10;
            playerCamera.Priority = 0;
            rhinoCamera.Priority = 0;
        }
        else if (newCharacter == "rhino")
        {
            SetPositionAndActivate(rhino, currentTransform);
            rhinoCamera.Priority = 10;
            playerCamera.Priority = 0;
            birdCamera.Priority = 0;
        }

        // Update the current character
        currentCharacter = newCharacter;
    }

    void SetPositionAndActivate(GameObject newCharacter, Transform previousTransform)
    {
        // Transfer only the position
        newCharacter.transform.position = previousTransform.position;

        // Activate the new character and deactivate others
        player.SetActive(newCharacter == player);
        bird.SetActive(newCharacter == bird);
        rhino.SetActive(newCharacter == rhino);
    }

    void ActivatePlayer()
    {
        // Initialize the player as the active character
        player.SetActive(true);
        bird.SetActive(false);
        rhino.SetActive(false);

        playerCamera.Priority = 10;  // Set high priority for Player Camera
        birdCamera.Priority = 0;     // Set low priority for Bird Camera
        rhinoCamera.Priority = 0;    // Set low priority for Rhino Camera

        currentCharacter = "player";
    }
}
