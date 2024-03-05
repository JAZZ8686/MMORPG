using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注册窗口UI控制器
/// </summary>
public class UIRegCtrl : UIWindowBase{

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
    /// 确认密码
    /// </summary>
    [SerializeField]
    private UIInput m_InputPwd2;
    /// <summary>
    /// 提示
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
            case "btnToLogon":
                BtnLogon();
                break;
            case "btnToReg":
                Reg();
                break;
        }
    }

    /// <summary>
    /// 打开登录窗口
    /// </summary>
    private void BtnLogon()
    {
        Close();
        NextOpenWindow = WindowUIType.Logon;
        //WindowUIMgr.Instance.OpenWindow(WindowUIType.Logon);
    }

    private void Reg()
    {
        string nickName = this.m_InputNickName.value.Trim();
        string pwd = this.m_InputPwd.value.Trim();
        string pwd2 = this.m_InputPwd2.value.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            this.m_LblTip.text = "请输入昵称";
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            this.m_LblTip.text = "请输入密码";
            return;
        }
        if (string.IsNullOrEmpty(pwd2))
        {
            this.m_LblTip.text = "请输入确认密码";
            return;
        }

        if (pwd != pwd2)
        {
            this.m_LblTip.text = "两次输入的密码不一致";
            return;
        }


        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickName);
        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);

        GlobalInit.Instance.CurrRoleNickName = nickName;


        SceneMgr.Instance.LoadToCity();
    }

}
