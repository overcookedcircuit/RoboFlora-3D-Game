using UnityEngine;

public class LightWeapon : GunBehavior
{
    Ray ray;
    RaycastHit rayHit;

    void Start(){
        isFiring = false;
    }

    public override void StartFiring()
    {
        isFiring = true;
        ray.origin = bulletSpawnPoint.position;
        
        ray.direction = bulletEndPoint.destination - bulletSpawnPoint.position;
        Debug.Log("direction: " +  ray.direction);
        var tracer = Instantiate(trailRenderer, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);
        if(Physics.Raycast(ray, out rayHit)){
            tracer.transform.position = rayHit.point;
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
