using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public GunWeapon gun;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gun = GetComponentInChildren<GunWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
           TakeDamage(20);
        }
        Shooting();
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Shooting(){
        if(Input.GetButtonDown("Fire1")){
            gun.StartFiring();
        }

        if(Input.GetButtonUp("Fire1")){
            gun.StopFiring();
        }
    }
}
