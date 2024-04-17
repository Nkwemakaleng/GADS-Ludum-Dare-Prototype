using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject deathPanel;
    //public GameObject GameGUIPanel;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        deathPanel.SetActive(false);
        //GameGUIPanel.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.active == false && settingsPanel.active == false && deathPanel.active == false) 
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }   
            else if(pausePanel.active == true)
            {
                Time.timeScale = 1;
                //GameGUIPanel.SetActive(false);
                pausePanel.SetActive(false);
            }
            else if (settingsPanel.active == true)
            {
                pausePanel.SetActive(true);
                settingsPanel.SetActive(false);
            }
            // else if (GameGUIPanel.active == true)
            // {
            //     
            //     pausePanel.SetActive(true);
            // }
        }

    }

  

    #region In-Game
    public void GamePaused()
    {
        if (pausePanel.active == false)
        { 
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void GameResume()
    {
        if (pausePanel.active == true)
        {
            pausePanel.SetActive(false);
            //GameGUIPanel.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    public void GameSettingsOn()
    {
        if (settingsPanel.active == false)
        {
            pausePanel.SetActive(false) ;
            settingsPanel.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void GameSettingsOff()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (deathPanel.active == false)
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    #endregion

}
