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

    private Stage prototyping = new Stage(new PrototypingSliders());
    private Stage developing = new Stage(new DevelopingSliders());
    private Stage design = new Stage(new DesignSliders());

    public Stage currentStage;

    public int bugs;

    // reviews and user score

    public Sprite sprite;

}