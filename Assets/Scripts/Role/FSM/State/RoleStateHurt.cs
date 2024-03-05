using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受伤状态
/// </summary>
public class RoleStateHurt : RoleStateAbstract
{

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr"></param>
    public RoleStateHurt(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("进入受伤状态");
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), true);

    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);

        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Hurt.ToString()))
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleState.Hurt);
            //如果动画执行了一遍 就切换待机
            if (CurrRoleAnimatorStateInfo.normalizedTime > 1)
            {
                
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            }
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
        }

    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), false);
    }
}
