using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口UI管理器
/// </summary>
public class WindowUIMgr : Singleton<WindowUIMgr>{

    private Dictionary<WindowUIType, UIWindowBase> m_DicWindow = new Dictionary<WindowUIType, UIWindowBase>();
    /// <summary>
    /// 已经打开的窗口数量
    /// </summary>
    public int OpenWindowCount
    {
        get
        {
            return m_DicWindow.Count;
        }
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
	public GameObject OpenWindow(WindowUIType type)
    {
        if (type == WindowUIType.None)
            return null;
        GameObject obj = null;
        //如果窗口不存在
        if (!m_DicWindow.ContainsKey(type))
        {
            obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIWindows, string.Format("pan{0}", type.ToString()), true);

            if (obj == null) return null;

            UIWindowBase windowBase = obj.GetComponent<UIWindowBase>();
            if (windowBase == null) return null;

            //层级管理

            m_DicWindow.Add(type, windowBase);

            windowBase.CurrentUIType = type;

            Transform transformParent = null;

            switch (windowBase.containerType)
            {
                case WindowUIContainerType.Center:
                    transformParent = SceneUIMgr.Instance.CurrentUIScene.Container_Center;
                    break;
                case WindowUIContainerType.TopLeft:
                    break;
                case WindowUIContainerType.TopRight:
                    break;
                case WindowUIContainerType.BottomLeft:
                    break;
                case WindowUIContainerType.BottomRight:
                    break;
            }

            obj.transform.parent = transformParent;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            NGUITools.SetActive(obj, false);

            StartShowWindow(windowBase, true);
        }
        else
        {
            obj = m_DicWindow[type].gameObject;
        }
        //设置层级
        LayerUIMgr.Instance.SetLayer(obj);

        return obj;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="type"></param>
    public void CloseWindow(WindowUIType type)
    {
        if (m_DicWindow.ContainsKey(type))
        {
            StartShowWindow(m_DicWindow[type],false);
        }
    }

    /// <summary>
    /// 开始打开窗口
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="isOPen"></param>
    protected void StartShowWindow(UIWindowBase windowBase,bool isOPen)
    {
        switch (windowBase.showStyle)
        {
            case WindowShowStyle.Normal:
                ShowNormal(windowBase, isOPen);
                break;
            case WindowShowStyle.CenterToBig:
                ShowCenterToBig(windowBase,isOPen);
                break;
            case WindowShowStyle.FromTop:
                ShowFromDir(windowBase, 0, isOPen);
                break;
            case WindowShowStyle.FromDown:
                ShowFromDir(windowBase, 1, isOPen);
                break;
            case WindowShowStyle.FromLeft:
                ShowFromDir(windowBase, 2, isOPen);
                break;
            case WindowShowStyle.FromRight:
                ShowFromDir(windowBase, 3, isOPen);
                break;
        }
    }

    /// <summary>
    /// 销毁窗口
    /// </summary>
    /// <param name="obj"></param>
    private void DestroyWindow(UIWindowBase windowBase)
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        m_DicWindow.Remove(windowBase.CurrentUIType);
        GameObject.Destroy(windowBase.gameObject);
    }


    #region 打开效果
    private void ShowNormal(UIWindowBase windowBase, bool isOpen)
    {

        if (isOpen)
        {
            NGUITools.SetActive(windowBase.gameObject, true);
        }
        else
        {
            DestroyWindow(windowBase);
        }

    }

    private void ShowCenterToBig(UIWindowBase windowBase, bool isOpen)
    {
        TweenScale ts = windowBase.gameObject.GetOrCreatComponent<TweenScale>();
        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = windowBase.duration;
        ts.animationCurve = GlobalInit.Instance.UIAnimationCurve;
        ts.SetOnFinished(() =>
        {
            if (!isOpen)
            {
                DestroyWindow(windowBase);
            }
        });
        NGUITools.SetActive(windowBase.gameObject, true);
        if (!isOpen)
        {
            ts.Play(isOpen);
        }
    }
    /// <summary>
    /// 从不同方向加载
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="dirType">0=从上  1=从下 2=从左  3=从右</param>
    /// <param name="isOPen"></param>
    private void ShowFromDir(UIWindowBase windowBase,int dirType,bool isOpen)
    {
        TweenPosition tp = windowBase.gameObject.GetOrCreatComponent<TweenPosition>();
        tp.animationCurve = GlobalInit.Instance.UIAnimationCurve;
        Vector3 from = Vector3.zero;

        switch (dirType)
        {
            case 0:
                from = new Vector3(0, 1000, 0);
                break;
            case 1:
                from = new Vector3(0, -1000, 0);
                break;
            case 2:
                from = new Vector3(-1400, 0, 0);
                break;
            case 3:
                from = new Vector3(1400, 0, 0);
                break;
        }




        tp.from = from;
        tp.to = Vector3.one;
        tp.duration = windowBase.duration;
        tp.SetOnFinished(() =>
        {
            if (!isOpen)
            {
                DestroyWindow(windowBase);
            }
        });
        NGUITools.SetActive(windowBase.gameObject, true);
        if (!isOpen)
        {
            tp.Play(isOpen);
        }

    }

    #endregion
}
