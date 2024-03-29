﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 测试协议
/// </summary>
public struct TestProto:IProto{

    

    public int Id;
    public string Name;
    public int Type;
    public float Price;

    public ushort ProtoCode { get { return 1001; } }

    public byte[] ToArray()
    {
        using(MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Id);
            ms.WriteUTF8String(Name);
            ms.WriteInt(Type);
            ms.WriteFloat(Price);
            return ms.ToArray();
        }
    }

    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using(MMO_MemoryStream ms=new MMO_MemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadUTF8String();
            proto.Type = ms.ReadInt();
            proto.Price = ms.ReadFloat();
        }

        return proto;
    }
}
