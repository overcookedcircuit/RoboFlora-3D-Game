using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGunImpact : MonoBehaviour
{
    public GameObject bulletImpactEffect;

    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision)
    {

        int numOfContact = collision.contactCount;
        ContactPoint[] contactPoints = new ContactPoint[numOfContact];
        collision.GetContacts(contactPoints);

        if(collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyBaseBehavior>().GetHurt(50);

        foreach (ContactPoint cp in contactPoints)
        {
            var impactEffect = bulletImpactEffect.GetComponent<ParticleSystem>();
            var main  = impactEffect.main;
            main.startSize = 5;
            // Instantiate a new bullet impact effect at each contact point
            GameObject impact = Instantiate(bulletImpactEffect, cp.point, Quaternion.LookRotation(cp.normal));
            impact.GetComponent<ParticleSystem>().Play();
            Destroy(impact, 1f); 
        }
    }
}
