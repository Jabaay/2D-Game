using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

   [SerializeField] private GameObject pauseMenu;
   [SerializeField] private GameObject controls;

    private bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        pauseGame();
    }


    // Pause the game.
    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }


    // Resume game.
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        StartCoroutine(PauseResume());
        Time.timeScale = 1f;
    }

    // Helper method for ResumeGame().
    IEnumerator PauseResume()
    {
        yield return new WaitForSeconds(2f);
    }


    // Go back to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



    // Quit game.
    public void QuitGame()
    {
        Application.Quit();
    }

}
