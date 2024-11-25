using System.Collections;
using UnityEngine;

public class HeavyWeapon : GunBehavior
{
    private float currentCharge;
    public float chargeNeeded;
    public float laserDuration = 0.85f;
    private LineRenderer lineRenderer;
    Ray ray;
    RaycastHit rayHit;

    void Start(){
        chargeNeeded = 3;
        isFiring = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update(){
        //Charge the weapon
        currentCharge += Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0, chargeNeeded);
    }
    public override void StartFiring()
    {   
        //If the charge time is long enough, fire a beam and reset the timer
        if(currentCharge >= chargeNeeded){
            isFiring = true;
            ray.origin = bulletSpawnPoint.position;
            ray.direction = bulletEndPoint.destination - bulletSpawnPoint.position;
            lineRenderer.SetPosition(0, ray.origin);
            if(Physics.Raycast(ray, out rayHit)){
                // tracer.SetPosition(1, rayHit.point);
                lineRenderer.SetPosition(1, rayHit.point);
            }else{
                lineRenderer.SetPosition(1,  ray.origin + ray.direction * bulletEndPoint.maxTargetRange);
            }
            StartCoroutine(ShootLaser());
            currentCharge = 0;
        }else{
            Debug.Log("Wait: " + currentCharge);
        }
        
    }

    IEnumerator ShootLaser(){
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lineRenderer.enabled = false;
    }
    public override void StopFiring()
    {
        isFiring = false;
    }
}
