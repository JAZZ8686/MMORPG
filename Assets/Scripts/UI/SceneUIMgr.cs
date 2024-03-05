using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI管理器
/// </summary>
public class SceneUIMgr : Singleton<SceneUIMgr> {
    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType
    {
        /// <summary>
        /// 登录
        /// </summary>
        Logon,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity
    }

    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneBase CurrentUIScene;

    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
	public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject obj = null;
        switch (type)
        {   
            case SceneUIType.Logon:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScene, "UI Root_LogonScene");
                CurrentUIScene = obj.GetComponent<UISceneLogonCtrl>();
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScene, "UI Root_City");
                CurrentUIScene = obj.GetComponent<UISceneCityCtrl>();
                break;
        }
        

        return null;
    }
}
