using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    AudioSource animationSoundPlayer;
    void Start()
    {
        animationSoundPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void PlayerFootstepSound()
    {
        animationSoundPlayer.Play();
    }    

}
