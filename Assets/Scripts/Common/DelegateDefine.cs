using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDefine : Singleton<DelegateDefine>{

    /// <summary>
    /// 场景加载完毕
    /// </summary>
    public Action OnScenceLoadOk;

}
