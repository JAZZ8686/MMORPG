using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{

    /// <summary>
	/// 转身速度
	/// </summary>
	private float m_RotationSpeed = 0.2f;
    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaterion;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr"></param>
    public RoleStateRun(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        m_RotationSpeed = 0;

        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);

    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleState.Run);
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
        }
        if (Vector3.Distance( new Vector3(CurrRoleFSMMgr.CurrRoleCtrl.TargetPos.x,0, CurrRoleFSMMgr.CurrRoleCtrl.TargetPos.z), 
            new Vector3(CurrRoleFSMMgr.CurrRoleCtrl.transform.position.x,0, CurrRoleFSMMgr.CurrRoleCtrl.transform.position.z)) > 0.1f)
        {
            Vector3 direction = CurrRoleFSMMgr.CurrRoleCtrl.TargetPos - CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
            direction = direction.normalized;//归一化
            direction = direction * Time.deltaTime * CurrRoleFSMMgr.CurrRoleCtrl.Speed;
            direction.y = 0;

            //transform.LookAt(new Vector3(m_TargetPos.x, transform.position.y, m_TargetPos.z));
            if (m_RotationSpeed<=1)
            {
                m_RotationSpeed += 2f*Time.deltaTime;
                m_TargetQuaterion = Quaternion.LookRotation(direction);
                CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation = Quaternion.Lerp(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaterion, m_RotationSpeed);

                if(Quaternion.Angle(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaterion) < 1)
                {
                    m_RotationSpeed = 0;
                }

            }
            CurrRoleFSMMgr.CurrRoleCtrl.CharacterController.Move(direction);

        }
        else
        {

            CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
        }

    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
}
