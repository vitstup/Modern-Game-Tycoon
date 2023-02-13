using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ThemePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image Bg;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI themeName;

    private ThemeInfo theme;

    private Vector3 scale = new Vector3(1.15f, 1.15f);

    private void Awake() => Bg = GetComponent<Image>();

    public void SetTheme(ThemeInfo theme)
    {
        icon.sprite = theme.sprite;
        themeName.text = Localization.Localize(theme.localizationKey);
        this.theme = theme;
    }

    public void Selected()
    {
        Bg.color = Color.black;
        icon.color = Color.white;
        themeName.color = Color.white;
        icon.transform.localScale = scale;
    }

    public void UnSelected()
    {
        Bg.color = Color.white;
        icon.color = Color.black;
        themeName.color = Color.black;
        icon.transform.localScale = Vector3.one;
    }

    public void Select()
    {
        UnSelected();
        // do something
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Selected();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnSelected();
    }
}