using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckManager
{
    List<PlayerCard> m_playerCardList = new List<PlayerCard>();

    public void Init() {

    }

    // 抽下一张牌
    public PlayerCard DrawNextCard()
    {
        int baseId = UnityEngine.Random.Range(1, 6);
        PlayerCard card = PlayerCardFactory.getInstance().CreatePlayerCard(baseId);
        return card;
    }

    // 将卡牌放回牌库（走到头了没用掉）
    public void RecycleCard(PlayerCard card) {
        card.Dispose();
    }

    // 使用卡牌
    public void UseCard(PlayerCard card) {
        card.Dispose();
    }

    // 直接销毁卡牌
    public void DestroyCardDirectly(PlayerCard card) {

    }
}