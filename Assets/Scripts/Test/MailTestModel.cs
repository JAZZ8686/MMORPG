using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailTestModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventDispatch.Instance.AddEventListener(ProtoCodeDef.Mail_Get_Detail, OnGetList);
	}

    private void OnGetList(byte[] buffer)
    {
        Mail_Get_DetailProto proto = Mail_Get_DetailProto.GetProto(buffer);
        Debug.Log(proto.IsSuccess);
        Debug.Log(proto.Name);
    }

    void OnDestroy()
    {
        EventDispatch.Instance.RemoveEventListener(ProtoCodeDef.Mail_Get_Detail, OnGetList);
    }
}
