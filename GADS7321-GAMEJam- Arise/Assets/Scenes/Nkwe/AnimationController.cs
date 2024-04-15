using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SFXManager.Instance.PlaySound(SFXManager.Sound.PlayerHit);
    }

    // Update is called once per frame
    void Update()
    {
        SFXManager.Instance.PlaySound(SFXManager.Sound.PlayerMove);
    }
}
