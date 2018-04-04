using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提示窗口
/// </summary>
class PromptMsg
{
    public string text;
    public Color color;

    public PromptMsg()
    {

    }

    public PromptMsg(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }

    /// <summary>
    /// 改变属性，防止多次 new 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public void Change(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}

