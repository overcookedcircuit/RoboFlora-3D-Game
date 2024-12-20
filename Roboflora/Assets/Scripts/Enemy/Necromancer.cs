using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : EnemyBaseBehavior

{
    public HealthBar healthBar; // Reference to the HealthBar component

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    public ParticleSystem dieEffect;

    public ParticleSystem spellEffect;

    public GameObject beam;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
        audioSource = GetComponent<AudioSource>();
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

    public void AttackSpell(){
        audioSource.clip = audioClips[0];
        audioSource.Play();
        spellEffect.Play();
        beam.SetActive(true);
    }

     public void DisableSpell(){
        spellEffect.Stop();
        audioSource.Stop();
        beam.SetActive(false);
    }

    
}