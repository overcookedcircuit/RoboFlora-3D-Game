using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit rayHit;

    public GameObject hitTarget;

    public float maxTargetRange = 50f;
    public Vector3 destination;

    public GameObject target;

    public GameObject bulletImpactEffect;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the camera's position in the forward direction
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if(Input.GetButtonDown("Fire1")){
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found!");
                return;
            }


            if (Physics.Raycast(ray, out rayHit, maxTargetRange))
            {
                destination = rayHit.point;
                hitTarget = rayHit.collider.gameObject;
                Debug.DrawLine(mainCamera.transform.position, destination, Color.red, 2f);
            }
            else
            {
                // If the ray doesn't hit anything, position the crosshair at maxTargetRange distance
                //transform.position = mainCamera.transform.position + mainCamera.transform.forward * maxTargetRange;
                destination = mainCamera.transform.position + mainCamera.transform.forward * maxTargetRange;
                hitTarget = null;
            }
        }
        
    }

}
