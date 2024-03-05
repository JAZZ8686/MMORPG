using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {


	public static CameraCtrl Instance;

	/// <summary>
	/// 控制摄像机上下
	/// </summary>
	[SerializeField]
	private Transform m_CameraUpAndDown;

	/// <summary>
	/// 摄像机缩放容器
	/// </summary>
	[SerializeField]
	private Transform m_CameraZoomContainer;

	/// <summary>
	/// 摄像机容器
	/// </summary>
	[SerializeField]
	private Transform m_CameraContainer;

	public void Init()
    {
		m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 35f, 80f));
	}

	void Awake()
    {
		Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// 设置摄像机旋转
	/// </summary>
	/// <param name="type">0=左 1=右</param>
	public void SetCameraRotate(int type)
    {
		transform.Rotate(0, 40 * Time.deltaTime * (type == 1 ? -1 : 1), 0);
    }
	/// <summary>
	/// 设置摄像机上下
	/// </summary>
	/// <param name="type">0=上 1=下</param>
	public void SetCameraUpAndDown(int type)
    {
		m_CameraUpAndDown.Rotate(0, 0, 30 * Time.deltaTime * (type == 1 ? -1 : 1));
		m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0,Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z,35f,80f));
	}
	/// <summary>
	/// 设置摄像机缩放
	/// </summary>
	/// <param name="type">0=拉近 1=拉远</param>
	public void SetCameraZoom(int type)
    {
		m_CameraContainer.Translate(Vector3.forward * 10 * Time.deltaTime * (type == 1 ? -1 : 1));
		m_CameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.transform.localPosition.z, -5f, 5f));
	}

	public void AutoLookAt(Vector3 pos)
    {
		m_CameraZoomContainer.LookAt(pos);
    }

	void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 15f);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 14f);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, 13f);
	}
}
