using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneScript : MonoBehaviour
{
    private void Update()
    {
       
    }
    #region MainMenu
    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }
    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
