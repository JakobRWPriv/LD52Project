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

    public bool isDashing;
    bool canDash = true;
    
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isDashing) {
            speed = 12f;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            speed = 0f;
        }
        if (!Input.GetKey(KeyCode.UpArrow)) {
            rotationTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.X) && canDash) {
            canDash = false;
            isDashing = true;
            StartCoroutine(Dash());
            speed = 40f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            animator.SetInteger("TurnDir", 1);
            rotationTimer += Time.deltaTime;
            
            myRotation -= 1.2f;
            if (!isDashing) {
                if (rotationTimer > 0.3f) {
                    myRotation -= 0.5f;
                }
                if (rotationTimer > 0.6f) {
                    myRotation -= 0.5f;
                }
            } else {
                myRotation -= 3f;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
            animator.SetInteger("TurnDir", 2);
            rotationTimer += Time.deltaTime;
            
            myRotation += 1.2f;
            if (!isDashing) {
                if (rotationTimer > 0.3f) {
                    myRotation += 0.5f;
                }
                if (rotationTimer > 0.6f) {
                    myRotation += 0.5f;
                }
            } else {
                myRotation += 3f;
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

        speedToSet = Mathf.SmoothDamp(speedToSet, speed, ref speedSmoothing, isDashing ? 0f : 0.2f);

        rotationToSet = Mathf.SmoothDamp(rotationToSet, myRotation, ref rotationSmoothing, 0.1f);
        transform.eulerAngles = new Vector3(0, 0, rotationToSet);
    }

    IEnumerator Dash() {
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        if (Input.GetKey(KeyCode.UpArrow)) {
            speed = 12f;
        } else {
            speed = 0f;
        }
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    void FixedUpdate() {
        rb2d.velocity = transform.up * speedToSet;
    }
}
