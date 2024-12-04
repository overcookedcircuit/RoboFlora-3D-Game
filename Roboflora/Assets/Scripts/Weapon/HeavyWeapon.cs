using System.Collections;
using UnityEngine;

public class HeavyWeapon : GunBehavior
{
    private float currentCharge;
    public float chargeNeeded;
    public float laserDuration = 0.85f;

    public GameObject beam;
    Ray ray;
    RaycastHit rayHit;

    void Start(){
        beam.SetActive(false);
        currentCharge = 0;
        chargeNeeded = 3;
        isFiring = false;
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
            StartCoroutine(ShootLaser());
            currentCharge = 0;
        }else{
            Debug.Log("Wait: " + currentCharge);
        }
        
    }

    IEnumerator ShootLaser(){
        beam.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        beam.SetActive(false);
    }
    public override void StopFiring()
    {
        isFiring = false;
    }

    public override void ResetCharge(){
        currentCharge = 0;
    }
}
