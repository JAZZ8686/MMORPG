using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角主城AI
/// </summary>
public class RoleMainPlayerCityAI : IRoleAI
{
    public RoleCtrl CurrRole { get; set; }

    public RoleMainPlayerCityAI(RoleCtrl roleCtrl)
    {
        CurrRole = roleCtrl;
    }

    public void DoAI()
    {
        //执行AI
        //1.如果我有锁定敌人 就进行攻击
        if (CurrRole.LockEnemy != null)
        {

            if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
            {
                CurrRole.LockEnemy = null;
                return;
            }


            if (Vector3.Distance(CurrRole.transform.position, CurrRole.LockEnemy.transform.position) <= 2)//初级课程自行添加的距离判断
            {
                if (CurrRole.currRoleFSMMgr.CurrRoleStateEnum != RoleState.Attack)
                {
                    Debug.LogError("主角攻击");
                    CurrRole.ToAttack();
                }
            }

            ////if (CurrRole.currRoleFSMMgr.CurrRoleStateEnum != RoleState.Attack)
            ////{
            ////    Debug.LogError("主角攻击");
            ////    CurrRole.ToAttack();
            ////}
            
        }

    }
}
