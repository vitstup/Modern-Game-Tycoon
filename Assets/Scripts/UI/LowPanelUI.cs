using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LowPanelUI : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler //, IPointerClickHandler
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData) => mouseInside();
    public void OnPointerExit(PointerEventData eventData) => mouseOutside();
    //public void OnPointerClick(PointerEventData eventData) => mouseOutside();

    private void mouseInside()
    {
        text.fontSize = 26;
        text.fontStyle = FontStyles.Bold;
    }

    private void mouseOutside()
    {
        text.fontSize = 20;
        text.fontStyle = FontStyles.Normal;
    }
}