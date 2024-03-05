
using ReadExcel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商品数据管理
/// </summary>
public partial class ProductDBModel : AbstractDBModel<ProductDBModel, ProductEntity>
{


    //public void Dispose()
    //{
    //    lst.Clear();
    //    lst = null;
    //}
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName
    {
        get { return "Product.data"; }
    }
    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parser"></param>
    /// <returns></returns>
    protected override ProductEntity MakeEntity(GameDataTableParser parser)
    {
        ProductEntity entity = new ProductEntity();

        entity.Id = parser.GetFieldValue("Id").ToInt();
        entity.Name = parser.GetFieldValue("Name");
        entity.Price = parser.GetFieldValue("Price").ToFloat();
        entity.PicName = parser.GetFieldValue("PicName");
        entity.Desc = parser.GetFieldValue("Desc");

        return entity;
    }
}
