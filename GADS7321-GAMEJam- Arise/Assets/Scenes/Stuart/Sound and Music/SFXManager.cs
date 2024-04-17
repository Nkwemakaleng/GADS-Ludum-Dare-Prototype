// SFXManager.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        ButtonClick,
        HelperSummon
        
    }
    // Singleton instance
    private static SFXManager instance;

    // AudioSource component
    public static AudioSource audioSource;

    // HashMap to store AudioClips
    private Dictionary<Sound, AudioClip> soundMap = new Dictionary<Sound, AudioClip>();

    public static Dictionary<Sound, float> SoundTimer;

    public static void Initialize()
    {
        // add any sound that will play like cancer ie moving sounds or sounds that play in quick succession 
        SoundTimer = new Dictionary<Sound, float>();
        SoundTimer[Sound.PlayerMove] = 0f;
        SoundTimer[Sound.Helper1Move] = 0f;
        SoundTimer[Sound.Helper2Move] = 0f;
        
    }

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

    /*private static  bool CanPlaySound(Sound sound)// This is to make sure certain songs dont play too much 
    { 
        switch (sound)
        {
          default:
              return true;
          case Sound.PlayerMove:
              if (SoundTimer.ContainsKey(sound))
              {
                        float lastTimePlayed = SoundTimer[sound];
                        float playerMoveTimeMax = 2.75f; // change this variable based on the sound. I recommend putting the almost the full length of the sfx
                        if (lastTimePlayed + playerMoveTimeMax < Time.time)
                        {
                            SoundTimer[sound] = Time.time;
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
              }
              else
              {
                  return false;
              }
          case Sound.Helper1Move:
              if (SoundTimer.ContainsKey(sound))
              {
                  float lastTimePlayed = SoundTimer[sound];
                  float helperMoveTimeMax = 0.15f;
                  if (lastTimePlayed + helperMoveTimeMax < Time.time)
                  {
                      SoundTimer[sound] = Time.time;
                      return true;
                  }
                  else 
                  {
                      return false;
                  }
              }
              else
              {
                  return false;
              }
        }
    }*/

    private static bool CanPlaySound(Sound sound)
    {
        // Dictionary to store maximum time intervals for each sound type
        Dictionary<Sound, float> maxTimeIntervals = new Dictionary<Sound, float>
        {
            { Sound.PlayerMove, 2.85f }, // Adjust these values based on the sound types 2.85f
            { Sound.Helper1Move, 2.85f },
            { Sound.Helper2Move, 0.15f }
            
            // Add more sound types and their corresponding max time intervals if needed
        };

        // Check if the sound exists in the dictionary and has a maximum time interval
        if (maxTimeIntervals.ContainsKey(sound))
        {
            float maxTime = maxTimeIntervals[sound];

            if (SoundTimer.ContainsKey(sound))
            {
                float lastTimePlayed = SoundTimer[sound];

                // Check if enough time has passed since the last play
                if (lastTimePlayed + maxTime < Time.time)
                {
                    SoundTimer[sound] = Time.time;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // If the sound timer doesn't contain the sound, allow playing the sound
                SoundTimer[sound] = Time.time;
                return true;
            }
        }

        // If the sound type is not defined in the maxTimeIntervals dictionary, allow playing the sound
        return true;
    }
    public void SetVolume(float volume)
    {
        // Clamp volume between 0 and 1
        volume = Mathf.Clamp01(volume);

        // Set the volume of the AudioSource component
        audioSource.volume = volume;
    }

    // Method to play button hover sound
    public void ButtonClick()
    {
        SFXManager.Instance.PlaySound(Sound.ButtonClick);
    }

    public void ButtonHover()
    {
        SFXManager.Instance.PlaySound(Sound.ButtonHover);
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
