using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录窗口UI控制器
/// </summary>
public class UILogonCtrl : UIWindowBase {

    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private UIInput m_InputNickName;

    /// <summary>
    /// 密码
    /// </summary>
    [SerializeField]
    private UIInput m_InputPwd;

    /// <summary>
    /// 提示文字
    /// </summary>
    [SerializeField]
    private UILabel m_LblTip;

    /// <summary>
    /// 重写基类OnBtnClick
    /// </summary>
    /// <param name="go"></param>
    protected override void OnBtnClick(GameObject go)
    {
        Debug.Log("go=" + go.name);
        switch (go.name)
        {
            case "btnToReg":
                BtnToReg();
                break;
            case "btnToLogon":
                Logon();
                break;
        }
    }

    /// <summary>
    /// 注册按钮点击
    /// </summary>
    private void BtnToReg()
    {
        this.Close();
        NextOpenWindow = WindowUIType.Reg;        

        //WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg);

    }
    /// <summary>
    /// 注册按钮点击
    /// </summary>
    private void Logon()
    {
        string nickName = this.m_InputNickName.value.Trim();
        string pwd = this.m_InputPwd.value.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            this.m_LblTip.text = "请输入昵称";
            return;
        }
        if (string.IsNullOrEmpty(nickName))
        {
            this.m_LblTip.text = "请输入密码";
            return;
        }

        string oldNickName = PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME);
        string oldPwd = PlayerPrefs.GetString(GlobalInit.MMO_PWD);
        if (oldNickName != nickName || oldPwd != pwd)
        {
            m_LblTip.text = "您输入的昵称或者密码错误";
            return;
        }
        GlobalInit.Instance.CurrRoleNickName = nickName;
        SceneMgr.Instance.LoadToCity();
    }
}
