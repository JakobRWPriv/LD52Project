using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerHeightController : MonoBehaviour
{
    public GameController gameController;
    public PlayerBehaviour player;
    public SpriteRenderer mainSprite;
    public SpriteRenderer[] srs;
    public BoxCollider2D boxCollider;
    public float growthSpeed;
    public Color plantColor;
    
    public float minimumGrowth;

    float alphaToSet;
    float myAlpha;
    float alphaSmoothing;

    public float speedIncrease;

    void Start() {
        Color chosenColor = gameController.possiblePlantColors[0];
        foreach(SpriteRenderer sr in srs) {
            sr.color = chosenColor;
            mainSprite.color = chosenColor;
        }

        minimumGrowth = srs[0].size.y;
        plantColor = chosenColor;
        growthSpeed = 0.15f;

        foreach(SpriteRenderer sr in srs) {
            sr.size += new Vector2(0, 1f);
        }
        boxCollider.size += new Vector2(0, 1f);
        boxCollider.gameObject.transform.position += new Vector3(0, 1f * 0.75f, 0);
    }
    
    void Update() {
        speedIncrease += Time.deltaTime * 0.001f;

        if (srs[0].size.y < minimumGrowth + 1f) {
            myAlpha = 0.4f;
        } else {
            myAlpha = 1f;
        }
        alphaToSet = Mathf.SmoothDamp(alphaToSet, myAlpha, ref alphaSmoothing, 0.1f);

        foreach(SpriteRenderer sr in srs) {
            sr.size += new Vector2(0, (growthSpeed + speedIncrease) * Time.deltaTime);

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alphaToSet);
            mainSprite.color = new Color(sr.color.r, sr.color.g, sr.color.b, alphaToSet);
        }

        boxCollider.size += new Vector2(0, (growthSpeed + speedIncrease) * Time.deltaTime);
        boxCollider.gameObject.transform.position += new Vector3(0, ((growthSpeed + speedIncrease) * 0.75f) * Time.deltaTime, 0);
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

    public void GetWatered() {
        foreach(SpriteRenderer sr in srs) {
            sr.size += new Vector2(0, 0.4f);
        }
        boxCollider.size += new Vector2(0, 0.4f);
        boxCollider.gameObject.transform.position += new Vector3(0, 0.4f * 0.75f, 0);
    }
}
