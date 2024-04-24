﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr> {

    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType CurrentSceneType
    {
        get;
        private set;
    }

    public void LoadLogOn()
    {
        CurrentSceneType = SceneType.Logon;
        
        SceneManager.LoadScene("Scene_Loading");

        //Application.LoadLevel("Logon");
    }

    /// <summary>
    /// 去城镇场景
    /// </summary>
    public void LoadToCity()
    {
        CurrentSceneType = SceneType.City;
        //Application.LoadLevel("GameScene_HuPaoCun");
        SceneManager.LoadScene("Scene_Loading");
    }
    /// <summary>
    /// 去城镇场景
    /// </summary>
    public void LoadToShaMo()
    {
        CurrentSceneType = SceneType.ShaMo;
        SceneManager.LoadScene("Scene_Loading");
    }
}
