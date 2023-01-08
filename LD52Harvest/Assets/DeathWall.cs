using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Plant") {
            print("DEATH");
        }
    }
}
