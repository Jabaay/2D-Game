using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    // 0 is mostKills, 1 is highScore, 2 is currentKills, 3 is currentScore;
    [SerializeField] private Text[] scoreBoard;

    public PlayerScript ps;
    public GameOver go;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")  != null)
        {
            scoreBoard[0].text = "Most Kills: " + go.highScore / 5;
            scoreBoard[1].text = "Highest Score: " + go.highScore;
            scoreBoard[2].text = "Current Kills: " + ps.killCount;
            scoreBoard[3].text = "Current Score: " + ps.killCount * 5;
        }
    }
}
