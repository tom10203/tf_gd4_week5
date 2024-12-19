using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI endGameScore;
    int lives = 3;
    int score = 0;

    public GameObject island;
    public GameObject spawnManager;
    public GameObject startScreenGOs;
    public GameObject InGameUI;
    public GameObject startScreenUI;
    public GameObject endGameUI;

     public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        endGameScore.text = score.ToString();
    }

    public void ReduceLives()
    {
        lives--;
        livesText.text = lives.ToString();
        if (lives == 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        island.SetActive(true);
        spawnManager.SetActive(true);
        InGameUI.SetActive(true);
        startScreenGOs.SetActive(false);
        startScreenUI.SetActive(false);
    }

    public void EndGame()
    {
        endGameUI.SetActive(true);
        island.SetActive(false);
        spawnManager.SetActive(false);
        InGameUI.SetActive(false);

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
