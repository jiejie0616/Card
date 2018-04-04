using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SceneMgr : ManagerBase
{
    public static SceneMgr Instance = null;

    void Awake()
    {
        Instance = this;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        Add(SceneEvent.LOAD_SCENE, this);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case SceneEvent.LOAD_SCENE:
                LoadSceneMsg msg = message as LoadSceneMsg;
                LoadScene(msg);
                break;
            default:
                break;
        }
    }

    private SceneManager sceneManager;
    private Action OnSceneLoaded = null;

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadScene(LoadSceneMsg msg)
    {
        if(msg.SceneBuildIndex != -1)
            SceneManager.LoadScene(msg.SceneBuildIndex);
        if(msg.SceneName != null)
            SceneManager.LoadScene(msg.SceneName);
        if (msg.OnSceneLoaded != null)
            OnSceneLoaded = msg.OnSceneLoaded;
    }

    /// <summary>
    /// 当场景加载完成时调用
    /// </summary>
    /// <param name="scene">场景名字</param>
    /// <param name="loadSceneMode">加载场景的方式</param>
    void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (OnSceneLoaded != null)
            OnSceneLoaded();
    }
}
