using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{
    public PlayerBehaviour player;
    public Animator animator;
    public Rigidbody2D rb2d;

    float speed;
    float speedToSet;
    float speedSmoothing;
    
    void Update() {

        if (player.transform.position.x > transform.position.x && transform.localScale.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (player.transform.position.x < transform.position.x && transform.localScale.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (player.transform.position.x > transform.position.x + 12f) {
            speed = 5f;
            animator.SetBool("IsMoving", true);
        } else if (player.transform.position.x < transform.position.x - 12f) {
            speed = -5f;
            animator.SetBool("IsMoving", true);
        } else {
            speed = 0;
            animator.SetBool("IsMoving", false);
        }

        speedToSet = Mathf.SmoothDamp(speedToSet, speed, ref speedSmoothing, 0.2f);
    }

    void FixedUpdate() {
        rb2d.velocity = transform.right * speedToSet;
    }
}
