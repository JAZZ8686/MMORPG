using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour {

    public delegate void OnReceiveProtoHandler(ushort protoCode, byte[] buffer);

    //定义委托
    public OnReceiveProtoHandler OnReceiveProto;

    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    /// <summary>
    /// 账户服务器地址
    /// </summary>
    public const string WebAccountUrl = "http://172.26.151.97:50608/";

    public const string SocketIP = "172.26.159.165";

    public const ushort Port = 1011;

    public static GlobalInit Instance;

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    public string CurrRoleNickName;

    /// <summary>
    /// 当前玩家
    /// </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;

    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(0f, 0f, 0f, 1f));

	void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
