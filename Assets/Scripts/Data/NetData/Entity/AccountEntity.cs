using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 账户实体
/// </summary>
public class AccountEntity{

    public int id { get; set; }

    public string UserName { get; set; }

    public string Pwd { get; set; }

    public int YuanBao { get; set; }

    public int LastServerId { get; set; }

    public string LastServerName { get; set; }

    public DateTime CreatTime { get; set; }

    public DateTime UpdateTime { get; set; }

}
