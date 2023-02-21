using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    private Vector3 startPos;

    private Vector3 endPos;

    private float step = 0.03f;

    private float progress;

    public void Show(Vector3 pos, Vector3 endPos)
    {
        startPos = new Vector3 (pos.x, pos.y + 1.5f, pos.z);
        transform.position = startPos;

        this.endPos = endPos;
    }


    private void FixedUpdate()
    {
        progress += step;
        transform.position = Vector3.Lerp(startPos, endPos, progress);

        if (progress >= 1) Destroy(gameObject); 
    }
}