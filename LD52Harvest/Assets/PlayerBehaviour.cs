using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameController gameController;
    public Rigidbody2D rb2d;
    public Animator animator;
    public BoxCollider2D grabCollider;
    public ThrownFingers thrownFingers;

    float speed;
    float speedToSet;
    float speedSmoothing;

    public float myRotationSpeed;
    float myRotation;
    float rotationToSet;
    float rotationSmoothing;

    public float rotationTimer;

    public bool isDashing;
    bool canDash = true;

    public bool isHolding;
    bool canGrab = true;
    public SpriteRenderer[] normalHandSprites;
    public SpriteRenderer[] holdingHandSprites;
    public SpriteRenderer[] fingerSprites;
    public SpriteRenderer[] throwHandSprites;
    public Color heldFingersColor;
    
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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Y)) {
            if (!isHolding && canGrab) {
                StartCoroutine(Grab());
            }
            if (isHolding) {
                StartCoroutine(Throw());
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && canDash && Input.GetKey(KeyCode.UpArrow)) {
            canDash = false;
            isDashing = true;
            StartCoroutine(Dash());
            speed = 40f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            animator.SetInteger("TurnDir", 1);
            rotationTimer += Time.deltaTime;
            
            myRotation -= myRotationSpeed * Time.deltaTime;
            if (!isDashing) {
                if (rotationTimer > 0.3f) {
                    myRotation -= (myRotationSpeed * 0.6f) * Time.deltaTime;
                }
                if (rotationTimer > 0.6f) {
                    myRotation -= (myRotationSpeed * 0.6f) * Time.deltaTime;
                }
            } else {
                myRotation -= (myRotationSpeed * 4) * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
            animator.SetInteger("TurnDir", 2);
            rotationTimer += Time.deltaTime;
            
            myRotation += myRotationSpeed * Time.deltaTime;
            if (!isDashing) {
                if (rotationTimer > 0.3f) {
                    myRotation += (myRotationSpeed * 0.6f) * Time.deltaTime;
                }
                if (rotationTimer > 0.6f) {
                    myRotation += (myRotationSpeed * 0.6f) * Time.deltaTime;
                }
            } else {
                myRotation += (myRotationSpeed * 4) * Time.deltaTime;
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

    IEnumerator Grab() {
        canGrab = false;
        animator.SetTrigger("Grab");
        foreach(SpriteRenderer sr in normalHandSprites) {
            sr.enabled = false;
        }
        foreach(SpriteRenderer sr in holdingHandSprites) {
            sr.enabled = true;
        }
        grabCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        grabCollider.enabled = false;

        
        yield return new WaitForSeconds(0.3f);

        if (!isHolding) {
            foreach(SpriteRenderer sr in normalHandSprites) {
                sr.enabled = true;
            }
            foreach(SpriteRenderer sr in holdingHandSprites) {
                sr.enabled = false;
            }
        }
        yield return new WaitForSeconds(0.05f);

        if (!isHolding)
            canGrab = true;
    }

    IEnumerator Throw() {
        animator.SetTrigger("Throw");
        foreach(SpriteRenderer sr in holdingHandSprites) {
            sr.enabled = false;
        }
        foreach(SpriteRenderer sr in throwHandSprites) {
            sr.enabled = true;
        }
        isHolding = false;
        foreach(SpriteRenderer sr in fingerSprites) {
            sr.enabled = false;
        }

        ThrownFingers fingers = Instantiate(thrownFingers, transform.position, Quaternion.identity);
        fingers.GetThrown(transform.eulerAngles.z, gameController, heldFingersColor);
        fingers.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        foreach(SpriteRenderer sr in throwHandSprites) {
            sr.enabled = false;
        }
        foreach(SpriteRenderer sr in normalHandSprites) {
            sr.enabled = true;
        }

        yield return new WaitForSeconds(0.05f);
        canGrab = true;
    }

    public void SuccessfulGrab(Color color) {
        isHolding = true;
        heldFingersColor = color;
        foreach(SpriteRenderer sr in fingerSprites) {
            sr.color = heldFingersColor;
            sr.enabled = true;
        }
    }

    void FixedUpdate() {
        rb2d.velocity = transform.up * speedToSet;
    }

    IEnumerator PutInWheelbarrow() {
        animator.SetTrigger("Throw");
        foreach(SpriteRenderer sr in holdingHandSprites) {
            sr.enabled = false;
        }
        foreach(SpriteRenderer sr in throwHandSprites) {
            sr.enabled = true;
        }
        isHolding = false;
        foreach(SpriteRenderer sr in fingerSprites) {
            sr.enabled = false;
        }

        yield return new WaitForSeconds(0.3f);

        foreach(SpriteRenderer sr in throwHandSprites) {
            sr.enabled = false;
        }
        foreach(SpriteRenderer sr in normalHandSprites) {
            sr.enabled = true;
        }

        yield return new WaitForSeconds(0.05f);
        canGrab = true;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Wheelbarrow" && isHolding) {
            gameController.AddPoint();
            StartCoroutine(PutInWheelbarrow());
        }
    }
}
