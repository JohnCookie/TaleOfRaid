using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResManager : Singleton<ResManager>
{
    public GameObject LoadPrefab(string path) {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) {
            Debug.LogError(string.Format("Resource Load failed: {0}", path));
        }
        return prefab;
    }
}
