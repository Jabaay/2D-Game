using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for Image object

public class UIManager : MonoBehaviour
{

    [SerializeField] private Sprite[] lifeImages;
    [SerializeField] private Image livesImageDisplay;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject titleScreen = null; // need GO for .SetActive() method, polymorphism
    private int score = 0;
    private int highScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Score", 0);
        highScoreText.text = "High " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lifeImages[currentLives];
    }


    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score; // concatenating

        if (score > highScore)
        {
            highScore= score; // udpate high score
            highScoreText.text = "High " + highScore;

            PlayerPrefs.SetInt("Score", highScore);
        }

    }


   public void HideTitleScreen()
    {
        score = 0;
        scoreText.text = "Score " + score; // update UI

        titleScreen.SetActive(false); // turn off title
    }


    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true); // turn on title
    }


}
