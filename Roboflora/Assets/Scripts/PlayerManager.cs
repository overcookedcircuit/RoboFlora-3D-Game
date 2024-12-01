using System.Collections;
using UnityEngine;
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

    public void SetMaxHealth(float health)
    {
        this.health = health;
        healthSlider.maxValue = health;
        healthSlider.value = health;

        healthFill.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        this.health = health;
        healthSlider.value = health;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxStamina(float stamina)
    {
        this.stamina = stamina;
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        this.stamina = stamina;
        staminaSlider.value = stamina;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(maxHealth);
        SetStamina(maxStamina);
    }
}
