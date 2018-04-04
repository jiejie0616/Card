using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour {
    public UserDto userDto { get; set; }        // 登录用户数据
    public MatchRoomDto matchRoomDto { get; set; }  // 匹配房间信息
}
