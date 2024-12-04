using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnGunImpact : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject bulletImpactEffect;

    private void OnTriggerEnter(Collider other)
    {
        var collisionPoint = other.ClosestPoint(transform.position);
        var impactEffect = bulletImpactEffect.GetComponent<ParticleSystem>();
        var main  = impactEffect.main;
        main.startSize = 5;
        // Instantiate a new bullet impact effect at each contact point
        GameObject impact = Instantiate(bulletImpactEffect, collisionPoint, Quaternion.identity);
        impact.GetComponent<ParticleSystem>().Play();
        Destroy(impact, 1f);

        if(other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<EnemyBaseBehavior>().GetHurt(50);
    }
}
