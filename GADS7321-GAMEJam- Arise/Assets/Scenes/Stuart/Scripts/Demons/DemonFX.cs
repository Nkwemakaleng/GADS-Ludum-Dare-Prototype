using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFX : MonoBehaviour
{
    //VARIABLES
    [SerializeField] GameObject player;
    private DemonManager dm;
    private GameObject activeDemon;

    private bool playedSummon = false;
    private float particleSpeed = 10F;

    void Start()
    {
        dm = player.GetComponent<DemonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Demon") != null)
        {
            activeDemon = GameObject.FindGameObjectWithTag("Demon");
        }
        
        //PLAYS PARTICLES WHEN CHANGING BETWEEN DEMON AND PLAYER
        Vector3 target;

        if (!dm.activeController)
        {
            target = player.transform.position;
        }
        else {
            target = activeDemon.transform.position;
        }

        if (activeDemon = null)
        {
            target = player.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, particleSpeed * Time.deltaTime);
    }
}
