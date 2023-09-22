using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }
    
    public void StartTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
