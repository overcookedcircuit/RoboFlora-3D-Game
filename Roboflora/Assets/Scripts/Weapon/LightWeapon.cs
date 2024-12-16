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
        audioSource.Play();
        //Get the direction of the crosshair and cast it
        isFiring = true;
        ray.origin = bulletSpawnPoint.position;
        ray.direction = bulletEndPoint.destination - bulletSpawnPoint.position;
        Debug.Log("End Point: "  + bulletEndPoint.destination);
        var tracer = Instantiate(trailRenderer, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if(Physics.Raycast(ray, out rayHit)){
            var impactEffect = bulletImpactEffect.GetComponent<ParticleSystem>();
            var main  = impactEffect.main;
            main.startSize = 1;
            bulletImpactEffect.transform.position = rayHit.point;
            bulletImpactEffect.GetComponent<ParticleSystem>().Play();

            tracer.transform.position = rayHit.point;
            //if Hit GameObject with 'Enemy' Tag, deal damage
            if(rayHit.transform.gameObject.tag == "Enemy"){
                rayHit.transform.gameObject.GetComponent<EnemyBaseBehavior>().GetHurt(10);
            }
        }else{
            tracer.transform.position = ray.origin + ray.direction * bulletEndPoint.maxTargetRange;
        }
    }

    public override void StopFiring()
    {
        isFiring = false;
    }

    public override void ResetCharge(){

    }
}
