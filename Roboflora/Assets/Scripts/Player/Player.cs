using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(Input.GetMouseButtonDown(1)){
            HeavyAttack();
        }

        if (Input.GetMouseButtonUp(1))
        {
            gunController = lightGun.GetComponentInChildren<GunBehavior>();
            gunController.StopFiring();
            defaultCam.Priority = 10;
            zoomCam.Priority = 0;
        }
        Shooting();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        this.gameObject.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        gunController.ResetCharge();
        defaultCam.Priority = 0;
        zoomCam.Priority = 10;
    }
}
