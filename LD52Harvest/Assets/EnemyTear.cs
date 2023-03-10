using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTear : MonoBehaviour
{
    public FingerHeightController plantTarget;
    public GameObject tearParticles;

    void OnEnable() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.Shoot, 0.3f, Random.Range(0.9f, 1.1f));
        Vector2 direction = (Vector2)plantTarget.transform.position - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * (Mathf.Rad2Deg);

        Quaternion dir = Quaternion.Euler(Vector3.forward * (angle));

        transform.eulerAngles = new Vector3(0, 0, dir.eulerAngles.z);
    }
    
    public void GetShot(FingerHeightController plant) {
        plantTarget = plant;
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, plantTarget.transform.position + new Vector3(0, -1f, 0), 20f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Ground") {
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.DropHit, 0.3f, Random.Range(0.9f, 1.1f));
            plantTarget.GetWatered();
            Instantiate(tearParticles, transform.position += new Vector3(0, -0.5f, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
