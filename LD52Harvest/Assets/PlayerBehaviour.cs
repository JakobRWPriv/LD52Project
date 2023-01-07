using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator animator;

    float speed;
    float speedToSet;
    float speedSmoothing;

    float myRotation;
    float rotationToSet;
    float rotationSmoothing;

    public float rotationTimer;
    
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            speed = 6f;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            speed = 0f;
        }
        if (!Input.GetKey(KeyCode.UpArrow)) {
            rotationTimer = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            animator.SetInteger("TurnDir", 1);
            rotationTimer += Time.deltaTime;
            
            myRotation -= 0.8f;
            if (rotationTimer > 0.3f) {
                myRotation -= 0.3f;
            }
            if (rotationTimer > 0.6f) {
                myRotation -= 0.3f;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
            animator.SetInteger("TurnDir", 2);
            rotationTimer += Time.deltaTime;
            
            myRotation += 0.8f;
            if (rotationTimer > 0.3f) {
                myRotation += 0.3f;
            }
            if (rotationTimer > 0.6f) {
                myRotation += 0.3f;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) {
            if (!Input.GetKey(KeyCode.UpArrow)) {
                animator.SetInteger("TurnDir", 0);
            } else {
                animator.SetInteger("TurnDir", 3);
            }
            rotationTimer = 0;
        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            if (!Input.GetKey(KeyCode.UpArrow)) {
                animator.SetInteger("TurnDir", 0);
            } else {
                animator.SetInteger("TurnDir", 3);
            }
            rotationTimer = 0;
        }

        speedToSet = Mathf.SmoothDamp(speedToSet, speed, ref speedSmoothing, 0.2f);
        rb2d.velocity = transform.up * speedToSet;

        rotationToSet = Mathf.SmoothDamp(rotationToSet, myRotation, ref rotationSmoothing, 0.1f);
        transform.eulerAngles = new Vector3(0, 0, rotationToSet);
    }

    void FixedUpdate() {
        
    }
}
