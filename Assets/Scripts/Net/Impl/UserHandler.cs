using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色网络功能处理类
/// </summary>
public class UserHandler : HindlerBase
{
    public override void OnReceive(int subCode, object message)
    {
        switch (subCode)
        {
            case UserCode.CREATE_SRES:
                CreateResponce((int)message);
                break;
            case UserCode.GET_INFO_SRES:
                GetInfoResponse(message as UserDto);
                break;
            case UserCode.ONLINE_SRES:
                OnlineResponce((int)message);
                break;
            default:
                break; 
        }
    }

    private SocketMsg socketMsg = new SocketMsg();

    /// <summary>
    /// 获取信息的回应
    /// </summary>
    /// <param name="user">用户信息</param>
    private void GetInfoResponse(UserDto user)
    {
        if (user == null)
        {
            // 没有角色，显示创建窗口
            Dispatch(AreaCode.UI, UIEvent.CREATE_PANEL_ACTIVE, true);
        }
        else
        {
            // 有角色
            Dispatch(AreaCode.UI, UIEvent.CREATE_PANEL_ACTIVE, false);
            //// 角色上线
            //socketMsg.Change(OpCode.USER, UserCode.ONLINE_CREQ, null);
            //Dispatch(AreaCode.NET, 0, socketMsg);
            // 保存数据
            Models.gameModel.userDto = user;
            //更新UI显示
            Dispatch(AreaCode.UI, UIEvent.REFRESH_INFO_PANEL, user);
        }
    }

    /// <summary>
    /// 上线的回应
    /// </summary>
    /// <param name="result"></param>
    private void OnlineResponce(int result)
    {
        switch (result)
        {
            case 0:
                // 上线成功
                Debug.Log("上线成功");
                break;
            case -1:
                // 客户端非法登录
                Debug.LogError("客户端非法登录");
                break;
            case -2:
                // 没有角色 不能创建
                Debug.LogError("没有角色 不能创建");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 创建角色回应
    /// </summary>
    /// <param name="result"></param>
    private void CreateResponce(int result)
    {
        switch (result)
        {
            case 0:
                // 创建成功
                // 隐藏创建面板
                Dispatch(AreaCode.UI, UIEvent.CREATE_PANEL_ACTIVE, false);
                // 获取角色信息
                socketMsg.Change(OpCode.USER, UserCode.GET_INFO_CREQ, null);
                Dispatch(AreaCode.NET, 0, socketMsg);
                break;
            case -1:
                // 客户端非法登录
                Debug.LogError("客户端非法登录");
                break;
            case -2:
                // 已经有角色 重复创建
                Debug.LogError("已经有角色 重复创建");
                break;
            default:
                break;
        }
    }
}
