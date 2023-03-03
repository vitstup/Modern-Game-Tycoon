using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class SignindSound : ButtonSound
{
    protected override void PlaySound()
    {
        AudioManager.instance.PlayBtn(true);
    }
}