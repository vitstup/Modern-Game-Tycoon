using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "genre", menuName = "Genre")]
public class GenreInfo : ScriptableObject
{
    [SerializeField] private ThemeInfo[] _favoriteThemes;
    [SerializeField] private ThemeInfo[] _unfavoriteThemes;

    // stage sliders
    // maybe bonuses from features

    public ThemeInfo[] favoriteThemes => _favoriteThemes;
    public ThemeInfo[] unfavoriteThemes => _unfavoriteThemes;
}