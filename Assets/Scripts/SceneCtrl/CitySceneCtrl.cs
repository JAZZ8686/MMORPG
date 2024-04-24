using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneCtrl : MonoBehaviour {

	/// <summary>
	/// 主角出生点
	/// </summary>
	[SerializeField]
	private Transform m_PlayerBornPos;


	void Awake()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.MainCity);

        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnPlayClick += OnPlayClick;
            FingerEvent.Instance.OnZoom += OnZoom;
        }
    }

    void Start()
    {
        if (DelegateDefine.Instance.OnScenceLoadOk != null)
        {
			DelegateDefine.Instance.OnScenceLoadOk();
        }
		if (GlobalInit.Instance == null) return;
        //加载玩家
        GameObject obj = RoleMgr.Instance.LoadRole("Role_MainPlayer",RoleType.MainPlayer);

		obj.transform.position = m_PlayerBornPos.position;

        //给当前玩家赋值
        GlobalInit.Instance.CurrPlayer = obj.GetComponent<RoleCtrl>();
		GlobalInit.Instance.CurrPlayer.Init(RoleType.MainPlayer, new RoleInfoBase() { NickName = GlobalInit.Instance.CurrRoleNickName, MaxHP = 10000, CurrHP = 10000 }, new RoleMainPlayerCityAI(GlobalInit.Instance.CurrPlayer)); ;

		UIPlayerInfo.Instance.SetPlayerInfo();
    }

	void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
			SceneMgr.Instance.LoadToShaMo();
        }
    }

	private void OnZoom(FingerEvent.ZoomType obj)
	{
		switch (obj)
		{
			case FingerEvent.ZoomType.In:
				CameraCtrl.Instance.SetCameraZoom(0);
				break;
			case FingerEvent.ZoomType.Out:
				CameraCtrl.Instance.SetCameraZoom(1);
				break;

		}
	}

	private void OnPlayClick()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

        RaycastHit[] hitArr = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));

        if (hitArr.Length > 0)
        {
			RoleCtrl hitRole=hitArr[0].collider.gameObject.GetComponent<RoleCtrl>();
            if (hitRole.currRoleType == RoleType.Monster)
            {
				GlobalInit.Instance.CurrPlayer.LockEnemy = hitRole;
            }
        }
        else
        {
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.collider.gameObject.name.Equals("a_dimian", System.StringComparison.CurrentCultureIgnoreCase))
				{

					if (GlobalInit.Instance.CurrPlayer != null)
					{
						GlobalInit.Instance.CurrPlayer.LockEnemy = null;
						GlobalInit.Instance.CurrPlayer.MoveTo(hitInfo.point);

					}
				}
			}
		}

		
	}

	private void OnFingerDrag(FingerEvent.FingerDir obj)
	{
		switch (obj)
		{
			case FingerEvent.FingerDir.Left:
				CameraCtrl.Instance.SetCameraRotate(0);
				break;
			case FingerEvent.FingerDir.Right:
				CameraCtrl.Instance.SetCameraRotate(1);
				break;
			case FingerEvent.FingerDir.Up:
				CameraCtrl.Instance.SetCameraUpAndDown(0);
				break;
			case FingerEvent.FingerDir.Down:
				CameraCtrl.Instance.SetCameraUpAndDown(1);
				break;
		}
	}

	void OnDestroy()
	{
		if (FingerEvent.Instance != null)
		{
			FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
			FingerEvent.Instance.OnPlayClick -= OnPlayClick;
			FingerEvent.Instance.OnZoom -= OnZoom;
		}
	}
}
