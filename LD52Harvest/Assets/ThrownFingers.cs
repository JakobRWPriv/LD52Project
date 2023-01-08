using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownFingers : MonoBehaviour
{
    public GameController gameController;
    public Rigidbody2D rb2d;
    public SpriteRenderer[] srs;

    void Start() {
        StartCoroutine(DestroyAfterTime());
    }
    
    public void GetThrown(float rot, GameController gc, Color color) {
        transform.eulerAngles = new Vector3(0, 0, rot);
        gameController = gc;
        foreach(SpriteRenderer sr in srs) {
            sr.color = color;
        }
    }

    void FixedUpdate() {
        rb2d.velocity = transform.up * 25f;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Wheelbarrow") {
            gameController.AddPoint();
            Destroy(gameObject);
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.IntoWheelbarrow, 1, Random.Range(0.9f, 1.1f));
        }
        if (otherCollider.tag == "Ground") {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
