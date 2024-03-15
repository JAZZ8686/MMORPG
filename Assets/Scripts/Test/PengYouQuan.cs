using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PengYouQuan : Singleton<PengYouQuan> {

	//委托原型
	public delegate void OnXiaBanHandler();

	//委托
	public OnXiaBanHandler OnXiaBan;


}
