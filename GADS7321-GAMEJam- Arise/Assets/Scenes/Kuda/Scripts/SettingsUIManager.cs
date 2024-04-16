using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUIManager : MonoBehaviour
{
    public GameObject displayPanel;
    public GameObject volumePanel;

    public void VolumeButton()
    {
        if(volumePanel.active == false)
        {
            volumePanel.SetActive(true);
            displayPanel.SetActive(false);
        }
    }
    public void DisplayButton()
    {
        if(displayPanel.active == false)
        {
            displayPanel.SetActive(true);
            volumePanel.SetActive(false);
        }
    }
}
