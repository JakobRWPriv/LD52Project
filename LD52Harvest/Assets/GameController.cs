using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public bool gameHasStarted;
    public bool gameOver;
    public int points;
    public GameObject pointParticles;
    public Color[] possiblePlantColors;
    public FingerHeightController[] plants;
    public TimerBar timerBar;
    public float gameTime;
    public int numberOfActiveEyes = 0;
    public Animator wheelbarrowAnimator;

    public TextMeshProUGUI loseCondition;
    public TextMeshProUGUI secondsSurvived;

    public TextMeshProUGUI highScoreText;

    public GameObject introScreen;
    public GameObject gameOverScreen;
    public GameObject newHighscoreObj;

    public bool canRestart;

    void Start() {
        highScoreText.text = PlayerPrefs.GetInt("HIGHSCORE", 0) + " seconds";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Y)) {
            if (!gameHasStarted && !gameOver) {
                gameHasStarted = true;
                introScreen.SetActive(false);
            }

            if (canRestart) {
                RestartGame();
            }
        }

        if (!gameHasStarted) return;

        gameTime += Time.deltaTime;
    }

    public void AddPoint() {
        wheelbarrowAnimator.SetTrigger("Bounce");
        points++;
        pointParticles.SetActive(true);
        timerBar.AddTime(5f);
    }

    public void GameOver(int gameOverType) {
        if ((int)gameTime > PlayerPrefs.GetInt("HIGHSCORE", 0)) {
            PlayerPrefs.SetInt("HIGHSCORE", (int)gameTime);
            newHighscoreObj.SetActive(true);
        }

        StartCoroutine(RestartCooldown());

        gameOver = true;
        gameHasStarted = false;
        gameOverScreen.SetActive(true);
        if (gameOverType == 1) {
            loseCondition.text = "Your owner lost patience with your slow work. No treats for you tonight. All is lost.";
        }
        secondsSurvived.text = (int)gameTime + " seconds";
    }

    void RestartGame() {
        SceneManager.LoadScene(0);
    }

    IEnumerator RestartCooldown() {
        yield return new WaitForSeconds(0.5f);
        canRestart = true;
    }
}
