using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBtnsPanel : MonoBehaviour
{
    private MultipleBtn[] Btns;

    private void Start()
    {
        Btns = GetComponentsInChildren<MultipleBtn>();
        ChangeState(Btns[0]);
    }

    public void ChangeState(MultipleBtn btn)
    {
        for (int i = 0; i < Btns.Length; i++)
        {
            if (Btns[i] == btn) Btns[i].ChangeState(true);
            else Btns[i].ChangeState(false);
        }
    }
}