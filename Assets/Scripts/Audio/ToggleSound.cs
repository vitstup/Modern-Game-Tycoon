using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[DisallowMultipleComponent]
public class ToggleSound : MonoBehaviour
{
    private void Awake()
    {
        var tog = GetComponent<Toggle>();
        tog.onValueChanged.AddListener(PlaySound);
    }

    protected virtual void PlaySound(bool active)
    {
        AudioManager.instance.PlayBtn();
    }
}