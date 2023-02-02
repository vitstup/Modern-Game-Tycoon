using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Building
{
    private Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInChildren<Canvas>();
    }

    public override void Rotate(bool Right)
    {
        base.Rotate(Right);
        if (Right) canvas.transform.Rotate(0, -45, 0, Space.World);
        else canvas.transform.Rotate(0, 45, 0, Space.World);
    }

    protected override void Move()
    {
        base.Move();
        canvas.gameObject.SetActive(false);
    }

    public override void Put()
    {
        base.Put();
        canvas.gameObject.SetActive(true);
    }
}
