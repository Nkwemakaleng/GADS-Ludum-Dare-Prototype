// SFXManager.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SFXManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerMove, 
        PlayerHit, 
        PlayerJump,
        PlayerIdle,
        Helper1Move,
        Helper1Ability,
        Helper2Move,
        Helper2Ability,
        ButtonHover,
        ButtonClick
        
    }
    // Singleton instance
    private static SFXManager instance;

    // AudioSource component
    private AudioSource audioSource;

    // HashMap to store AudioClips
    private Dictionary<Sound, AudioClip> soundMap = new Dictionary<Sound, AudioClip>();

    public static Dictionary<Sound, float> SoundTimer;

    // Expose a List of AudioClips in the Unity inspector
   // public List<AudioClip> soundClips ;

    // Static reference to the SFXManager instance
    public static SFXManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SFXManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("SFXManager");
                    instance = obj.AddComponent<SFXManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        // Ensure there is only one instance of SFXManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Get the AudioSource component
            audioSource = gameObject.GetComponent<AudioSource>();

            // Populate the soundMap using the provided soundClips List in game assets 
            foreach (GameAssets.AudioList soundClip in GameAssets.i.soundClip)
            {
              
                soundMap.Add(soundClip.sound, soundClip.audioClip);
                Debug.Log("song " + soundClip.sound.ToString() );
            }
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    // Public method to play sound by name
    public void PlaySound(Sound sound)
    {
        //CanPlaySound(sound)
        if (CanPlaySound(sound) )
        {
                    if (soundMap.ContainsKey(sound))
                    {
                        audioSource.PlayOneShot(soundMap[sound]);
                    }
                    else
                    {
                        Debug.LogWarning("Sound not found: " + sound.ToString() );
                    }
        }
    }

    private static  bool CanPlaySound(Sound sound)
    { 
        switch (sound)
        {
          default:
              return true;
          case Sound.PlayerMove:
              if (SoundTimer.ContainsKey(Sound.PlayerMove))
              {
                        float lastTimePlayed = SoundTimer[sound];
              }
              break;
        }     
        
        
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}
