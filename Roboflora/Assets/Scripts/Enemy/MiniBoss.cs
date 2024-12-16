using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MiniBoss : EnemyBaseBehavior
{
    public HealthBar healthBar; // Reference to the HealthBar component

    public GameObject weapon;

    private AudioSource attackSound;

    private ParticleSystem dieEffect;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
        attackSound = GetComponent<AudioSource>();
        dieEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void EnableWeaponCollision(){
        weapon.GetComponent<BoxCollider>().enabled = true;
        attackSound.Play();
        Debug.Log("Enable Collision");
    }

    public void DisableWeaponCollision(){
        weapon.GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Disable Collision");
    }

    public override void GetHurt(int damage)
    {
        this.health -= damage;
        healthBar.SetHealth(health); // Update the health bar
        if (health <= 0)
        {
            Die();
        }
        Debug.Log("HEALTH: " +this.health);
    }

    public override void Die()
    {
        SkinnedMeshRenderer[] skins = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer skin in skins){
            skin.enabled = false;
        }
        dieEffect.Play();
        Destroy(this.gameObject, 0.3f);
    }

    
}
