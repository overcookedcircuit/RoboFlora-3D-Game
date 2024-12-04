using System.Collections;
using UnityEngine;

public class HeavyWeapon : GunBehavior
{
    private float currentCharge;
    public float chargeNeeded;
    public float laserDuration = 0.65f;

    public GameObject beam;
    public GameObject triggerBeam;
    public Transform beamPosition;
    Ray ray;
    RaycastHit rayHit;

    void Start(){
        beam.SetActive(false);
        triggerBeam.SetActive(false);
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
        beam.transform.position = beamPosition.position;
        beam.SetActive(true);
        triggerBeam.transform.position = beamPosition.position;
        triggerBeam.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        beam.SetActive(false);
        triggerBeam.SetActive(false);
    }
    public override void StopFiring()
    {
        isFiring = false;
    }

    public override void ResetCharge(){
        currentCharge = 0;
    }
}
