using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;

    [SerializeField] private Image PauseBtn;
    [SerializeField] private Image X1Btn;
    [SerializeField] private Image X2Btn;

    [SerializeField] private Sprite PauseActive;
    [SerializeField] private Sprite PauseInactive;
    [SerializeField] private Sprite PlayActive;
    [SerializeField] private Sprite PlayInactive;

    [SerializeField] private GameObject BackPanel;

    // Update is called once per frame
    private void Update()
    {
        
    }
}