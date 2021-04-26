using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour, IGame
{
    ResManager resManager = null;
    UIManager uiManager = null;
    LevelManager levelManager = null;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        OnStartLoad();
    }

    void FixedUpdate()
    {

    }

    void Update()
    {

    }

    public void OnStartLoad()
    {
        resManager = ResManager.getInstance();
        uiManager = UIManager.getInstance();
        levelManager = LevelManager.getInstance();
    }

    public void OnLoad()
    {
    }

    public void OnEndLoad()
    {
        uiManager.ShowPage("MainPage");
    }

}
