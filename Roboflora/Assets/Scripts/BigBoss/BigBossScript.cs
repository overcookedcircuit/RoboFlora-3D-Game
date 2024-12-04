using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BigBossScript : EnemyBaseBehavior
{
    private bool hitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        hitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.tag == "Player"){
    //         Debug.Log("I am hitting player");
    //     }
    // }
    private void OnTriggerStay(Collider other)
    {   
        //If boss hit player
        if(other.gameObject.tag == "Player" && hitPlayer == true){
            ContactPlayer(other);
        }
        Debug.Log(other.gameObject.name);
        
    }

    public void ContactPlayer(Collider other){
        other.gameObject.GetComponent<Player>().TakeDamage(50);
        Animator animator = other.gameObject.GetComponent<Animator>();
        animator.ResetTrigger("isHit");
        animator.SetTrigger("isHit");
        //Push Player away from boss
        float distance = 25;
        Vector3 dir = other.transform.position - transform.position; // Direction away from the boss
        dir = dir.normalized;
        other.transform.position += dir * distance * Time.deltaTime;

        DisableHitPlayer();
    }

    public void EnableHitPlayer(){
        hitPlayer = true;
    }

    public void DisableHitPlayer(){
        hitPlayer = false;
    }

    public override void GetHurt(int damage){
        this.health -= damage;
        if(health <= 0){
            Die();
        }
        Debug.Log("HEALTH: " +this.health);
    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }
}
