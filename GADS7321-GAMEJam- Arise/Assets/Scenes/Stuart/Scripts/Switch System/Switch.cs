using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //VARIABLES
    public bool isActive = false;
    [SerializeField] Material unactiveMat;
    [SerializeField] Material activeMat;
    private GameObject demon;

    void Update()
    {
        if (isActive)
        {
            GetComponent<MeshRenderer>().material = activeMat;
        }
        else {
            GetComponent<MeshRenderer>().material = unactiveMat;
        }

        if (demon.IsDestroyed()) {
            isActive = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Demon"))
        {
            demon = collision.gameObject;
            isActive = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Demon"))
        {
            demon = null;
            isActive = false;
        }
    }
}
