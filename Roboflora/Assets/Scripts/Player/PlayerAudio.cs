using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playWalkSound()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void playJumpSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void PlayHurtSound()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public void PlayRhinoAttackSound()
    {
        Debug.Log("Playing called damage");

        if (audioClips[3] != null)
        {
            Debug.Log("Playing rhino damage");
            audioSource.clip = audioClips[3];
            audioSource.Play();
        }
    }
}
