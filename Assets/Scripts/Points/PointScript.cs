using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private TextMeshPro text;

    private Vector3 startPos;

    private Vector3 endPos;

    private float step = 0.03f;

    private float progress;

    public void Show(Vector3 pos, Vector3 endPos, int sprite, float value)
    {
        gameObject.SetActive(true);
        startPos = new Vector3(pos.x, pos.y + 1.5f, pos.z);
        transform.position = startPos;

        this.endPos = endPos;

        spriteRenderer.sprite = sprites[sprite];

        text.text = System.Math.Round(value, 1).ToString();
    }


    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            progress += step;
            transform.position = Vector3.Lerp(startPos, endPos, progress);
        }
        if (progress >= 1) 
        { 
            PointsManager.instance.AddToPool(this); 
            progress = 0; 
            gameObject.SetActive(false); 
        }
    }
}