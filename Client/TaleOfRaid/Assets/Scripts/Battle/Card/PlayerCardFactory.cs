using System;
using System.Collections.Generic;

public class PlayerCardFactory:Singleton<PlayerCardFactory>
{
    long currCardId = 0;

    public PlayerCard CreatePlayerCard(int baseCardId) {
        PlayerCard card = new PlayerCard(++currCardId, baseCardId);
        return card;
    }    
}
