using Protocol.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : UIBase
{

    private void Awake()
    {
        Bind(UIEvent.SHOW_ENTER_ROOM_BUTTON);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SHOW_ENTER_ROOM_BUTTON:
                btnEnter.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private Button btnMatch;
    private Image imgBg;
    private Text txtDes;
    private Button btnCancel;
    private Button btnEnter;

    private SocketMsg socketMsg;

    // Use this for initialization
    void Start()
    {
        btnMatch = transform.Find("btnMatch").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        txtDes = transform.Find("txtDes").GetComponent<Text>();
        btnCancel = transform.Find("btnCancel").GetComponent<Button>();
        btnEnter = transform.Find("btnEnter").GetComponent<Button>();

        btnMatch.onClick.AddListener(matchClick);
        btnCancel.onClick.AddListener(cancelClick);
        btnEnter.onClick.AddListener(enterClick);

        socketMsg = new SocketMsg();

        //默认状态
        setObjectsActive(false);
        btnEnter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (txtDes.gameObject.activeInHierarchy == false)
            return;

        timer += Time.deltaTime;
        if (timer >= intervalTime)
        {
            doAnimation();
            timer = 0f;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnMatch.onClick.RemoveListener(matchClick);
        btnCancel.onClick.RemoveListener(cancelClick);
        btnEnter.onClick.RemoveListener(enterClick);
    }

    private void matchClick()
    {
        setObjectsActive(true);
        // 向服务器发起开始匹配的请求
        socketMsg.Change(OpCode.MATCH, MatchCode.ENTER_CREQ, null);
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    private void cancelClick()
    {
        setObjectsActive(false);
        btnEnter.gameObject.SetActive(false);
        // 向服务器发起离开匹配的请求
        socketMsg.Change(OpCode.MATCH, MatchCode.LEAVE_CREQ, null);
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    /// <summary>
    /// 点击进入房间按钮
    /// </summary>
    private void enterClick()
    {
        LoadSceneMsg msg = new LoadSceneMsg(2,
                    delegate()          // 匿名委托
                    {
                        // TODO 加载游戏信息
                        Debug.Log("场景加载完成！");
                    }
                    );
        Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, msg);
    }

    /// <summary>
    /// 控制点击匹配按钮之后的显示的物体
    /// </summary>
    private void setObjectsActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        txtDes.gameObject.SetActive(active);
        btnCancel.gameObject.SetActive(active);
    }

    private string defaultText = "正在寻找房间";
    //点的数量
    private int dotCount = 0;
    //动画的间隔时间
    private float intervalTime = 1f;
    //计时器
    private float timer = 0f;

    /// <summary>
    /// 做动画
    /// </summary>
    private void doAnimation()
    {
        txtDes.text = defaultText;

        //点增加
        dotCount++;
        if (dotCount > 5)
            dotCount = 1;

        for (int i = 0; i < dotCount; i++)
        {
            txtDes.text += ".";
        }
    }

}
