using System;
using System.Collections.Generic;
using UnityEngine;

class UIManager : Singleton<UIManager>
{
    public UIManager() {
        UnityEngine.Debug.Log("UIManager Init");
    }

    public void ShowPage(string pageName) {

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