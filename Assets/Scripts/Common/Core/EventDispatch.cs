using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者模式
/// </summary>
public class EventDispatch : Singleton<EventDispatch> {

	//委托原型 params是一个计算机函数，
	//表示函数的参数是可变个数的，即可变的方法参数，
	//用于表示类型相同，但参数数量不确定。
	public delegate void OnActionHandler(byte[] buffer);

	private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="handler"></param>
	public void AddEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            dic[protoCode].Add(handler);
        }
        else
        {
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dic[protoCode] = lstHandler;
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dic[protoCode];
            lstHandler.Remove(handler);
            if(lstHandler.Count == 0)
            {
                dic.Remove(protoCode);
            }
        }
        else
        {
        }
    }

    /// <summary>
    /// 派发监听
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="param"></param>
    public void Dispatch(ushort protoCode, byte[] buffer)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dic[protoCode];
            if (lstHandler!=null&& lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        lstHandler[i](buffer);
                    }
                }
            }
        }
    }

}
