using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaoPo : MonoBehaviour {
	public static LaoPo Instance;
	// Use this for initialization
	void Start () {
		Instance = this;

		PengYouQuan.Instance.OnXiaBan = TongZhiXiaBan;
	}
	
	public void TongZhiXiaBan()
    {
		Debug.Log("小明老婆 收到小明下班通知");
    }


	// Update is called once per frame
	void Update () {
		
	}
}
