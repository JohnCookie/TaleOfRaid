using System;
using System.Collections.Generic;
using UnityEngine;

class UIManager : Singleton<UIManager>
{
    const string PagePrefabFolder = "Assets/Prefabs/Pages/";
    int currPageIndex = 0;
    GameObject uiRoot = null;

    Dictionary<string, GameObject> pagePrefabDict = new Dictionary<string, GameObject>(); // 存储Page的Prefab 作为缓存
    Dictionary<string, PageInfo> pageDict = new Dictionary<string, PageInfo>(); // 存储Page的实体 是可以销毁的

    List<PageInfo> pageList = new List<PageInfo>();

    public UIManager() {
        UnityEngine.Debug.Log("UIManager Init");
        uiRoot = UnityEngine.GameObject.Find("Canvas/UIRoot");
    }

    public void ShowPage(string pageName) {
        GameObject pagePrefab = ResManager.getInstance().LoadPrefab(PagePrefabFolder + pageName + "/" + pageName + ".prefab");
        GameObject pageObj = GameObject.Instantiate<GameObject>(pagePrefab);

        PageInfo pageInfo = new PageInfo(pageName, pageObj, currPageIndex);
        currPageIndex++;

        pageObj.transform.SetParent(uiRoot.transform, false);
    }

    public void ShowLastPage() {

    }


}

public class PageInfo {
    public string pageName;
    public GameObject pageObj;
    public int pageIndex;

    public PageInfo(string name, GameObject obj, int index) {
        this.pageName = name;
        this.pageObj = obj;
        this.pageIndex = index;
    }
}