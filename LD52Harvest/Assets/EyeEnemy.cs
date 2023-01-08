using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public GameController gameController;
    public FingerHeightController plantTarget;
    public EnemyTear enemyTear;
    public bool isActive;
    public bool isSecondaryEye;

    float shootTimer = 3f;

    float myYPos;
    float yPosToSet = 20f;
    float yPosSmoothing;

    float activateTimer;

    void Start() {
        myYPos = Random.Range(4f, 12f);

        activateTimer = Random.Range(5f, 10f);
    }

    public void Activate() {
        isActive = true;
        gameController.numberOfActiveEyes++;
    }

    void Update() {
        activateTimer -= Time.deltaTime;

        if (activateTimer < 0) {
            activateTimer = Random.Range(5f, 10f);

            int chanceToAppearMax = (12 * 2) + (gameController.numberOfActiveEyes * 2);

            if (gameController.gameTime > 10f) {
                chanceToAppearMax = 11;
            }
            if (gameController.gameTime > 20f) {
                chanceToAppearMax = 10;
            }
            if (gameController.gameTime > 30f) {
                chanceToAppearMax = 9;
            }
            if (gameController.gameTime > 40f) {
                chanceToAppearMax = 8;
            }
            if (gameController.gameTime > 50f) {
                chanceToAppearMax = 7;
            }
            if (gameController.gameTime > 60f) {
                chanceToAppearMax = 6;
            }
            if (gameController.gameTime > 70f) {
                chanceToAppearMax = 5;
            }
            if (gameController.gameTime > 80f) {
                chanceToAppearMax = 4;
            }
            if (gameController.gameTime > 90f) {
                chanceToAppearMax = 3;
            }
            if (gameController.gameTime > 100f) {
                chanceToAppearMax = 2;
            }

            if (isSecondaryEye ) {
                chanceToAppearMax = chanceToAppearMax * 2;
            }

            int ran = Random.Range(0, chanceToAppearMax);
            if (ran == 0 && !isActive) {
                Activate();
            }
        }

        if (!isActive) return;

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

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (!isActive) return;

        if (otherCollider.tag == "ThrownFingers") {
            isActive = false;
            Destroy(otherCollider.gameObject);
            transform.position = new Vector3(transform.position.x, 20f, 0);
            shootTimer = 0;
            yPosToSet = 20f;
            gameController.numberOfActiveEyes--;
        }
    }
}
