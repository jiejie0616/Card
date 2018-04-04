using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : UIBase {
    private Button btnStart;                // 开始游戏按钮
    private Button btnRegist;               // 注册账号按钮

    // Use this for initialization
    void Start()
    {
        btnStart = transform.Find("btnStart").GetComponent<Button>();
        btnRegist = transform.Find("btnRegist").GetComponent<Button>();

        btnStart.onClick.AddListener(startClick);           // 添加点击事件
        btnRegist.onClick.AddListener(registClick);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnStart.onClick.RemoveAllListeners();              // 移除所有点击事件
        btnRegist.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 开始游戏按钮响应函数
    /// </summary>
    private void startClick()
    {
        Dispatch(AreaCode.UI, UIEvent.START_PANEL_ACTIVE, true);      
    }


    /// <summary>
    /// 注册账号按钮按钮响应函数
    /// </summary>
    private void registClick()
    {
        Dispatch(AreaCode.UI, UIEvent.REGIST_PANEL_ACTIVE, true);
    }
}
