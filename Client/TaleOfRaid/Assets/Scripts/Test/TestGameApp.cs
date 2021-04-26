using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameApp : MonoBehaviour
{
    public TestBattleDeckView battleDeckView;

    // 相对正式的管理
    BattleInfoManager battleMangaer = new BattleInfoManager();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        battleDeckView.DoUpdate();
        battleMangaer.Update();
    }
    
}
