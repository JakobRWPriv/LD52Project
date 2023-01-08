using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimationSounds : MonoBehaviour
{
    public void StepSound() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.FarmerStep, 1, Random.Range(0.9f, 1.1f));
    }
}
