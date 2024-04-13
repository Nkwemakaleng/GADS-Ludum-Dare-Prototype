using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject deathPanel;

    AutoExposure exposure;

    private void Start()
    {
        brightness.TryGetSettings(out exposure);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        deathPanel.SetActive(false);
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
                pausePanel.SetActive(false);
            }
            else if (settingsPanel.active == true)
            {
                pausePanel.SetActive(true);
                settingsPanel.SetActive(false);
            }
            else if (deathPanel.active == true)
            {
                
            }
        }

    }

    #region MainMenu
    public void Play()
    {
        SceneManager.LoadScene("KudaPlay");
    }

    public void Settings()
    {
        SceneManager.LoadScene("KudaSettings");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("KudaMainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Brightness
    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .05f;
        }
    }
    #endregion

    #region In-Game
    public void GamePaused()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        { 
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GameSettings()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else 
        {
            pausePanel.SetActive(false) ;
            settingsPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GameOver()
    {
        if (deathPanel.activeSelf)
        {
            deathPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else 
        { 
            deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    #endregion

}
