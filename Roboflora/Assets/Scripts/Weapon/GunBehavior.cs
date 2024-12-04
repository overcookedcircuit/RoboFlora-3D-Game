using UnityEngine;

public abstract class GunBehavior : MonoBehaviour{
    public bool isFiring;
    public Transform bulletSpawnPoint;
    public CrosshairTarget bulletEndPoint;
    public GameObject bulletImpactEffect;
    public TrailRenderer trailRenderer;

    public abstract void StartFiring();

    public abstract void StopFiring();

    public abstract void ResetCharge();
}