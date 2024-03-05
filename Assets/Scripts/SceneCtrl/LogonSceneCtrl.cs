using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonSceneCtrl : MonoBehaviour {
	GameObject obj;
	void Awake()
    {
		//obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScene,"UI Root_LogonScene",true);
		SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.Logon);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			Destroy(obj);
        }
		else if (Input.GetKeyDown(KeyCode.C))
		{
			obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScene, "UI Root_LogonScene", true);
		}
	}
}
