using UnityEngine;

public class PlayerCard
{
    const string CARD_PREFAB_RAW_PATH = "Assets/Prefabs/Pages/CardPage/CardItem.prefab";

    public GameObject cardObject; // 场景内的实体
    public CardData cardData; // 卡牌的基础信息
    public TestDeckCardItem cardItem; // 卡牌的逻辑代码
    long cardInstanceId { get; set; }

    public PlayerCard(long cardId, int baseId) {
        cardInstanceId = cardId;

        GameObject cardPrefab = ResManager.getInstance().LoadPrefab(CARD_PREFAB_RAW_PATH);
        if (cardPrefab != null)
        {
            cardObject = GameObject.Instantiate(cardPrefab);
            cardObject.name = "Card" + cardId;
            cardItem = cardObject.AddComponent<TestDeckCardItem>();
            cardData = CardDataHelper.getInstance().getCardById(baseId);
            cardItem.SetCardInfo(this);
        }
    }

    public void Dispose() {
        cardData = null;
        cardItem = null;
        GameObject.Destroy(cardObject);
    }

    public void Use() {
        // 使用卡牌 触发卡牌的效果 
        ISkill skill = new NormalAttackSkill();
        skill.PreCheck();
        skill.MakeEffect();
        skill.PostEffect();
    }
}