using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeWarning : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public EyeEnemy[] eyesToCheck;
    public bool checkRight;
    public GameObject warningObj;
    public bool activateWarning;

    void Update() {
        activateWarning = false;

        foreach(EyeEnemy eye in eyesToCheck) {
            if (checkRight) {
                if (eye.isActive && eye.transform.position.x > cameraMovement.transform.position.x + 27f) {
                    activateWarning = true;
                }
            } else {
                if (eye.isActive && eye.transform.position.x < cameraMovement.transform.position.x - 27f) {
                    activateWarning = true;
                }
            }
        }

        warningObj.SetActive(activateWarning);
    }
}
