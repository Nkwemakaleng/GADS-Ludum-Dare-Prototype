using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LightingController : MonoBehaviour
{
    private GameObject light;
    private GameObject player;

    private ParticleSystem particles;

    private float lightDuration = 15f;
    //private float lightRange = 2f;
    private bool playerInRange;
    
    private static bool canSummon;
    private static bool canDemonMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particles = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        playerInRange = false;
        light = this.gameObject;
        particles.Stop();
    }

    private void Update()
    {
        //float distanceBetween = Vector3.Distance(player.transform.position, light.transform.position);

        if (light.GetComponent<Light>().intensity > 0.5F)
        {
            particles.Play();
        }
            if (!playerInRange)
        {
            lightDuration -= Time.deltaTime;

            //Adjusting the brightness/intensity of light sources according to time:
            light.GetComponent<Light>().intensity = lightDuration * 0.1538461538461538F;

            if (light.GetComponent<Light>().intensity < 0.5F)
            {
                particles.Stop();
                light.GetComponent<Light>().enabled = false;
            }
        }
        else {
            light.GetComponent<Light>().intensity = 2f;
            lightDuration = 15f;
        }
    }

    void OnTriggerEnter(UnityEngine.Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            light.GetComponent<Light>().enabled = true;
            playerInRange = true;
        }
    }

    void OnTriggerExit(UnityEngine.Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
