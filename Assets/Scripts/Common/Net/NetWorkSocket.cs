﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetWorkSocket : MonoBehaviour {

    #region 单例
    private static NetWorkSocket instance;

    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("NetWorkSocket");
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<NetWorkSocket>();
            }
            return instance;
        }
    }
    #endregion

    //private byte[] buffer = new byte[1024];

    #region 发送消息所需变量
    //发送消息队列
    private Queue<byte[]> m_SendQueue=new Queue<byte[]>();

    //检查队列的委托
    private Action m_CheckSendQuene;
    #endregion

    #region 接收消息所需变量
    //接收数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[2048];

    //接收数据包的缓冲数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    //接收消息的队列
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    private int m_ReceiveCount = 0;

    #endregion

    /// <summary>
    /// 客户端socket
    /// </summary>
    private Socket m_Client;

    void Update()
    {
        #region 从队列中获取数据
        while (true)
        {
            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();

                        ushort protoCode = 0;
                        byte[] protoContent = new byte[buffer.Length - 2];
                        using(MMO_MemoryStream ms=new MMO_MemoryStream(buffer))
                        {
                            //协议编号
                            protoCode = ms.ReadUShort();
                            ms.Read(protoContent, 0, protoContent.Length);

                            EventDispatch.Instance.Dispatch(protoCode, protoContent);

                            //临时
                            //GlobalInit.Instance.OnReceiveProto(protoCode,protoContent); 
                        }
                        //using (MMO_MemoryStream ms = new MMO_MemoryStream())
                        //{
                        //    ms.WriteUTF8String("客户端时间=" + DateTime.Now.ToString());
                        //    this.SendMsg(ms.ToArray());
                        //}
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }
        #endregion


    }

    private void OnDestroy()
    {
        if (m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }

    #region Connect 连接到socket服务器
    /// <summary>
    /// 连接到socket服务器
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param>
    public void Connect(string ip,int port)
    {
        //如果socket已经存在 并且处于连接中状态 则直接返回
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQuene = OnCheckSendQueueCallBack;

            ReceiveMsg();
            Debug.Log("连接成功");
        }
        catch(Exception e)
        {
            Debug.Log("连接失败=" + e.Message);
        }
    }
    #endregion

    #region OnCheckSendQueueCallBack 检查队列的委托回调
    /// <summary>
    /// 检查队列的委托回调
    /// </summary>
    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue)
        {
            //如果队列中有数据包 则发送数据包
            if (m_SendQueue.Count > 0)
            {
                //发送数据包
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    #endregion

    #region MakeData 封装数据包
    /// <summary>
    /// 封装数据包
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeData(byte[] data)
    {
        byte[] retBuffer = null;
        using(MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)data.Length);
            ms.Write(data, 0, data.Length);
            retBuffer = ms.ToArray();
        }

        return retBuffer;
    }
    #endregion

    #region SendMsg 发送消息 把消息加入队列
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(byte[] buffer)
    {
        //得到封装后的数据包
        byte[] sendBuffer = MakeData(buffer);

        lock (m_SendQueue)
        {
            //把数据包加入队列
            m_SendQueue.Enqueue(sendBuffer);

            //启动委托(执行委托)
            m_CheckSendQuene.BeginInvoke(null, null);
        }
    }
    #endregion


    #region Send 真正发送数据包到服务器
    /// <summary>
    /// 真正发送数据包到服务器
    /// </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        m_Client.BeginSend(buffer, 0, buffer.Length,SocketFlags.None,SendCallBack,m_Client);
    }
    #endregion

    #region SendCallBack 发送数据包的回调
    /// <summary>
    /// 发送数据包的回调
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallBack(IAsyncResult ar)
    {
        m_Client.EndSend(ar);

        //继续检查队列
        OnCheckSendQueueCallBack();
    }
    #endregion

    //=================================================

    #region ReceiveMsg 接收数据
    /// <summary>
    /// 接收数据
    /// </summary>
    private void ReceiveMsg()
    {
        //异步接收数据
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }
    #endregion

    #region ReceiveCallBack 接收数据回调
    /// <summary>
    /// 接收数据回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try//如果直接关闭unity结束任务会异常
        {
            int len = m_Client.EndReceive(ar);

            if (len > 0)
            {
                //已经接收到数据

                //把接收到的数据写入缓冲数据流的尾部
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //把制定长度的字节写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                //如果缓存数据流的长度>2 说明至少有个不完整的包过来
                //为什么这里是2 因为客户端封装数据包 用的ushort 长度是2
                if (m_ReceiveMS.Length > 2)
                {
                    //进行循环 拆分数据包
                    while (true)
                    {
                        //把数据流指针位置放在0处
                        m_ReceiveMS.Position = 0;

                        //currMsgLen = 包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();

                        //currFullMsgLen 总包的长度=包头长度+包体长度
                        int currFullMsgLen = 2 + currMsgLen;

                        //如果数据流的长度>=整包的长度 说明至少受到了一个完整版
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //至少收到一个完整包

                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLen];

                            //把数据流指针放到2的位置 也就是包体的位置
                            m_ReceiveMS.Position = 2;

                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);

                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }
                            

                            

                            //===============处理剩余字节数组==========================================

                            //剩余字节长度
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                            if (remainLen > 0)
                            {
                                //把指针放在第一个包的尾部
                                m_ReceiveMS.Position = currFullMsgLen;

                                //定义剩余字节数组
                                byte[] remainBuffer = new byte[remainLen];

                                //把数据流读到剩余字节数组
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                //把剩余字节数组重新写入数据流
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                remainBuffer = null;
                            }
                            else
                            {
                                //没有剩余字节

                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                                break;
                            }


                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }
                    }
                }

                //进行下一次接收数据包
                ReceiveMsg();
            }
            else
            {
                //客户端断开连接
                Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));

            }
        }
        catch (Exception ex)
        {
            //客户端断开连接
            Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
        }


    }
    #endregion
}
