using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestBattleDeckView : MonoBehaviour
{
    public Vector3 cardGeneratePosition = new Vector3(600f, 0f, 0f);
    public float CARD_OUTSIDE_X = -145f;

    //public GameObject cardItemPrefab;
    public Transform deckParent;

    List<PlayerCard> cardInRoll = new List<PlayerCard>();
    float rollSpd = -40f;
    float putInRollDuration = 0.2f;
    float resortDuration = 0.1f;

    bool bDeckSetupEnd = false; // 初始化卡牌的标记

    float GENERATE_CARD_INTERVAL = 1.0f;
    float currTimetick = 0.0f;

    public void Awake()
    {
    }

    public void Start()
    {
        // 此处应初始化场地，卡组等内容
        BattleInfoManager.getInstance().InitBattle(this);
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartInitDeck();
        }

        if (bDeckSetupEnd) {
            UpdateCardPosition();

            if (CheckFirstCardOutside()) {
                RemoveCard(0, false);
            }

            if (CheckNeedGenerateNewCard()) {
                float targetPositionX = 0; // 如果一张卡都没了 那么到第一张位置0
                if (cardInRoll.Count > 0)
                {
                    GameObject firstCard = cardInRoll[0].cardObject;
                    targetPositionX = firstCard.transform.localPosition.x + putInRollDuration * rollSpd + 100f * cardInRoll.Count; // 0.2秒后的位置
                }

                PlayerCard newCard = generateNewCard(false);
                AddCard(newCard);
                newCard.cardItem.DoMove(targetPositionX, putInRollDuration, ()=> {
                    newCard.cardItem.InitMoveEnd = true;
                });
            }
        }
    }

    public void StartInitDeck() {
        StartCoroutine(InitDeck());
    }

    IEnumerator InitDeck() {
        for (int i = 0; i < 6; i++)
        {
            PlayerCard newCard = generateNewCard();
            float duration = 0.5f - i * 0.05f;
            if (duration < 0.2f)
                duration = 0.2f;
            newCard.cardObject.transform.DOLocalMoveX(i*100f, duration, false).SetEase(Ease.OutQuad).OnComplete(()=>{
                newCard.cardItem.InitMoveEnd = true;
            });
            yield return new WaitForSeconds(duration);
        }
        bDeckSetupEnd = true;
        yield return null;
    }

    PlayerCard generateNewCard(bool putInRoll = true) {
        PlayerCard card =  BattleInfoManager.getInstance().playerDeckManager.DrawNextCard();

        card.cardObject.transform.SetParent(deckParent);
        card.cardObject.transform.localPosition = cardGeneratePosition;
        if (putInRoll)
        {
            cardInRoll.Add(card);
        }
        return card;
    }

    void UpdateCardPosition() {
        for (int i = 0; i < cardInRoll.Count; i++) {
            if (cardInRoll[i].cardItem.InitMoveEnd)
            {
                cardInRoll[i].cardObject.transform.Translate(rollSpd * Time.deltaTime, 0f, 0f, Space.Self);
            }
        }
    }

    bool CheckFirstCardOutside() {
        if (cardInRoll.Count < 0) {
            return false;
        }
        if (cardInRoll[0].cardObject.transform.localPosition.x <= CARD_OUTSIDE_X) {
            return true;
        }
        return false;
    }

    public void AddCard(PlayerCard newCard) {
        cardInRoll.Add(newCard);
    }

    void RemoveCard(int index, bool isUsed) {
        PlayerCard oldCard = cardInRoll[index];
        cardInRoll.RemoveAt(index);
        if (isUsed) {
            BattleInfoManager.getInstance().playerDeckManager.UseCard(oldCard);
        }
        else
        {
            BattleInfoManager.getInstance().playerDeckManager.RecycleCard(oldCard);
        }
        ResetOldSiblingIndex(index);
    }

    bool CheckNeedGenerateNewCard() {
        currTimetick += Time.deltaTime;
        if (currTimetick >= GENERATE_CARD_INTERVAL)
        {
            currTimetick -= GENERATE_CARD_INTERVAL;
            return cardInRoll.Count < 6; //卡槽总数小于6 并且没有新卡牌在入队列
        }
        else {
            return false;
        }
    }

    void ResetOldSiblingIndex(int fromindex) {
        // 有卡删除 所有在删除卡之后的序列都要减一
        for (int i = 0; i < cardInRoll.Count; i++) {
            cardInRoll[i].cardItem.updateSibling(fromindex);
        }
    }

    public void useCard(PlayerCard card) {
        card.Use();
        int cardIndex = cardInRoll.IndexOf(card);
        RemoveCard(cardIndex, true);
        ResortCard(cardIndex);
    }

    void ResortCard(int fromIndex) {
        if (cardInRoll.Count < 0)
            return;
        GameObject firstCard = cardInRoll[0].cardObject;
        float firstCardX = firstCard.transform.localPosition.x;
        for (int i = fromIndex; i < cardInRoll.Count; i++) {
            PlayerCard card = cardInRoll[i];
            //card.transform.DOLocalMoveX(firstCardX+ resortDuration * rollSpd+i*100f, resortDuration, false).OnComplete(() => {
            //    card.GetComponent<TestDeckCardItem>().bResorting = false;
            //});
            card.cardItem.DoMove(firstCardX + resortDuration * rollSpd + i * 100f, resortDuration, null);
        }
    }
}
