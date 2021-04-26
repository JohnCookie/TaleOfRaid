using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestDeckCardItem : MonoBehaviour
{
    // 卡牌在队列里的位置sibling编号（因为ugui是层级排序）
    int oldIndex = 0;

    bool canInteract = false; // 是否可交互 tween过程中不能交互
    public bool InitMoveEnd { get { return canInteract; } set { canInteract = value; } }

    // 自己做插值
    bool bTweening = false;
    float tweenTargetX = 99999f;
    float currTweenTime = 0f;
    float tweenDuration = 0.15f;
    List<UnityAction> tweenCallbacks = new List<UnityAction>();

    // 卡牌载体（但是这样耦合了 需要解耦）
    PlayerCard cardInfo;

    void Awake() {
        oldIndex = transform.GetSiblingIndex();
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener(onPointerEnter);
        trigger.triggers.Add(enterEntry);
        
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener(onPointerExit);
        trigger.triggers.Add(exitEntry);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener(onPointerClick);
        trigger.triggers.Add(clickEntry);
    }

    void Update()
    {
        if (bTweening) {
            currTweenTime += Time.deltaTime;
            if (currTweenTime > tweenDuration) {
                currTweenTime = tweenDuration;
            }
            float positionX = Mathf.Lerp(transform.localPosition.x, tweenTargetX, currTweenTime / tweenDuration);
            transform.localPosition = new Vector3(positionX, transform.localPosition.y, transform.localPosition.z);

            if (currTweenTime == tweenDuration) {
                bTweening = false;
                currTweenTime = 0;
                for (int i = 0; i < tweenCallbacks.Count; i++) {
                    tweenCallbacks[i]?.Invoke();
                }
                tweenCallbacks.Clear();
            }
        }
    }

    public void SetCardInfo(PlayerCard card) {
        this.cardInfo = card;
        transform.Find("DescBg/CardName").GetComponent<Text>().text = cardInfo.cardData.Name;
        transform.Find("DescBg/CardDescribe").GetComponent<Text>().text = cardInfo.cardData.Desc;
    }

    public void updateSibling(int changeIndex) {
        if (oldIndex > changeIndex)
        {
            oldIndex -= 1;
        }
    }

    public void DoMove(float targetX, float duration, UnityAction action)
    {
        //transform.DOLocalMoveX(targetX, duration, false).OnComplete(() => {
        //    battleScript.AddCard(this.gameObject);
        //    canInteract = true;
        //});
        if (!bTweening)
        {
            tweenTargetX = targetX;
            tweenDuration = duration;
            if (action != null)
            {
                tweenCallbacks.Add(action);
            }
            bTweening = true;
        }
        else {
            tweenTargetX = targetX;
            tweenDuration = tweenDuration - currTweenTime;
            if (action != null)
            {
                tweenCallbacks.Add(action);
            }
        }
    }

    void onPointerEnter(BaseEventData data) {
        if (canInteract)
        {
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            oldIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
        }
    }

    void onPointerExit(BaseEventData data)
    {
        if (canInteract)
        {
            transform.localScale = Vector3.one;
            transform.SetSiblingIndex(oldIndex);
        }
    }

    void onPointerClick(BaseEventData data) {
        if (canInteract)
        {
            BattleInfoManager.getInstance().BattleView.useCard(cardInfo);
        }
    }
}
