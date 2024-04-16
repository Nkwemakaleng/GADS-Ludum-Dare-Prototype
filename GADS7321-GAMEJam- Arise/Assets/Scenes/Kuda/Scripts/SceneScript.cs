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
        SceneManager.LoadScene("KudaPlay");
    }

    public void Settings()
    {
        SceneManager.LoadScene("KudaSettings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("KudaCredits");
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
}
