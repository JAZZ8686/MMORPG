using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiaoming : MonoBehaviour {

	public static Xiaoming Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        //下班同通知别人
        if (Input.GetKeyUp(KeyCode.A))
        {
			//发朋友圈
			if (PengYouQuan.Instance != null)
            {
				PengYouQuan.Instance.OnXiaBan();
			}
			
        }
	}
}
