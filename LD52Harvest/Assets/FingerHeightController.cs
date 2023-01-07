using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerHeightController : MonoBehaviour
{
    public PlayerBehaviour player;
    public SpriteRenderer[] srs;
    public BoxCollider2D boxCollider;
    public float growthSpeed;
    public Color plantColor;
    
    public float minimumGrowth;

    float alphaToSet;
    float myAlpha;
    float alphaSmoothing;

    void Start() {
        minimumGrowth = srs[0].size.y;
        plantColor = srs[0].color;
        growthSpeed = Random.Range(0.08f, 0.13f);
    }
    
    void Update() {
        if (srs[0].size.y < minimumGrowth + 1f) {
            alphaToSet = 255f;
        } else {
            alphaToSet = 130f;
        }
        alphaToSet = Mathf.SmoothDamp(alphaToSet, myAlpha, ref alphaSmoothing, 0.1f);

        foreach(SpriteRenderer sr in srs) {
            sr.size += new Vector2(0, growthSpeed * Time.deltaTime);

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alphaToSet);
        }

        boxCollider.size += new Vector2(0, growthSpeed * Time.deltaTime);
        boxCollider.gameObject.transform.position += new Vector3(0, (growthSpeed * 0.75f) * Time.deltaTime, 0);
    }

    public void Grabbed() {
        if (srs[0].size.y < minimumGrowth + 1f) return;

        if (srs[0].size.y < minimumGrowth + 2f) {
            foreach(SpriteRenderer sr in srs) {
                sr.size -= new Vector2(0, 1);
            }
            player.SuccessfulGrab(plantColor);
            boxCollider.size -= new Vector2(0, 1);
            boxCollider.gameObject.transform.position -= new Vector3(0, 1 * 0.75f, 0);
            return;
        }

        foreach(SpriteRenderer sr in srs) {
            sr.size -= new Vector2(0, 2);
        }
        player.SuccessfulGrab(plantColor);
        boxCollider.size -= new Vector2(0, 2);
        boxCollider.gameObject.transform.position -= new Vector3(0, 2 * 0.75f, 0);
    }
}
