using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class TestMMOMemory : MonoBehaviour {

	void Start () {
        //1.连接到服务器
        //NetWorkSocket.Instance.Connect("172.19.196.66",1011);

        TestProto proto = new TestProto();
        proto.Id = 1;
        proto.Name = "测试";
        proto.Type = 0;
        proto.Price = 99.5f;

        byte[] buffer = null;

        //1.json方式
        //string json = JsonMapper.ToJson(proto);

        //using (MMO_MemoryStream ms=new MMO_MemoryStream())
        //{
        //    ms.WriteUTF8String(json);
        //    buffer=ms.ToArray();
        //}
        //Debug.Log("buffer:" + buffer.Length);
        //json转换为数组后长度是48

        //==================================================
        //2.自定义
        buffer = proto.ToArray();
        Debug.Log("buffer:" + buffer.Length);

        TestProto proto2 = TestProto.GetProto(buffer);

        Debug.Log("Name="+proto2.Name);

        //自定义的协议传输 长度是24

    }

    private void Send(string msg)
    {
        //using(MMO_MemoryStream ms=new MMO_MemoryStream())
        //{
        //    ms.WriteUTF8String(msg);

        //    NetWorkSocket.Instance.SendMsg(ms.ToArray());
        //}
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Send("你好A");
        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Send("你好B");
        //}
        //else if (Input.GetKeyDown(KeyCode.C))
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Send("你好循环"+i);
        //    }
            
        //}
    }
}

