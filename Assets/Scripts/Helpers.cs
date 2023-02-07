using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers 
{
    public static bool IsOverUi()
    {
        var _eventDataCurPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurPos, _results);
        return _results.Count > 0;
    }

    public static void DeleteChilds(Transform transform)
    {
        foreach (Transform child in transform) Object.Destroy(child.gameObject);
    }
}