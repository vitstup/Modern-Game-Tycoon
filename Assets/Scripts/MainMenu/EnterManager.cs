using System.Collections.Generic;
using UnityEngine;

public class EnterManager : MonoBehaviour
{
    [SerializeField] private List<EscScript.EscValue> values;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Check();
    }

    private void Check()
    {
        for (int i = values.Count - 1; i >= 0; i--)
        {
            if (values[i].panel.activeSelf)
            {
                values[i].button.onClick.Invoke();
                break;
            }
        }
    }
}
