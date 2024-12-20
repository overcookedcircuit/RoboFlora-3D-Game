using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using TMPro;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player;                        // Reference to the Player GameObject
    public GameObject bird;                          // Reference to the Bird GameObject
    public GameObject rhino;                         // Reference to the Rhino GameObject

    public CinemachineVirtualCamera playerCamera;    // Reference to the Player Virtual Camera
    public CinemachineVirtualCamera birdCamera;      // Reference to the Bird Virtual Camera
    public CinemachineVirtualCamera rhinoCamera;     // Reference to the Rhino Virtual Camera
    private string currentCharacter = "player";      // Start with the player as the active character

    public GameObject crosshair;
    [SerializeField] private Image batteryImage; // Drag your Image component here in the Inspector
    [SerializeField] private TMP_Text batteryCountText; // Reference to TMP_Text for countdown
    Sprite[] batterySprites = new Sprite[6];
    private int morphBattery = 5;
    private bool isRecharging = false; // Prevent multiple recharge countdowns
    
    void Start()
    {
        // Initialize by setting the player active and others inactive
        ActivatePlayer();

        for (int i = 0; i < 6; i++) // Fixed range to include all battery sprites
        {
            batterySprites[i] = Resources.Load<Sprite>($"battery{i}");
        }

        UpdateBatteryUI(); // Update UI to reflect initial battery state
    }

    void Update()
    {
        if (morphBattery < 5 && !isRecharging)
        {
            StartCoroutine(RechargeBattery());
        }

        if (currentCharacter == "bird")
        {
            if (PlayerManager.Instance.stamina > 0)
            {
                PlayerManager.Instance.SetStamina(PlayerManager.Instance.stamina - 20f * Time.deltaTime);
            }
            else
            {
                SwitchCharacter("player", true);
            }
        }

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

    void SwitchCharacter(string newCharacter, bool skipBatteryCheck = false)
    {
        if (currentCharacter == newCharacter)
        {
            Debug.Log($"{newCharacter} is already active. No morphing required.");
            return; // Exit the method
        }

        if (!skipBatteryCheck && morphBattery <= 0)
        {
            Debug.Log("Not enough battery to morph!");
            return;
        }

        if (!skipBatteryCheck)
        {
            morphBattery -= 1;
            UpdateBatteryUI();
        }

        // Get the current active character's transform
        Transform currentTransform = null;

        if (currentCharacter == "player") currentTransform = player.transform;
        else if (currentCharacter == "bird") currentTransform = bird.transform;
        else if (currentCharacter == "rhino") currentTransform = rhino.transform;
        
        // Determine which character to activate
        if (newCharacter == "player")
        {
            SetPositionAndActivate(player, currentTransform);
            updateAIControllerTarget(player);
            playerCamera.Priority = 10;
            birdCamera.Priority = 0;
            rhinoCamera.Priority = 0;
            crosshair.SetActive(true);
        }
        else if (newCharacter == "bird")
        {
            SetPositionAndActivate(bird, currentTransform);
            updateAIControllerTarget(bird);
            birdCamera.Priority = 10;
            playerCamera.Priority = 0;
            rhinoCamera.Priority = 0;
            crosshair.SetActive(false);
        }
        else if (newCharacter == "rhino")
        {
            SetPositionAndActivate(rhino, currentTransform);
            updateAIControllerTarget(rhino);
            rhinoCamera.Priority = 10;
            playerCamera.Priority = 0;
            birdCamera.Priority = 0;
            crosshair.SetActive(false);
        }
        
        // Update the current character
        currentCharacter = newCharacter;
    }

    void updateAIControllerTarget(GameObject target){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            var aiController = enemy.GetComponent<AIController>();
            aiController.Player = target.transform;
            if(enemy.GetComponent<BigBossScript>() != null){
                enemy.GetComponent<BigBossScript>().playerTransform = target.transform;
            }
        }
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

    IEnumerator RechargeBattery()
    {
        isRecharging = true;

        int countdown = 10;
        while (countdown > 0)
        {
            batteryCountText.text = $"{countdown}";
            yield return new WaitForSeconds(1);
            countdown--;
        }

        // Add +1 to morphBattery
        morphBattery = Mathf.Min(morphBattery + 1, 5); // Ensure morphBattery doesn't exceed 5
        UpdateBatteryUI();

        // Clear the countdown text
        batteryCountText.text = "";

        isRecharging = false;
    }

    void UpdateBatteryUI()
    {
        // Update the battery sprite and UI text
        if (morphBattery >= 0 && morphBattery < batterySprites.Length)
        {
            batteryImage.sprite = batterySprites[morphBattery];
        }
    }
}
