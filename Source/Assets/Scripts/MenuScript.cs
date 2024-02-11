using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    // Start game.
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }


    // Load options.
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }


    // Load main menu.
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




    // Quit game.
    public void QuitGame()
    {
        Application.Quit();
    }

}
