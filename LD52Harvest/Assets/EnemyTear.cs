using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTear : MonoBehaviour
{
    public Transform target;
    
    public void GetShot(Transform tar) {
        target = tar;
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 3f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Ground") {
            Destroy(gameObject);
        }
    }
}
