using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录场景UI控制器
/// </summary>
public class UISceneLoadingCtrl : UISceneBase {

	/// <summary>
	/// 进度条
	/// </summary>
	[SerializeField]
	private UIProgressBar m_Progress;

	/// <summary>
	/// 设置进度条的值
	/// </summary>
	[SerializeField]
	private UILabel m_lblProgress;

	/// <summary>
	/// 发光的图
	/// </summary>
	[SerializeField]
	private UISprite m_SprProgressLight;

	/// <summary>
	/// 设置进度条的值
	/// </summary>
	/// <param name="value"></param>
	public void SetProgressValue(float value)
    {
		m_Progress.value = value;
		m_lblProgress.text = string.Format("{0}%", (int)(value * 100));

		m_SprProgressLight.transform.localPosition = new Vector3(900f*value, 0, 0);
    }
	
}
