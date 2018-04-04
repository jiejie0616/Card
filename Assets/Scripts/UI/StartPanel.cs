using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIBase {

    private Button btnLogin;                // 登录按钮
    private Button btnClose;                // 关闭按钮
    private InputField inputAccount;        // 用户名输入框
    private InputField inputPassword;       // 密码输入框

    private PromptMsg promptMsg;            // 提示信息
    private SocketMsg socketMsg;            // 发送的信息

    void Awake()
    {
        Bind(UIEvent.START_PANEL_ACTIVE);           // 注册事件码
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.START_PANEL_ACTIVE:
                setPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        btnLogin = transform.Find("btnLogin").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        inputAccount = transform.Find("inputAccount").GetComponent<InputField>();
        inputPassword = transform.Find("inputPassword").GetComponent<InputField>();
        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();

        btnLogin.onClick.AddListener(loginClick);
        btnClose.onClick.AddListener(closeClick);

        //面板需要默认隐藏
        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        // 移除所有响应函数
        btnLogin.onClick.RemoveListener(loginClick);
        btnClose.onClick.RemoveListener(closeClick);
    }

    /// <summary>
    /// 登录按钮的点击事件处理
    /// </summary>
    private void loginClick()
    {
        // 若用户名输入为空
        if (string.IsNullOrEmpty(inputAccount.text))
        {
            promptMsg.Change("用户名输入为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }  
        // 若密码输入为空
        if (string.IsNullOrEmpty(inputPassword.text))      
        {
            promptMsg.Change("密码输入为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        // 若密码输入不是4到16位
        if(inputPassword.text.Length < 4
            || inputPassword.text.Length > 16)
        {
            promptMsg.Change("密码输入不是4到16位", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }

        //需要和服务器交互了
        AccountDto dto = new AccountDto(inputAccount.text, inputPassword.text);
        socketMsg.Change(OpCode.ACCOUNT, AccountCode.LOGIN, dto);
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    /// <summary>
    /// 关闭按钮点击事件处理
    /// </summary>
    private void closeClick()
    {
        setPanelActive(false);
    }
}
