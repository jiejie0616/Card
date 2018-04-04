using Protocol.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NetManager : ManagerBase
{
    public static NetManager Instance = null;
    private ClientPeer client = new ClientPeer("127.0.0.1", 6666);          // 客户端

    private void Start()
    {
        client.Connect();
    }

    private void Update()
    {
        if (client == null)
            return;

        while (client.SocketMsgQueue.Count > 0)
        {
            SocketMsg msg = client.SocketMsgQueue.Dequeue();
            ProcessSocketMsg(msg);              // 处理数据
        }
    }

    #region 处理接收到来自服务器的消息

    HindlerBase accountHandler = new AccountHandler();
    HindlerBase userHandler = new UserHandler();
    HindlerBase matchHandler = new MatchHandler();

    private void ProcessSocketMsg(SocketMsg msg)
    {
        switch (msg.OpCode)
        {
            case OpCode.ACCOUNT :
                accountHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            case OpCode.USER:
                userHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            case OpCode.MATCH:
                matchHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            default:
                break;
        }
    }

    #endregion

    #region 给服务器发送消息

    private void Awake()
    {
        Instance = this;
        Add(0, this);           // 添加事件
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case 0:             // 发送消息
                client.Send(message as SocketMsg);
                break;
            default:
                break;
        }
    }

    #endregion
}

