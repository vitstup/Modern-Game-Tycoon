using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class ButtonSound : MonoBehaviour
{
    private void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    protected virtual void PlaySound()
    {
        AudioManager.instance.PlayBtn();
    }
}