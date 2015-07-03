using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sov.AVGPart
{
    /*
     * 创建/加载场景(Unity)与脚本场景
     */
    class SceneManager : MonoBehaviour
    {

        static SceneManager _sharedSceneManager = null;
        public static SceneManager Instance
        {
            get
            {
                if (_sharedSceneManager == null)
                {
                    GameObject go = new GameObject("SceneManager");
                    Debug.Log("create SceneManager");
                    if (go == null)
                    {
                        Debug.LogError("SceneManager Create Wrong!");
                    }
                    _sharedSceneManager = go.AddComponent<SceneManager>();
                    _sharedSceneManager.Init();
                }
                return _sharedSceneManager;
            }
        }

        void Init()
        {
            _asyncLoadScene = new Dictionary<string, AsyncOperation>();
        }

        Dictionary<string, AsyncOperation> _asyncLoadScene;

        public void PreLoadScene(string name)
        {
            //StartCoroutine(PreLoadingScene(name));
            AsyncOperation op = Application.LoadLevelAsync(name);
            _asyncLoadScene.Add(name, op);
            op.allowSceneActivation = false;
        }
        //加载到90%时停止
        IEnumerator PreLoadingScene(string scene)
        {            
            AsyncOperation op = Application.LoadLevelAsync(scene);

            _asyncLoadScene.Add(name, op);

            op.allowSceneActivation = false;

            while (op.progress < 1.0f)
            {
                if(op.progress >=0.9f)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }

            op.allowSceneActivation = false;
        }

        public void LoadScene(string scene)
        {
            AsyncOperation op = _asyncLoadScene[scene];
            op.allowSceneActivation = true;
            //StartCoroutine(LoadSceneLoaded(scene));
        }

        IEnumerator LoadSceneLoaded(string scene)
        {
            AsyncOperation op = _asyncLoadScene[scene];
            op.allowSceneActivation = true;
            while(!op.isDone)
                yield return op;
        }
    }
}
