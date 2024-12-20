using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isHit = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //If boss hit player
        if (other.gameObject.tag == "Player" && isHit)
        {
            other.gameObject.GetComponent<PlayerBaseBehavior>().TakeDamage(30);
            Animator animator = other.gameObject.GetComponent<Animator>();
            animator.ResetTrigger("isHit");
            animator.SetTrigger("isHit");
            StartCoroutine(DelayCollision());
        }
    }


    private IEnumerator DelayCollision()
    {   
        isHit = false;
        yield return new WaitForSeconds(0.5f);
        isHit = true;
    }
}
