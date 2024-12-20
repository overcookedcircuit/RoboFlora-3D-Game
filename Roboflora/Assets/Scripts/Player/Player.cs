using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : PlayerBaseBehavior
{
    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera zoomCam;
    public CinemachineBrain cinemaBrain;
    // public int maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    public GameObject chargeBar;
    public GameObject lightGun;
    public GameObject heavyGun;

    public MusicManager musicManager;
    private GunBehavior gunController;
    public PlayerManager playerManager;



    // Start is called before the first frame update
    void Start()
    {
        // playerManager.SetMaxHealth(maxHealth);
        
        // playerManager.health = maxHealth;

        // currentHealth = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
        gunController = lightGun.GetComponentInChildren<GunBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.health < playerManager.maxHealth)
        {
            playerManager.SetHealth(playerManager.health  += 10f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            HeavyAttack();
        }

        if (Input.GetMouseButtonUp(1))
        {
            gunController = lightGun.GetComponentInChildren<GunBehavior>();
            gunController.StopFiring();
            defaultCam.Priority = 10;
            zoomCam.Priority = 0;
            chargeBar.SetActive(false);
            GetComponent<PlayerMovement>().cameraTransform = defaultCam.transform;
        }
        if (Time.timeScale != 0) {
            Shooting();
        }
    }


    void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunController.StartFiring();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            gunController.StopFiring();
        }
    }
    void HeavyAttack()
    {
        chargeBar.SetActive(true);
        chargeBar.GetComponent<ChargeBar>().SetMaxCharge(3);
        gunController = heavyGun.GetComponentInChildren<GunBehavior>();
        gunController.ResetCharge();
        defaultCam.Priority = 0;
        zoomCam.Priority = 10;
        GetComponent<PlayerMovement>().cameraTransform = zoomCam.transform;
    }

    public override void TakeDamage(int damage)
    {
        playerManager.health -= damage;
        playerManager.SetHealth(playerManager.health);
        if (playerManager.health <= 0)
        {
        }
        else
        {
            GetComponent<PlayerAudio>().PlayHurtSound();
        }
    }

    public override void Die()
    {
        playerManager.PlayerDied();
    }
}
