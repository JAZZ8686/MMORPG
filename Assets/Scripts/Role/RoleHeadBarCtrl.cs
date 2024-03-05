using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleHeadBarCtrl : MonoBehaviour {

	/// <summary>
	/// 昵称
	/// </summary>
	[SerializeField]
	private UILabel lblNickName;

	/// <summary>
	/// 飘血显示
	/// </summary>
	[SerializeField]
	private bl_HUDText hudText;

	/// <summary>
	/// 血条
	/// </summary>
	[SerializeField]
	private UISlider pbHp; 

	/// <summary>
	/// 对齐的目标点
	/// </summary>
	private Transform m_Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Camera.main == null || UICamera.mainCamera == null||m_Target==null) return;

		//世界坐标点转换成视口坐标
		Vector3 pos = Camera.main.WorldToViewportPoint(m_Target.position);

		//转换成UI摄像机的世界坐标
		Vector3 uiPos = UICamera.mainCamera.ViewportToWorldPoint(pos);

		gameObject.transform.position = uiPos;
	}

	public void Init(Transform target,string nickName,bool isShowHPBar=false)
    {
		m_Target = target;
		lblNickName.text = nickName;

		NGUITools.SetActive(pbHp.gameObject, isShowHPBar);
    }

	public void Hurt(int hurtValue,float pbHPValue)
    {
		pbHp.value = pbHPValue;
    }

}
