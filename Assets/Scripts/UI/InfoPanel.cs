using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIBase {
    private Text txtName;
    private Text txtLv;
    private Slider sldExp;
    private Text txtExp;
    private Text txtBeen;

    void Awake()
    {
        Bind(UIEvent.REFRESH_INFO_PANEL);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_INFO_PANEL:
                // 刷新玩家信息窗口
                UserDto user = message as UserDto;
                RefreshPanel(user.Name, user.Lv, user.Exp, user.Been);
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        txtName = transform.Find("txtName").GetComponent<Text>();
        txtLv = transform.Find("txtLv").GetComponent<Text>();
        sldExp = transform.Find("sldExp").GetComponent<Slider>();
        txtExp = transform.Find("txtExp").GetComponent<Text>();
        txtBeen = transform.Find("txtBeen").GetComponent<Text>();
	}

    private void RefreshPanel(string name, int lv, int exp, int been)
    {
        txtName.text = name;
        txtLv.text = "Lv." + lv;
        sldExp.value = (float)exp / (lv * 100);
        txtExp.text = exp + "/" + (lv * 100);
        txtBeen.text = "×" + been;
    }
}
