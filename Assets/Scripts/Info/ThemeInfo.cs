using UnityEngine;

[CreateAssetMenu(fileName = "theme", menuName = "Theme")]
public class ThemeInfo : ScriptableObject
{
    [SerializeField] private Sprite _sprite;

    public Sprite sprite => _sprite;
}