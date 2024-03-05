using ReadExcel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据管理基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T,P>where T:new() where P:AbstractEntity {

    protected List<P> m_List;
    protected Dictionary<int, P> m_Dic;

    public AbstractDBModel()
    {
        m_List=new List<P>();
        m_Dic=new Dictionary<int, P>();

        LoadData();
    }

    #region 单例
    public static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    #region 需要子类实现的属性或方法
    /// <summary>
    /// 数据文件名称
    /// </summary>
    protected abstract string FileName { get;}

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parser"></param>
    /// <returns></returns>
    protected abstract P MakeEntity(GameDataTableParser parser);
    #endregion

    #region 加载数据LoadData
    private void LoadData()
    {
        //路径问题
        using (GameDataTableParser parse = new GameDataTableParser(string.Format(@"E:\A_Project_WZL\MMORPG\www\Data\{0}", FileName)))
        {
            while (!parse.Eof)
            {
                

                //lst.Add(entity);
           

                P p = MakeEntity(parse);
                m_List.Add(p);     //dic[entity.Id] = entity;
                m_Dic[p.Id] = p;
                parse.Next();
            }
        }
    }
    #endregion

    #region GetList 获取集合
    public List<P> GetList()
    {
        return m_List;
    }
    #endregion

    #region Get 根据编号获取实体
    /// <summary>
    /// 根据编号获取实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public P Get(int id)
    {
        if (m_Dic.ContainsKey(id))
        {
            return m_Dic[id];
        }
        return null;
    }
    #endregion

}
