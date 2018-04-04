using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储所有 UI 界面的事件码
/// </summary>
public class UIEvent{
    public const int START_PANEL_ACTIVE = 0;        // 设置开始界面的显示 
    public const int REGIST_PANEL_ACTIVE = 1;       // 设置注册界面的显示
    public const int REFRESH_INFO_PANEL = 2;        // 设置刷新玩家信息窗口
    public const int SHOW_ENTER_ROOM_BUTTON = 3;    // 设置进入房间按钮的显示
    public const int CREATE_PANEL_ACTIVE = 4;       // 设置创建界面的显示

    public const int PROMPT_MSG = int.MaxValue;     // 设置提示消息的显示
}
