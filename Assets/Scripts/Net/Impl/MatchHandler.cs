using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : HindlerBase {
    public override void OnReceive(int subCode, object message)
    {
        switch (subCode)
        {
            case MatchCode.ENTER_SRES:
                EnterResponce(message as MatchRoomDto);
                break;
            case MatchCode.ENTER_BRO:
                EnterBro(message as UserDto);
                break;
            case MatchCode.LEAVE_BRO:
                LeaveBro((int)message);
                break;
            case MatchCode.READY_BRO:
                ReadyBro((int)message);
                break;
            case MatchCode.START_BRO:
                StartBro();
                break;
            default:
                break;
        }
    }

    private PromptMsg promptMsg = new PromptMsg();

    /// <summary>
    /// 进入匹配界面响应
    /// </summary>
    /// <param name="matchRoom"></param>
    private void EnterResponce(MatchRoomDto matchRoom)
    {
        // 存储本地
        Models.gameModel.matchRoomDto = matchRoom;
        // TODO 战斗房间 UI
        // 显示进入房间按钮
        Dispatch(AreaCode.UI, UIEvent.SHOW_ENTER_ROOM_BUTTON, null);
    }

    /// <summary>
    /// 收到进入匹配房间广播信息
    /// </summary>
    /// <param name="matchRoom"></param>
    private void EnterBro(UserDto newUser)
    {
        Models.gameModel.matchRoomDto.Add(newUser);
        // TODO 进行UI更新
        // 给用户一个提示
        promptMsg.Change("有新玩家（" + newUser.Name + ")进入", Color.green);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
    }

    /// <summary>
    /// 收到离开匹配房间广播信息
    /// </summary>
    /// <param name="userId"></param>
    private void LeaveBro(int userId)
    {
        // TODO UI 更新
        Models.gameModel.matchRoomDto.Leave(userId);
    }

    /// <summary>
    /// 收到准备广播
    /// </summary>
    /// <param name="userId"></param>
    private void ReadyBro(int userId)
    {
        // 保存数据
        Models.gameModel.matchRoomDto.Ready(userId);
        // TODO UI更新
    }

    /// <summary>
    /// 收到开始游戏广播
    /// </summary>
    private void StartBro()
    {
        promptMsg.Change("所有玩家准备开始游戏", Color.green);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        // TODO 开始游戏
    }
}
