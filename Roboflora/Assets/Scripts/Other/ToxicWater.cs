using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicWater : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.currentHealth = 0; // Set health to 0
                playerScript.healthBar.SetHealth(playerScript.currentHealth); // Update health bar
                playerScript.Die(); // Trigger death if necessary
            }
        }
    }
}
