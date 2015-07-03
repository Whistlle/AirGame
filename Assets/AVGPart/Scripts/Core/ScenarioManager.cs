using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Sov.AVGPart
{
    class ScenarioManager : MonoBehaviour
    {
        static ScenarioManager _sharedScenarioManager = null;
        public static ScenarioManager Instance
        {
            get
            {
                if (_sharedScenarioManager == null)
                {
                    GameObject go = new GameObject("SceneManager");
                    Debug.Log("create SceneManager");
                    if (go == null)
                    {
                        Debug.LogError("SceneManager Create Wrong!");
                    }
                    _sharedScenarioManager = go.AddComponent<ScenarioManager>();
                    _sharedScenarioManager.Init();
                    DontDestroyOnLoad(_sharedScenarioManager);
                }
                return _sharedScenarioManager;
            }
        }
        void Init()
        {
            _loadedScenario = new Dictionary<string, Scene>();
        }

        Dictionary<string, Scene> _loadedScenario;

        public void PrePhraseScenario(Scene s)
        {
            _loadedScenario.Add(s.Name, s);
            s.LoadScript();
            s.PhraseFinish();
        }

        public void PrePhraseScenarioAsync(Scene s)
        {
            //开一个新的线程预加载
            _loadedScenario.Add(s.Name, s);
            s.LoadScriptAsync();
           // s.PhraseFinish();
        }

        public void RunLoadedScenario(string name)
        {
            if (_loadedScenario.ContainsKey(name))
            {
                Debug.LogFormat("Run Scenario: {0}", name);
                ScriptEngine.Instance.Run(_loadedScenario[name]);
            }
            else
            {
               // Debug.LogFormat("Cannot load Scenario:{0}, do not preload?", name);

              //  PrePhraseScenario(new Scene(name));
              //  ScriptEngine.Instance.Run(_loadedScenario[name]);
            }
        }


    }
}
