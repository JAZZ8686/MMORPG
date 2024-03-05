using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr : Singleton<RoleMgr> {


	/// <summary>
	/// 根据角色预设名称 加载角色
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameObject LoadRole(string name,RoleType type)
    {
		string path = string.Empty;

        switch (type)
        {
			case RoleType.MainPlayer:
				path = "Player";
				break;
			case RoleType.Monster:
				path = "Monster";
				break;
		}

		return ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.Role, string.Format("{0}/{1}", path,name), cache: true);

	}



	public override void Dispose()
    {
        base.Dispose();
    }

}
