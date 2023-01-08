using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public FingerHeightController plantTarget;
    public EnemyTear enemyTear;

    float shootTimer = 3f;

    float myYPos;
    float yPosToSet = 20f;
    float yPosSmoothing;

    void Start() {
        myYPos = Random.Range(4f, 12f);
    }

    void Update() {
        yPosToSet = Mathf.SmoothDamp(yPosToSet, myYPos, ref yPosSmoothing, 0.5f);
        transform.position = new Vector3(transform.position.x, yPosToSet, 0);

        shootTimer -= Time.deltaTime;
        if (shootTimer < 0) {
            EnemyTear tear = Instantiate(enemyTear, transform.position + new Vector3(-0.38f, -0.253f, 0), Quaternion.identity);
            tear.GetShot(plantTarget);
            tear.gameObject.SetActive(true);
            shootTimer = 3f;
        }
    }
}