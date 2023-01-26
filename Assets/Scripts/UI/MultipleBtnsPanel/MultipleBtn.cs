using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultipleBtn : MonoBehaviour
{
    private Image background;
    [SerializeField]private GameObject rightPanel;
    private TextMeshProUGUI text;

    private bool selected = false;

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        updateUI();
    }

    private void updateUI()
    {
        if(!selected)
        {
            background.color = Constans.GrayBgInactive;
            rightPanel.SetActive(false);
            text.color = Constans.WhiteTextInactive;
        }
        else
        {
            background.color = Constans.GrayBgActive;
            rightPanel.SetActive(true);
            text.color = Constans.WhiteTextActive;
        }
    }

    public void ChangeState(bool selected)
    {
        this.selected = selected;
        updateUI();
    }
}