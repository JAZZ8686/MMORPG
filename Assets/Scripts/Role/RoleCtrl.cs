using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : MonoBehaviour {

	/// <summary>
	/// 昵称挂点
	/// </summary>
	[SerializeField]
	private Transform m_HeadBarPos;

	/// <summary>
	/// 头顶UI条
	/// </summary>
	private GameObject m_HeadBar;

	private RoleHeadBarCtrl roleHeadBarCtrl;

	/// <summary>
	/// 动画
	/// </summary>
	[SerializeField]
	public Animator Animator;

	/// <summary>
	/// 控制器
	/// </summary>
	[HideInInspector]
	public CharacterController CharacterController;


	/// <summary>
	/// 移动速度
	/// </summary>
	[SerializeField]
	public float Speed = 10f;
	/// <summary>
	/// 移动的目标点
	/// </summary>
	[HideInInspector]
	public Vector3 TargetPos=Vector3.zero;

	/// <summary>
	/// 出生点
	/// </summary>
	[HideInInspector]
	public Vector3 BornPoint;

	/// <summary>
	/// 视野范围
	/// </summary>
	public float ViewRange;

	/// <summary>
	/// 巡逻范围
	/// </summary>
	public float PatrolRange;

	/// <summary>
	/// 攻击范围
	/// </summary>
	public float AttackRange;

	/// <summary>
	/// 当前角色类型
	/// </summary>
	public RoleType currRoleType = RoleType.None;

	/// <summary>
	/// 当前角色信息
	/// </summary>
	public RoleInfoBase CurrRoleInfo = null;

	/// <summary>
	/// 当前角色有限状态机管理
	/// </summary>
	public RoleFSMMgr currRoleFSMMgr;

	/// <summary>
	/// 当前角色AI
	/// </summary>
	public IRoleAI CurrRoleAI = null;

	/// <summary>
	/// 锁定敌人
	/// </summary>
	[HideInInspector]
	public RoleCtrl LockEnemy;

	/// <summary>
	/// 角色受伤委托
	/// </summary>
	public System.Action OnRoleHurt;

	/// <summary>
	/// 角色死亡
	/// </summary>
	public System.Action<RoleCtrl> OnRoleDie;



	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="roleType">角色类型</param>
	/// <param name="roleInfo">角色信息</param>
	/// <param name="ai">AI</param>
	public void Init(RoleType roleType,RoleInfoBase roleInfo,IRoleAI ai)
    {
		currRoleType= roleType;
		CurrRoleInfo = roleInfo;
		CurrRoleAI = ai;

    }

	// Use this for initialization
	void Start () {
		CharacterController=GetComponent<CharacterController>();

        

		if (currRoleType == RoleType.MainPlayer)
		{
			if (CameraCtrl.Instance != null)
			{
				CameraCtrl.Instance.Init();
			}
		}
		currRoleFSMMgr = new RoleFSMMgr(this);
		ToIdle();
		InitHeadTitleBar();
	}

   



    // Update is called once per frame
    void Update () {

        ////如果角色没有AI 直接返回
        if (CurrRoleAI == null) return;
        CurrRoleAI.DoAI();

        if (currRoleFSMMgr != null)
        {
			
			currRoleFSMMgr.OnUpdate();
            if (Input.GetKeyDown(KeyCode.Space))
            {
				currRoleFSMMgr.ChangeState(RoleState.Attack);
			}
			
        }
		//currRoleFSMMgr.OnUpdate();
		if (CharacterController == null) return;

        if (!CharacterController.isGrounded)
        {
			
			CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

		#region 旧动画
		//      if (Input.GetKeyUp(KeyCode.A))
		//      {
		//	m_Anim.CrossFade("Idle_Normal");//动画融合的方式播放动画
		//      }
		//else if (Input.GetKeyUp(KeyCode.B)){
		//	m_Anim.CrossFade("Idle_Fight");
		//}
		#endregion	
		if (currRoleType == RoleType.MainPlayer)
        {
			CameraAutoFollow();
		}

	}

	/// <summary>
	/// 初始化头顶UI条
	/// </summary>
	private void InitHeadTitleBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
		if(CurrRoleInfo==null) return;
		if (m_HeadBarPos == null) return;
		//克隆预设
		m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIOther, "RoleHeadBar");
		m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
		m_HeadBar.transform.localScale = Vector3.one;
		roleHeadBarCtrl = m_HeadBar.GetComponent<RoleHeadBarCtrl>();


		//给预设赋值
		roleHeadBarCtrl.Init(m_HeadBarPos,CurrRoleInfo.NickName,isShowHPBar:(currRoleType==RoleType.MainPlayer?false:true));
    }


    #region 控制角色方法

	public void ToIdle()
    {
		currRoleFSMMgr.ChangeState(RoleState.Idle);
    }

	public void MoveTo(Vector3 targetPos)
    {
		if (targetPos == Vector3.zero) return;
		TargetPos = targetPos;
		currRoleFSMMgr.ChangeState(RoleState.Run);
	}

	public void ToAttack()
    {
		if (LockEnemy == null) return;
		Debug.LogError("正在攻击了");
		currRoleFSMMgr.ChangeState(RoleState.Attack);
		//暂时写死
		LockEnemy.ToHurt(100, 0.5f);
		//LockEnemy = null;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="attackValue">受到的攻击力</param>
	/// <param name="delay">延迟时间</param>
	public void ToHurt(int attackValue,float delay)
    {

		StartCoroutine(ToHurtCoroutine(attackValue, delay));
		
	}

	private IEnumerator ToHurtCoroutine(int attackValue, float delay)
    {
		yield return new WaitForSeconds(delay);
		Debug.LogError("受到伤害");
		//计算得出伤害
		int hurt = (int)(attackValue* UnityEngine.Random.Range(0.5f,1f));

        if (OnRoleHurt != null)
        {
			OnRoleHurt();
        }

		roleHeadBarCtrl.Hurt(hurt, (float)CurrRoleInfo.CurrHP / CurrRoleInfo.MaxHP);
		CurrRoleInfo.CurrHP -= hurt;

        if (CurrRoleInfo.CurrHP <= 0)
        {
			currRoleFSMMgr.ChangeState(RoleState.Die);
		}
        else
        {
			Debug.LogError("切换到受伤动画");
			currRoleFSMMgr.ChangeState(RoleState.Hurt);
		}

		
	}

	public void ToDie()
    {
		currRoleFSMMgr.ChangeState(RoleState.Die);
	}


    #endregion

	void OnDestroy()
    {
        if (m_HeadBar != null)
        {
			Destroy(m_HeadBar);
        }
    }


    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
		if (CameraCtrl.Instance == null) return;
		CameraCtrl.Instance.transform.position = gameObject.transform.position;
		CameraCtrl.Instance.AutoLookAt(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
		
	}
	
}
