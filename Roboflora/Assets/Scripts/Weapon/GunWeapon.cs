using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{

    public bool isFiring;
    public Transform bulletSpawnPoint;
    public Transform bulletEndPoint;
    public TrailRenderer trailRenderer;
    Ray ray;
    RaycastHit rayHit;

    void Start(){
        isFiring = false;
    }

    public void StartFiring()
    {
        isFiring = true;
        ray.origin = bulletSpawnPoint.position;
        ray.direction = bulletEndPoint.position - bulletSpawnPoint.position;

        var tracer = Instantiate(trailRenderer, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);
        if(Physics.Raycast(ray, out rayHit)){
            tracer.transform.position = rayHit.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
