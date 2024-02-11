using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject failToBeatRecord;
    [SerializeField] private GameObject beatRecord;

    public PlayerScript ps;

    public int highScore;


    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }


    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }


    void Update()
    {
        if (ps.healthBar.value == 0)
        {
            deathScreen.SetActive(true);

            // Check if the current score beats the high score
            if (highScore != 0)
            {
                if (ps.score > highScore)
                {
                    highScore = getPlayerScore(ps.killCount);
                    PlayerPrefs.SetInt("HighScore", getPlayerScore(ps.killCount));
                    beatRecord.SetActive(true);
                    failToBeatRecord.SetActive(false);
                }
            }
            else
            {
                beatRecord.SetActive(false);
                failToBeatRecord.SetActive(true);
            }
        }
        else
        {
            deathScreen.SetActive(false);
            beatRecord.SetActive(false);
            failToBeatRecord.SetActive(false);
        }
    }


    private int getPlayerScore(int kc)
    {
        kc = ps.killCount;
        return kc * 5;
    }

}
