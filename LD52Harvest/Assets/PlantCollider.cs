using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollider : MonoBehaviour
{
    public FingerHeightController plant;
    public BoxCollider2D boxCollider;
    
    void OnTriggerStay2D(Collider2D othercollider) {
        if (othercollider.tag == "PlayerGrab") {
            boxCollider.enabled = false;
            plant.Grabbed();
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
    }
}
