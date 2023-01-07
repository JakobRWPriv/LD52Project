using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownFingers : MonoBehaviour
{
    public GameController gameController;
    public Rigidbody2D rb2d;
    
    public void GetThrown(float rot, GameController gc) {
        transform.eulerAngles = new Vector3(0, 0, rot);
        gameController = gc;
    }

    void FixedUpdate() {
        rb2d.velocity = transform.up * 25f;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Wheelbarrow") {
            gameController.AddPoint();
            Destroy(gameObject);
        }
    }
}
