using System;
using System.Collections.Generic;

public class BattleInfoManager:Singleton<BattleInfoManager>
{
    private TestBattleDeckView _battleView;
    public TestBattleDeckView BattleView { get { return _battleView; } }

    // EnemyTeam

    // PlayerTeam
    public PlayerDeckManager playerDeckManager = new PlayerDeckManager();

    // BattleStatus

    public void InitBattle(TestBattleDeckView view) {
        _battleView = view;

        // 初始化玩家卡组 待填充
        playerDeckManager.Init();
    }

    public void Update() {

    }
}
