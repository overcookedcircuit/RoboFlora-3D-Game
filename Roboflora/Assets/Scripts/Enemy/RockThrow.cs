using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    private bool canHit;
    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
        StartCoroutine(DelayDeath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && canHit)
        {
            canHit = false;
            other.gameObject.GetComponent<Player>().TakeDamage(50);
            Debug.Log(other.gameObject.GetComponent<Player>().currentHealth);
            Animator animator = other.gameObject.GetComponent<Animator>();
            animator.ResetTrigger("isHit");
            animator.SetTrigger("isHit");
        }
    }

    private IEnumerator DelayDeath()
    {   
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
