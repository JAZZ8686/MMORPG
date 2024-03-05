using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色AI接口
/// </summary>
public interface IRoleAI{

	/// <summary>
	/// 当前控制的角色
	/// </summary>
	RoleCtrl CurrRole
    {
		get;
		set;
    }

	/// <summary>
	/// 执行AI
	/// </summary>
	void DoAI();
}
