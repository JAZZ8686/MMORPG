using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class TestMMOMemory : MonoBehaviour {

	void Start () {
        //1.连接到服务器
        NetWorkSocket.Instance.Connect(GlobalInit.SocketIP, GlobalInit.Port);


        //自定义的协议传输 长度是24

        GlobalInit.Instance.OnReceiveProto = OnReceiveProtoCallBack;

    }

    //委托回调
    private void OnReceiveProtoCallBack(ushort protoCode, byte[] buffer)
    {
        Debug.Log("protoCode=" + protoCode);
        if (protoCode == ProtoCodeDef.Mail_Get_Detail)
        {
            Mail_Get_DetailProto proto = Mail_Get_DetailProto.GetProto(buffer);

            Debug.Log("IsSuccess" + proto.IsSuccess);
            Debug.Log("ErrorCode" + proto.ErrorCode);
        }
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    TestProto proto = new TestProto();
        //    proto.Id = 100;
        //    proto.Name = "测试协议";
        //    proto.Price = 56.5f;
        //    proto.Type = 80;

        //    NetWorkSocket.Instance.SendMsg(proto.ToArray());
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mail_Get_DetailProto proto = new Mail_Get_DetailProto();
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
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

