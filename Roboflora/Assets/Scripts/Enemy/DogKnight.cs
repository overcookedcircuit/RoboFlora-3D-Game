using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnight : EnemyBaseBehavior
{
    public HealthBar healthBar;
    public ParticleSystem dieEffect;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);        
    }

    public void EnableWeaponCollision(){
        weapon.GetComponent<BoxCollider>().enabled = true;
        //attackSound.Play();
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
