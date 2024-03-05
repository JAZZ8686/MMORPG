using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLogonCtrl : UISceneBase {

    protected override void OnStart()
    {
        base.OnStart();
        GameObject obj = WindowUIMgr.Instance.OpenWindow(WindowUIType.Logon);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("关闭");
            WindowUIMgr.Instance.CloseWindow(WindowUIType.Logon);
        }
    }

}
