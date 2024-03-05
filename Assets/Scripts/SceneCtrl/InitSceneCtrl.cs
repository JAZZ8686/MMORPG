using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadLogOn());	
	}
	
	IEnumerator LoadLogOn()
    {
		yield return new WaitForSeconds(2f);
		SceneMgr.Instance.LoadLogOn();
    }
}
