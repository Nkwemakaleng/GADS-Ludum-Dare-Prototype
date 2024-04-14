using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Activation : MonoBehaviour
{
    //VARIABLES
    private Switch switchObject;
    private bool activationBool;
    private int activationIndex = 0;

    //DOOR VARIABLES
    private bool doorOpened = false;

    //GETS BOOL FROM PARENT, DETERMINES ACTIVATION TYPE AND THUS ACTIONS
    void Start()
    {
        switchObject = GetComponentInParent<Switch>();
        activationType(this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        //GETS SWITCH BOOLEAN
        if (switchObject != null) {
            activationBool = switchObject.isActive;
            //Debug.Log(activationBool + "    " + activationIndex);
        }

        //CONDITIONS FOR DOOR
        if (activationIndex.Equals(1) && activationBool)
        {
            if (!doorOpened) {
                openDoor();
                doorOpened = true;
            }
        }
        if (activationIndex.Equals(1) && !activationBool)
        {
            if (doorOpened) {
                closeDoor();
                doorOpened = false;
            } 
        }
    }

    private void openDoor() {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 4F, transform.GetChild(0).position.z);
    }

    private void closeDoor() {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 4F, transform.GetChild(0).position.z);
    }

    private void activationType(string tag) {
        //DOOR CASE = 1
        if (tag.Equals("Door")) {
            activationIndex = 1;
        }    
    }


}
