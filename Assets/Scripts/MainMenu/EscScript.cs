using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscScript : MonoBehaviour
{
    [System.Serializable]
    private class EscValue
    {
        [field: SerializeField] public GameObject panel { get; private set; }
        [field: SerializeField] public Button button { get; private set; }

        public EscValue(GameObject panel, Button button)
        {
            this.panel = panel;
            this.button = button;
        }
    }

    [SerializeField] private List<EscValue> values;

    private void Start()
    {
        values.Add(new EscValue(SettingsScript.instance.settingsCanvas.gameObject, SettingsScript.instance.clsBtn)); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Check();
    }

    private void Check()
    {
        bool btnUsed = false;
        for (int i = values.Count - 1; i >= 0 ; i--)
        {
            if (values[i].panel.activeSelf)
            {
                values[i].button.onClick.Invoke();
                btnUsed = true;
                break;
            }
        }

        if (!btnUsed)
        {
            if (TimeManager.instance != null && TimeManager.instance.runStatus == RunStatus.standart)
            {
                GameMenuScript.instance.OpenMenu(true);
            }
        }
    }
}