using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotationSpeed;
    void Start() {
        rotationSpeed = Random.Range(600f, 800f);
        int ran = Random.Range(0, 2);
        if (ran == 1) {
            rotationSpeed = -rotationSpeed;
        }
    }

    void Update() {
        transform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime);
    }
}
