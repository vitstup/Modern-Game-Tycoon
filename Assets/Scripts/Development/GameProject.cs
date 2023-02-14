using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameProject : Project
{
    public int size;
    public Engine engine;
    public GenreInfo genre;
    public ThemeInfo theme;
    public Platform[] platforms = new Platform[4];

    public Stage prototyping;
    public Stage developing;
    public Stage design;
    // current stage progress

    public int bugs;

    // reviews and user score

    public Sprite sprite;

}