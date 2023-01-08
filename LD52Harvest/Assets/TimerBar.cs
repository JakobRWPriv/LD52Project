using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public RectTransform timerBar;
    float remainingTime = 60f;

    void Update() {
        remainingTime -= Time.deltaTime;
        UpdateBarFill();

        if (remainingTime < 0) {
            //print("TIME OUT");
        }
    }

    public void AddTime(float timeToAdd) {
        remainingTime += timeToAdd;
        if (remainingTime > 60f) {
            remainingTime = 60;
        }
    }

    public void UpdateBarFill() {
        timerBar.sizeDelta = new Vector2(remainingTime * (1000f / 60f), timerBar.sizeDelta.y);
    }
}
