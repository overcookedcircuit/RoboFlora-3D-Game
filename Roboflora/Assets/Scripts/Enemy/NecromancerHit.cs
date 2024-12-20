using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //If boss hit player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBaseBehavior>().TakeDamage(1);
        }
    }
}
