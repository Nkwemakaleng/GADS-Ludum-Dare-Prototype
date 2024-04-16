using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LightingController : MonoBehaviour
{
    private Light light;
    private GameObject player;
    private ParticleSystem particles;

    private float lightDuration = 15f;
    private float maxLightIntensity = 2f;
    private float minLightIntensity = 0.5f;
    private bool playerInRange;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particles = GetComponentInChildren<ParticleSystem>();
        playerInRange = false;
        light = GetComponent<Light>();
        light.intensity = 0f;
        particles.Stop();
    }

    private void Update()
    {
        if (playerInRange)
        {
            lightDuration = 15f;
            light.intensity = Mathf.Lerp(light.intensity, maxLightIntensity, Time.deltaTime * 2f);

            if (!particles.isPlaying)
            {
                particles.Play();
            }
        }
        else
        {
            lightDuration -= Time.deltaTime;
            light.intensity = Mathf.Lerp(light.intensity, 0f, Time.deltaTime * 0.5f);

            if (light.intensity <= 0.01f)
            {
                light.intensity = 0f;
                particles.Stop();
            }
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
