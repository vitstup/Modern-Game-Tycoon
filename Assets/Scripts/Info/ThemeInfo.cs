using UnityEngine;

[CreateAssetMenu(fileName = "theme", menuName = "Theme")]
public class ThemeInfo : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _localizationKey;

    public Sprite sprite => _sprite;
    public string localizationKey => _localizationKey;
}