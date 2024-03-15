using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuiXiang : MonoBehaviour {
	public static DuiXiang Instance;
	// Use this for initialization
	void Start () {
		Instance = this;
		PengYouQuan.Instance.OnXiaBan += TongZhiXiaBan;
	}
	public void TongZhiXiaBan()
	{
		Debug.Log("小明兄弟 收到小明下班通知");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
