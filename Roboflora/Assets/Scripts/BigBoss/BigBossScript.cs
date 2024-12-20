using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BigBossScript : EnemyBaseBehavior
{
    private bool hitPlayer;
    public HealthBar healthBar; // Reference to the HealthBar component

    public GameObject rock;
    public GameObject rockToThrow;

    public Transform playerTransform;
    public float launchVelocity = 700f;

    private ParticleSystem dieEffect;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
        hitPlayer = false;
        dieEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        //If boss hit player
        if (other.gameObject.tag == "Player" && hitPlayer == true)
        {
            ContactPlayer(other);
        }
    }

    public void ContactPlayer(Collider other)
    {
        other.gameObject.GetComponent<PlayerBaseBehavior>().TakeDamage(50);
        Animator animator = other.gameObject.GetComponent<Animator>();
        animator.ResetTrigger("isHit");
        animator.SetTrigger("isHit");
        //Push Player away from boss
        float distance = 25;
        Vector3 dir = other.transform.position - transform.position; // Direction away from the boss
        dir = dir.normalized;
        other.transform.position += dir * distance * Time.deltaTime;

        DisableHitPlayer();
    }

    public void EnableHitPlayer()
    {
        hitPlayer = true;
    }

    public void DisableHitPlayer()
    {
        hitPlayer = false;
    }

    public override void GetHurt(int damage)
    {
        this.health -= damage;
        healthBar.SetHealth(health); // Update the health bar
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        SkinnedMeshRenderer[] skins = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer skin in skins){
            skin.enabled = false;
        }
        dieEffect.Play();
        Destroy(this.gameObject, 1f);
    }

    public void EnableRock(){
        //ROCK DEFAULT POSITION x=0, y =1.13, Z = 1.37
        rock.SetActive(true);
    }


    public void ThrowRock(){
        rock.SetActive(false);

         // Instantiate the rock to throw at the rock placeholder's position
        GameObject ball = Instantiate(rockToThrow, rock.transform.position, Quaternion.identity);


        Rigidbody rb = ball.GetComponent<Rigidbody>();
                                                     
          // Calculate the direction toward the player's location
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Add force to throw the rock toward the player
        rb.AddForce(direction * launchVelocity);
    }
}
