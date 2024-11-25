using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera zoomCam;
    public CinemachineBrain cinemaBrain;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject lightGun;
    public GameObject heavyGun;
    private GunBehavior gunController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gunController = lightGun.GetComponentInChildren<GunBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
           TakeDamage(20);
        }

        if(Input.GetMouseButtonDown(1)){
            HeavyAttack();
        }

        if (Input.GetMouseButtonUp(1))
        {
            gunController = lightGun.GetComponentInChildren<GunBehavior>();
            defaultCam.Priority = 10;
            zoomCam.Priority = 0;
        }
        Shooting();
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Shooting(){
        if(Input.GetButtonDown("Fire1")){
            gunController.StartFiring();
        }

        if(Input.GetButtonUp("Fire1")){
            gunController.StopFiring();
        }
    }

    void HeavyAttack(){
        gunController = heavyGun.GetComponentInChildren<GunBehavior>();
        defaultCam.Priority = 0;
        zoomCam.Priority = 10;
    }
}
