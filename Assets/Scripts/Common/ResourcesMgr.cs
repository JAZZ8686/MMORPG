using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResourcesMgr:Singleton<ResourcesMgr>
{
    #region ResourcesType 资源类型
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourcesType
    {
        /// <summary>
        /// 场景UI
        /// </summary>
        UIScene,
        /// <summary>
        /// 窗口
        /// </summary>
        UIWindows,
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 特效
        /// </summary>
        Effect,
        /// <summary>
        /// 其他
        /// </summary>
        UIOther
    }
    #endregion

    private Hashtable m_PrefabTable;

    public ResourcesMgr()
    {
        m_PrefabTable=new Hashtable();
    }
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">短路径</param>
    /// <param name="cache">是否放入缓存</param>
    /// <returns>预设克隆体</returns>
    public GameObject Load(ResourcesType type, string path,bool cache=false)
    {
        
        GameObject obj = null;
        if (m_PrefabTable.Contains(path))
        {
            Debug.Log("资源从缓存中加载");
            obj = m_PrefabTable[path] as GameObject;
        }
        else
        {
            StringBuilder sbr = new StringBuilder();
            switch (type)
            {
                case ResourcesType.UIScene:
                    sbr.Append("UIPrefab/UIScene/");
                    break;
                case ResourcesType.UIWindows:
                    sbr.Append("UIPrefab/UIWindows/");
                    break;
                case ResourcesType.Role:
                    sbr.Append("RolePrefab/");
                    break;
                case ResourcesType.Effect:
                    sbr.Append("EffectPrefab/");
                    break;
                case ResourcesType.UIOther:
                    sbr.Append("UIPrefab/UIOther/");
                    break;
            }
            sbr.Append(path);
            obj = Resources.Load(sbr.ToString()) as GameObject;
            if (cache)
            {
                m_PrefabTable.Add(path, obj);
            }
        }
            

        return GameObject.Instantiate(obj);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();

        m_PrefabTable.Clear();

        //把未使用的资源进行释放
        Resources.UnloadUnusedAssets();
    }


}
