using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
 private static GameAssets _i;

 public static GameAssets i
 {
  get
  {
   if (_i == null) 
    _i = (Instantiate(Resources.Load("Game Assets")) as GameObject).GetComponent<GameAssets>();
   return _i; 
  }
  
 }
 // Expose a List of AudioClips in the Unity inspector
 public List<AudioList> soundClip;
 [System.Serializable]
public class AudioList
 {
  public SFXManager.Sound sound;
  public AudioClip audioClip;
 }
}
