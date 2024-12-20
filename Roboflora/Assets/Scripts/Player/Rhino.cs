using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rhino : PlayerBaseBehavior
{
    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage) {
        damage -= 20;
        if(damage <= 0)
            damage = 1;
        playerManager.health -= damage;
        playerManager.SetHealth(playerManager.health);
        if(playerManager.health <= 0){
            Die();
        }else{
            GetComponent<PlayerAudio>().PlayHurtSound();
        }
    }

    public override void  Die(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name); 
    }
}
