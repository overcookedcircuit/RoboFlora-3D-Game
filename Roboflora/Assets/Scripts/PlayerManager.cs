using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerManager Instance { get; private set; }

    // Health and Stamina UI elements
    public Slider healthSlider;
    public Slider staminaSlider;
    public Gradient healthGradient;
    public Image healthFill;
    public Image staminaFill;

    public GameObject pauseMenu; // Pause menu object
    private bool gamePaused;

    // Player stats
    public float maxHealth = 100;
    public float maxStamina = 100;
    public float health = 100;
    public float stamina = 100;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple PlayerManager instances found! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Dynamically assign the pause menu if not set in the Inspector
        Time.timeScale = 1;
        gamePaused = false;
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag("PauseMenu");
            if (pauseMenu == null)
            {
                Debug.LogError("Pause Menu not found! Please assign it in the Inspector or ensure it has the 'PauseMenu' tag.");
            }
        }

        SetMaxHealth(maxHealth);
        SetStamina(maxStamina);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (gamePaused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }


    public void SetMaxHealth(float health)
    {
        this.health = health;
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
            healthFill.color = healthGradient.Evaluate(1f);
        }
    }

    public void SetHealth(float health)
    {
        Debug.Log("PLAYERMANAGER IS BEING USED");
        this.health = health;
        if (healthSlider != null)
        {
            healthSlider.value = health;
            healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
        }
    }

    public void SetMaxStamina(float stamina)
    {
        this.stamina = stamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = stamina;
            staminaSlider.value = stamina;
        }
    }

    public void SetStamina(float stamina)
    {
        this.stamina = stamina;
        if (staminaSlider != null)
        {
            staminaSlider.value = stamina;
        }
    }

    public void Play()
    {
        gamePaused = false;

        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag("PauseMenu");
            if (pauseMenu == null)
            {
                Debug.LogError("Pause Menu not found! Please assign it in the Inspector or ensure it has the 'PauseMenu' tag.");
            }
        }

        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenu.SetActive(false); // Disable the pause menu
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gamePaused = true;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
