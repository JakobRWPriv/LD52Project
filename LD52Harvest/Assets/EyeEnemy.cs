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
    public GameObject deathParticles;

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
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.Activate, 0.5f, Random.Range(0.9f, 1.1f));
        isActive = true;
        gameController.numberOfActiveEyes++;
    }

    void Update() {
        activateTimer -= Time.deltaTime;

        if (activateTimer < 0) {
            activateTimer = Random.Range(10f, 20f);

            int chanceToAppearMax = (10000) + (gameController.numberOfActiveEyes * 3);

            if (gameController.gameTime > 30f) {
                chanceToAppearMax = 11 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 60f) {
                chanceToAppearMax = 10 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 90f) {
                chanceToAppearMax = 9 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 120f) {
                chanceToAppearMax = 8 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 150f) {
                chanceToAppearMax = 7 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 180f) {
                chanceToAppearMax = 6 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 210f) {
                chanceToAppearMax = 5 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 240f) {
                chanceToAppearMax = 4 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 270f) {
                chanceToAppearMax = 3 + (gameController.numberOfActiveEyes * 10);
            }
            if (gameController.gameTime > 300f) {
                chanceToAppearMax = 2 + (gameController.numberOfActiveEyes * 10);
            }

            if (isSecondaryEye ) {
                chanceToAppearMax = chanceToAppearMax * 2;
            }

            int ran = Random.Range(0, chanceToAppearMax);
            if (ran == 0 && !isActive) {
                Activate();
            } else {
                print("FAILED");
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
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.Die, 0.5f, Random.Range(0.9f, 1.1f));
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.Die2, 0.5f, Random.Range(0.9f, 1.1f));
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            isActive = false;
            Destroy(otherCollider.gameObject);
            transform.position = new Vector3(transform.position.x, 20f, 0);
            shootTimer = 0;
            yPosToSet = 20f;
            gameController.numberOfActiveEyes--;
        }
    }
}
