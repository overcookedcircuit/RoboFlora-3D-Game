using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{

    public MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {             
        if(other.gameObject.tag == "Player")
            if(!musicManager.isFightBoss){
                musicManager.PlayMusic(3);
                musicManager.isFightBoss = true;
                Debug.Log("BOSS FIGHT");
            }else{
                musicManager.PlayMusic(2);
                musicManager.isFightBoss = false;
                Debug.Log("BOSS EXIT");
            }
    }
}
