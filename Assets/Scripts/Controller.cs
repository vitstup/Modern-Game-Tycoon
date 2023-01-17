using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isOverUi()
    {
        var _eventDataCurPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurPos, _results);
        return _results.Count > 0;
    }
}
