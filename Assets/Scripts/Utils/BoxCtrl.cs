using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision info)
    {
		Debug.Log(info.collider.name);
    }
	void OnCollisionExit(Collision info)
    {

    }
	
}
