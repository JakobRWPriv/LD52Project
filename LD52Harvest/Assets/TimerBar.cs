using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public GameController gameController;
    public RectTransform timerBar;
    float remainingTime = 60f;

    void Update() {
        if (!gameController.gameHasStarted) return;

        remainingTime -= Time.deltaTime;
        UpdateBarFill();

        if (remainingTime < 0) {
            gameController.GameOver(1);
        }
    }

    public void AddTime(float timeToAdd) {
        remainingTime += timeToAdd;
        if (remainingTime > 60f) {
            remainingTime = 60;
        }
    }

    public void UpdateBarFill() {
        timerBar.sizeDelta = new Vector2(remainingTime * (867f / 60f), timerBar.sizeDelta.y);
    }
}
