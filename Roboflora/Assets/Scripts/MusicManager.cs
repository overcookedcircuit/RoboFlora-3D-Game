using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    AudioSource audioSource;
    public bool isFightBoss;
    // Start is called before the first frame update
    void Start()
    {
        isFightBoss = false;
        audioSource = GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();
        // Check if the name of the current Active Scene is your first Scene.
        if (scene.name == "MainMenu")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }else if(scene.name == "Tutorial"){
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }else if(scene.name == "world1"){
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int index){
        if(audioSource.clip != audioClips[index]){
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }
}
