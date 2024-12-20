using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : PlayerBaseBehavior
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
        damage += 20;
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
