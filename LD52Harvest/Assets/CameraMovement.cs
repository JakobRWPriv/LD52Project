using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerBehaviour player;
    float posX;
    Vector2 velocity;
    
    void FixedUpdate() {
        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, 0.3f);

        transform.position = new Vector3(posX, 0, -1);
    }
}
