using UnityEngine;
using Cinemachine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player;                        // Reference to the Player GameObject
    public GameObject bird;                          // Reference to the Bird GameObject
    public CinemachineVirtualCamera playerCamera;    // Reference to the Player Virtual Camera
    public CinemachineVirtualCamera birdCamera;      // Reference to the Bird Virtual Camera

    private bool isPlayerActive = true;              // Start with the player as the active character

    void Start()
    {
        // Initialize by setting the player active and bird inactive
        ActivatePlayer();
    }

    void Update()
    {
        // Switch characters when the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPlayerActive)
            {
                ActivateBird();
            }
            else
            {
                ActivatePlayer();
            }
        }
    }

    void ActivatePlayer()
    {
        // Enable Player and Player Camera, disable Bird and Bird Camera
        player.SetActive(true);
        bird.SetActive(false);

        playerCamera.Priority = 10;  // Set high priority for Player Camera
        birdCamera.Priority = 0;     // Set low priority for Bird Camera

        isPlayerActive = true;
    }

    void ActivateBird()
    {
        // Enable Bird and Bird Camera, disable Player and Player Camera
        bird.SetActive(true);
        player.SetActive(false);

        birdCamera.Priority = 10;    // Set high priority for Bird Camera
        playerCamera.Priority = 0;   // Set low priority for Player Camera

        isPlayerActive = false;
    }
}
