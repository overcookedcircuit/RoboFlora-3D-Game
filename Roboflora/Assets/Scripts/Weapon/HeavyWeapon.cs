using System;
using System.Collections;
using UnityEngine;

public class HeavyWeapon : GunBehavior
{
    private float currentCharge;
    public float chargeNeeded;
    public float laserDuration = 0.65f;

    public GameObject triggerBeam;
    public Transform beamPosition;

    public ChargeBar chargeBar;

    public AudioClip[] audioClips;
    private AudioSource audioSource;
    Ray ray;
    RaycastHit rayHit;
    void Start(){
        triggerBeam.SetActive(false);
        currentCharge = 0;
        chargeNeeded = 3;
        isFiring = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        isChargeSound = false;
  
    }

    void Update(){
        //Charge the weapon
        currentCharge += Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0, chargeNeeded);
        if(chargeBar != null)
            chargeBar.SetCharge((int) currentCharge);
    }
    public override void StartFiring()
    {   
        //If the charge time is long enough, fire a beam and reset the timer
        if(currentCharge >= chargeNeeded){
            isFiring = true;
            StartCoroutine(ShootLaser());
            currentCharge = 0;
            audioSource.clip = audioClips[1];
            audioSource.Play();
            chargeBar.ResetChargeBar();
        }else{
            Debug.Log("Wait: " + currentCharge);
        }
        
    }

    IEnumerator ShootLaser(){
        triggerBeam.transform.position = beamPosition.position;
        triggerBeam.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        ResetCharge();
        triggerBeam.SetActive(false);
    }
    public override void StopFiring()
    {
        isFiring = false;
        isChargeSound = false;
    }

    public override void ResetCharge(){
        audioSource.clip = audioClips[0];
        currentCharge = 0;
        chargeBar.ResetChargeBar();
    }

}
