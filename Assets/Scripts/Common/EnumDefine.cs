using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    Logon,
    City,
    ShaMo
}

/// <summary>
/// 窗口类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    Logon,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo
}

/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Center
}

/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    Normal,//正常打开
    CenterToBig,//中间放大
    FromTop,//从下往上
    FromDown,
    FromLeft,
    FromRight
}

/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None=0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer=1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster=2
}

public enum RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None=0,
    /// <summary>
    /// 待机
    /// </summary>
    Idle=1,
    /// <summary>
    /// 跑了
    /// </summary>
    Run=2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack=3,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt=4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die=5
}

/// <summary>
/// 角色动画z状态名称
/// </summary>
public enum RoleAnimatorName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    Hurt,
    Die,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3,
}

public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    CurrState
}
