﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    public Button battleBtn;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterBattle() {
        UIManager.getInstance().ShowPage("BattlePage");
    }
    
}
