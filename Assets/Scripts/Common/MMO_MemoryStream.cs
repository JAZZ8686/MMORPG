using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

/// <summary>
/// 数据转换(buye short int long float decimal bool string)
/// </summary>
public class MMO_MemoryStream : MemoryStream {
    public MMO_MemoryStream()
    {

    }
    public MMO_MemoryStream(byte[] buffer) : base(buffer)
    {

    }

    #region Short

    /// <summary>
    /// 从流中读取一个short数组
    /// </summary>
    /// <returns></returns>
    public short ReadShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr,0,2);
        return System.BitConverter.ToInt16(arr, 0);
    }

    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteShort(short value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region UShort

    /// <summary>
    /// 从流中读取一个ushort数组
    /// </summary>
    /// <returns></returns>
    public ushort ReadUShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr, 0, 2);
        return System.BitConverter.ToUInt16(arr, 0);
    }

    /// <summary>
    /// 把一个ushort数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteUShort(ushort value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Int

    /// <summary>
    /// 从流中读取一个int数组
    /// </summary>
    /// <returns></returns>
    public int ReadInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return System.BitConverter.ToInt32(arr, 0);
    }

    /// <summary>
    /// 把一个int数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteInt(int value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region UInt

    /// <summary>
    /// 从流中读取一个uint数组
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return System.BitConverter.ToUInt32(arr, 0);
    }

    /// <summary>
    /// 把一个uint数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteUInt(uint value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Long

    /// <summary>
    /// 从流中读取一个long数组
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return System.BitConverter.ToInt64(arr, 0);
    }

    /// <summary>
    /// 把一个long数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteLong(long value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region ULong

    /// <summary>
    /// 从流中读取一个ulong数组
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return System.BitConverter.ToUInt64(arr, 0);
    }

    /// <summary>
    /// 把一个ulong数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteLong(ulong value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Float

    /// <summary>
    /// 从流中读取一个float数组
    /// </summary>
    /// <returns></returns>
    public float ReadFloat()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToSingle(arr, 0);
    }

    /// <summary>
    /// 把一个float数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteFloat(float value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Double

    /// <summary>
    /// 从流中读取一个double数组
    /// </summary>
    /// <returns></returns>
    public double ReadUDouble()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return System.BitConverter.ToDouble(arr, 0);
    }

    /// <summary>
    /// 把一个double数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteUInt(double value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Bool

    /// <summary>
    /// 从流中读取一个bool数组
    /// </summary>
    /// <returns></returns>
    public bool ReadBool()
    {
        return base.ReadByte() == 1;
    }

    /// <summary>
    /// 把一个bool数据写入流
    /// </summary>
    /// <param name="value"></param>
    public void WriteBool(bool value)
    {
        base.WriteByte((byte)(value==true?1:0));
    }
    #endregion

    #region UTF8String
    /// <summary>
    /// 从流中读取一个string数组
    /// </summary>
    /// <returns></returns>
    public string ReadUTF8String()
    {
        ushort len = this.ReadUShort();
        byte[] arr=new byte[len];
        base.Read(arr, 0, len);
        return Encoding.UTF8.GetString(arr);
    }

    /// <summary>
    /// 把一个string数组写入流
    /// </summary>
    /// <returns></returns>
    public void WriteUTF8String(string str)
    {
        byte[] arr = Encoding.UTF8.GetBytes(str);
        if (arr.Length > 65535)
        {
            throw new InvalidCastException("字符串超出范围");
        }
        this.WriteUShort((ushort)arr.Length);
        base.Write(arr, 0, arr.Length);
    }
    #endregion

}
