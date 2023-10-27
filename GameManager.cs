using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver = true;

    private bool paused = false;

    [SerializeField] private GameObject pausedText;

    [SerializeField] private GameObject Player = null;

    private UIManager UI = null;


    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                pausedText.SetActive(true);
                Time.timeScale = 0; // pause the game, percentage
            }
            else
            {
                paused = false;
                pausedText.SetActive(false);
                Time.timeScale = 1;
            }
        }

        // press space to begin
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            // put in the player 
            Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);

            // start the game
            gameOver = false;

            // turn off title screen
            UI.HideTitleScreen();
        }
    }


}
