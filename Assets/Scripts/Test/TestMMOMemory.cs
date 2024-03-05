using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class TestMMOMemory : MonoBehaviour {

	void Start () {
        //1.连接到服务器
        NetWorkSocket.Instance.Connect("172.22.30.27",1011);

        //2.发消息

    }

    private void Send(string msg)
    {
        using(MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteUTF8String(msg);

            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Send("你好A");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Send("你好B");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < 10; i++)
            {
                Send("你好循环"+i);
            }
            
        }
    }
}

