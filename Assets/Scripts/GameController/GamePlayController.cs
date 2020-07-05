using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField]
    private GameObject bird;

    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;

    [SerializeField]
    private Button restartGameButton, instructionsButton;

    [SerializeField]
    private GameObject pausePanel;

    void MakeInstance() {
        if (instance == null) {
            instance = this;
        }
    }

    void Awake() {
        MakeInstance();
        Time.timeScale = 0f;
    }

    public void PauseGame() {
        if (BirdScript.instance != null) {
            if (BirdScript.instance.isAlive) {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                gameOverText.text = "Pause";
                endScore.text = "" + BirdScript.instance.score;

                bestScore.text = "" + GameController.instance.GetHighScore();
               
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }

    public void PlayGame() {
        scoreText.gameObject.SetActive(true);
        bird.SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenuButton() {
        SceneFader.instance.FadeIn("MainMenu");
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        SceneFader.instance.FadeIn("GamePlay");
    }

    public void SetScore(int score) {
        scoreText.text = "Score: " + score;
    }

    public void PlayerDiedShowScore(int score) {
        pausePanel.SetActive(true);
        gameOverText.text = "Game Over";
        scoreText.gameObject.SetActive(false);

        endScore.text = "" + score;

        if (score > GameController.instance.GetHighScore()) {
            GameController.instance.SetHighScore(score);
        }

        bestScore.text = "" + GameController.instance.GetHighScore();

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());

    }
}
