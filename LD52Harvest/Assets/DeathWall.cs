using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    public GameController gameController;

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Plant") {
            gameController.GameOver(0);
        }
    }
}
