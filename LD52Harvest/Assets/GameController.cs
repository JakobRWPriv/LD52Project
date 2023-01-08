using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int points;
    public GameObject pointParticles;
    public Color[] possiblePlantColors;
    public FingerHeightController[] plants;
    public TimerBar timerBar;

    void Start() {
        
    }

    public void AddPoint() {
        points++;
        pointParticles.SetActive(true);
        timerBar.AddTime(5f);
    }
}
