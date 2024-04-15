using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    //OBJECT VARIABLES
    private Rigidbody playerRB;
    private DemonManager dm;

    //RUNNING FX
    private ParticleSystem runningFX;
    private GameObject runningFXobject;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        dm = GetComponent<DemonManager>();

        //RUNNING FX
        runningFX = transform.GetChild(3).GetComponent<ParticleSystem>();
        runningFXobject = transform.GetChild(3).gameObject;
    }

    void Update()
    {
        //RUNNING FX CONDITIONS
        if (playerRB.velocity.x > 0 && dm.isGrounded)
        {
            runningFX.Emit(1);
        }
        else if (playerRB.velocity.x < 0 && dm.isGrounded)
        {
            runningFX.Emit(1);
        }
        else {
            runningFX.Emit(0);
        }
    }
}
