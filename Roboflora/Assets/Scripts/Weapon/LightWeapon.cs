using UnityEngine;

public class LightWeapon : GunBehavior
{
    Ray ray;
    RaycastHit rayHit;
    private AudioSource audioSource;
    void Start(){
        isFiring = false;
        audioSource = GetComponent<AudioSource>();
    }

    public override void StartFiring()
    {
        if (isFiring) return; // Prevent firing repeatedly while already firing.

        isFiring = true;
        audioSource.Play();

        // Set the ray's origin to the bullet spawn point's current position.
        ray.origin = bulletSpawnPoint.position;
        // Calculate the direction dynamically based on the bullet end point's current position.
        ray.direction = bulletEndPoint.destination - bulletSpawnPoint.position;
        // Instantiate the tracer trail at the updated bullet spawn point position.
        var tracer = Instantiate(trailRenderer, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);
        tracer.transform.position = ray.origin + ray.direction * bulletEndPoint.maxTargetRange;
        Destroy(tracer, 1f);
        if(bulletEndPoint.hitTarget != null){
            bulletImpactEffect.transform.position = bulletEndPoint.destination;
            bulletImpactEffect.GetComponent<ParticleSystem>().Play();
            
            if(bulletEndPoint.hitTarget.gameObject.tag == "Enemy"){
                bulletEndPoint.hitTarget.gameObject.GetComponent<EnemyBaseBehavior>().GetHurt(10);
            }
        }  
        
    }


    public override void StopFiring()
    {
        isFiring = false;
    }

    public override void ResetCharge(){

    }
}
