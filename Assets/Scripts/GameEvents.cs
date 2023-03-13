using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private bool warningInvoked;
    private bool loseInvoked;
    private bool winInvoked;

    private void Awake()
    {
        TimeManager.DayUpdateEvent.AddListener(Check);
    }

    private void Check()
    {
        if (!warningInvoked && Main.instance.money <= -100000) { WinLoseUI.instance.OpenCanvas(WinLoseUI.State.warning); warningInvoked = true; }
        if (!loseInvoked && Main.instance.money <= -250000) { WinLoseUI.instance.OpenCanvas(WinLoseUI.State.lose); loseInvoked = true; }
        if (!winInvoked && TimeManager.instance.year >= 2023 ) { WinLoseUI.instance.OpenCanvas(WinLoseUI.State.win); winInvoked = true; }
    }
}