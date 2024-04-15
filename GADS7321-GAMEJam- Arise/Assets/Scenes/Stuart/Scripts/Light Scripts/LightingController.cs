using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    private GameObject light;
    private GameObject player;
    private float lightDuration = 15f;
    //private float lightRange = 2f;
    private bool playerInRange;
    
    private static bool canSummon;
    private static bool canDemonMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInRange = false;
        light = this.gameObject;
    }

    private void Update()
    {
        //float distanceBetween = Vector3.Distance(player.transform.position, light.transform.position);

        //if (distanceBetween <= lightRange) //If within range
        //{
        //    lightDuration = 15f;
        //    playerInRange = true;
        //    canSummon = true;

        //    light.GetComponent<Light>().enabled = true;
        //    light.GetComponent<Light>().intensity = 2f;
        //}
        //else if (distanceBetween > lightRange) //If outside range
        //{
        //    playerInRange = false;
        //}

        if (!playerInRange)
        {
            lightDuration -= Time.deltaTime;

            //Adjusting the brightness/intensity of light sources according to time:
            if (lightDuration < 13)
            {
                light.GetComponent<Light>().intensity = 1.8f;
            }
            if (lightDuration < 11)
            {
                light.GetComponent<Light>().intensity = 1.6f;
            }
            if (lightDuration < 9)
            {
                light.GetComponent<Light>().intensity = 1.4f;
            }
            if (lightDuration < 7)
            {
                light.GetComponent<Light>().intensity = 1.2f;
            }
            if (lightDuration < 5)
            {
                light.GetComponent<Light>().intensity = 1f;
            }
            if (lightDuration < 3)
            {
                light.GetComponent<Light>().intensity = 0.6f;
            }
            if (lightDuration < 1)
            {
                light.GetComponent<Light>().intensity = 0.3f;
            }
            if (lightDuration < 0)
            {
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
