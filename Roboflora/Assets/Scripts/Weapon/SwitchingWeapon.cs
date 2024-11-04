using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour
{   
    public GameObject lightWeapon;
    public GameObject heavyWeapon;

    public Transform leftLightHand;
    public Transform rightLightHand;

    public Transform leftHeavyHand;
    public Transform rightHeavyHand;

    public Transform leftHandLocation;
    public Transform rightHandLocation;
    // Start is called before the first frame update
    void Start()
    {
        lightWeapon.SetActive(true);
        heavyWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
           lightWeapon.SetActive(false);
           heavyWeapon.SetActive(true);
           leftHandLocation.position = leftHeavyHand.position;
           leftHandLocation.rotation = leftHeavyHand.rotation;
           rightHandLocation.position = rightHeavyHand.position;
           rightHandLocation.rotation = rightHeavyHand.rotation;
        }

        if (Input.GetMouseButtonUp(1))
        {
            lightWeapon.SetActive(true);
            heavyWeapon.SetActive(false);
           leftHandLocation.position = leftLightHand.position;
           leftHandLocation.rotation = leftLightHand.rotation;
           rightHandLocation.position = rightLightHand.position;
           rightHandLocation.rotation = rightLightHand.rotation;
        }
    }
}
