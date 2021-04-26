using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardDataList {
    public List<CardData> data;
}

public class CardData
{
    public int Id;
    public string Name;
    public string Desc;
    public string Img;
}

public class CardDataHelper {
    public static CardDataHelper _instance;
    private CardDataHelper() {
        using (StreamReader cardDataSR = new StreamReader(Application.dataPath + "/ExcelData/card_data.json"))
        {
            string data = cardDataSR.ReadToEnd();
            cardDataSR.Close();

            data = "{\"data\":" + data + "}";

            CardDataList json = JsonMapper.ToObject<CardDataList>(data);
            // 预处理数据
            for (int i = 0; i < json.data.Count; i++) {
                if (cardDataDict.ContainsKey(json.data[i].Id)){
                    cardDataDict[json.data[i].Id] = json.data[i];
                }
                else {
                    cardDataDict.Add(json.data[i].Id, json.data[i]);
                }
            }
        }
    }

    public static CardDataHelper getInstance() {
        if (_instance == null)
        {
            _instance = new CardDataHelper();
        }
        return _instance;
    }

    private Dictionary<int, CardData> cardDataDict = new Dictionary<int, CardData>();

    public CardData getCardById(int id) {
        return cardDataDict.ContainsKey(id) ? cardDataDict[id] : null;
    }
}
