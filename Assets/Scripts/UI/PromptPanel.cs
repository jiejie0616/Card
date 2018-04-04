using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PromptPanel : UIBase {
    private Text txt;
    private CanvasGroup cg;

    void Awake()
    {
        Bind(UIEvent.PROMPT_MSG);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.PROMPT_MSG:                    // 提示信息
                PromptMsg msg = message as PromptMsg;
                PromptMessage(msg.text, msg.color);
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        txt = transform.Find("Text").GetComponent<Text>();
        cg = transform.Find("Text").GetComponent<CanvasGroup>();
        cg.alpha = 0;
	}

    /// <summary>
    /// 提示消息
    /// </summary>
    /// <param name="text">显示文字</param>
    /// <param name="color">显示颜色</param>
    private void PromptMessage(string text, Color color)
    {
        txt.text = text;
        txt.color = color;
        cg.alpha = 0;
        StartCoroutine(PromptAnim());       // 播放动画
    }

    /// <summary>
    /// 提示消息动画
    /// </summary>
    /// <returns></returns>
    IEnumerator PromptAnim()
    {   
        while (cg.alpha < 1f)                       // 渐渐出现
        {
            cg.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);        // 显示 1s
        while (cg.alpha > 0f)                       // 渐渐消失
        {
            cg.alpha -= Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }
    }
}
