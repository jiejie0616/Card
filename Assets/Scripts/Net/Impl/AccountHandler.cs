using Protocol.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AccountHandler : HindlerBase
{
    private PromptMsg promptMsg = new PromptMsg();

    public override void OnReceive(int subCode, object message)
    {
        switch (subCode)
        {
            case AccountCode.LOGIN:                 // 登录
                LoginResponce((int)message);
                break;
            case AccountCode.REGIST_SRES:           // 注册
                RegistResponce((int)message);
                break;
            default:
                break;
        }
    }

    // 处理登录信息
    private void LoginResponce(int value)
    {
        switch (value)
        {
            case 0:
                //promptMsg.Change("登录成功", Color.green);
                //Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                // 跳到下一个场景
                LoadSceneMsg msg = new LoadSceneMsg(1,
                    delegate()          // 匿名委托
                    {
                        // 加载游戏信息
                        SocketMsg socketMsg = new SocketMsg(OpCode.USER, UserCode.GET_INFO_CREQ, null);
                        Dispatch(AreaCode.NET, 0, socketMsg);
                        Debug.Log("场景加载完成！");
                    }
                    );
                Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, msg);
                break;
            case -1:
                promptMsg.Change("账号不存在", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case -2:
                promptMsg.Change("账号在线", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case -3:
                promptMsg.Change("账号密码不匹配", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            default:
                break;
        }

        //if (value == "登陆成功")
        //{
        //    promptMsg.Change(value.ToString(), Color.green);
        //    Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        //    // TODO 跳到下一个场景
        //    return;
        //}

        //// 登录错误
        //promptMsg.Change(value.ToString(), Color.red);
        //Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
    }

    // 处理注册信息
    private void RegistResponce(int value)
    {
        switch (value)
        {
            case 0:
                promptMsg.Change("注册成功", Color.green);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case -1:
                promptMsg.Change("账号已经存在", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case -2:
                promptMsg.Change("账号输入不合法", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case -3:
                promptMsg.Change("密码不合法", Color.red);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            default:
                break;
        }
        //if (value == "注册成功")
        //{
        //    promptMsg.Change(value.ToString(), Color.green);
        //    Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        //    // TODO 跳到下一个场景
        //    return;
        //}

        //// 登录错误
        //promptMsg.Change(value.ToString(), Color.red);
        //Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
    }
}
