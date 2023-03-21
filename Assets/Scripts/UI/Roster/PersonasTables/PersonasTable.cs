using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PersonasTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI programming;
    [SerializeField] private TextMeshProUGUI gameDesign;
    [SerializeField] private TextMeshProUGUI artDesign;
    [SerializeField] private TextMeshProUGUI soundDesign;
    [SerializeField] private TextMeshProUGUI screenwriting;

    public abstract void UpdateInfo(List<Persona> personas);

    protected void SetInfo(int[] values)
    {
        programming.text = values[0].ToString();
        gameDesign.text = values[1].ToString();
        artDesign.text = values[2].ToString();
        soundDesign.text = values[3].ToString();
        screenwriting.text = values[4].ToString();
    }
}