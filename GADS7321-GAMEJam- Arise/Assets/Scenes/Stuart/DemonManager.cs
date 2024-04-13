using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonManager : MonoBehaviour
{
    //OBJECT VARIABLES
    [SerializeField] GameObject[] demons;   
    GameObject activeDemon;

    //MATERIAL VARIABLES
    [SerializeField] Material standardMaterial;
    [SerializeField] Material selectedMaterial;

    //STATE VARIABLES
    public int demonIndex = 0;     //Which demon is being summoned
    public bool demonActive = false;       //Whether demon is summoned or not
    private bool isSpawned = false;        //Checks whether demon has been spawned
    public bool activeController = false;  //Controls whether player or demon is being controlled (false - player, true - demon)

    // Update is called once per frame
    void Update()
    {
        //WHETHER DEMON IS ACTIVE - ACTIVATING DEMON IS LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            demonActive = !demonActive;
        }

        //TOGGLES BETWEEN CONTROLLING DEMON AND PLAYER
        if (Input.GetMouseButtonDown(1))
        {
            activeController = !activeController;
        }

        //CHANGES WHICH DEMON IS SELECTED - CHANGE TO WORK WITH ARRAY
        //GOING LEFT
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("Left Pressed");

            if (demonIndex - 1 < 0)
            {
                demonIndex = demons.Length - 1;
            }
            else
            {
                demonIndex--;
            }
        }

            //GOING RIGHT
            if (Input.GetKeyDown("2"))
            {
                Debug.Log("Right Pressed");

                if (demonIndex + 1 > demons.Length-1)
                {
                    demonIndex = 0;
                }
                else
                {
                    demonIndex++;
                }
            }

            //CONTROLS DEMON SPAWN + DESPAWN
            if (demonActive)
            {
                if (!isSpawned)
                {
                    spawnDemon();
                    isSpawned = true;
                }
            }
            else
            {
                deleteDemon();
            }

            //DEMON + PLAYER CONTROL
            if (activeController)
            {
                if (activeDemon != null)
                    activeDemon.transform.position = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            }
            else
            {

                transform.position = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            }
    }
    //INSTANTIATES DEMON IN FRONT OF PLAYER
    public void spawnDemon() {
        activeDemon = Instantiate(demons[demonIndex], new Vector3(transform.position.x + 3F, 0F, 0F), Quaternion.identity);
    }

    //DELETES DEMON + REVERTS CONTROL VARIABLES
    public void deleteDemon() {
        Destroy(activeDemon);
        activeController = false;
        isSpawned = false;
    }
}
