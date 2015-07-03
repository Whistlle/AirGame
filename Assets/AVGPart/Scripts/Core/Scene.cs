using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Sov.AVGPart
{
    /*
     * 一个场景类
     * 一个游戏中可以有多个场景但是运行中的只有一个
     * 压栈
     */

    public class Scene
    {
        public class SceneStatus
        {
            public bool SkipThisTag
            {
                get;
                internal set;
            }

            public bool EnableClickContinue
            {
                get;
                internal set;
            }

            public bool EnableNextCommand
            {
                get;
                internal set;
            }

            public SceneStatus()
            {
                Reset();
            }

            internal void Reset()
            {
                //Status reset
                EnableClickContinue = true;
                EnableNextCommand = true;
                SkipThisTag = false;
            }

        }

        public SceneStatus Status;

        public string Name
        {
            get;
            set;
        }
        public List<AbstractTag> Tags
        {
            get;
            set;
        }

        public int CurrentLine
        {
            get;
            set;
        }

        public string CurrentScenario
        {
            get;
            private set;
        }

        public string ScriptFilePath
        {
            get;
            set;
        }

        public string ScriptContent;

        public Dictionary<string, int> ScenarioDict
        {
            get;
            private set;
        }

       // public Action PhraseFinishCalback;

        public bool IsPhraseFinish
        {
            get;
            set;
        }
        ScriptEngine _engine;


        public Scene(string scriptPath):
            this() //init
        {
            ScriptFilePath = scriptPath;
            Name = scriptPath;
        }
        
        public void PhraseFinish()
        {
            Debug.LogFormat("Phrase Before IsPhraseFinish={0}", IsPhraseFinish);
            IsPhraseFinish = true;
            Debug.LogFormat("Phrase After IsPhraseFinish={0}", IsPhraseFinish);
        }

        //some init
        public Scene()
        {
            Tags = new List<AbstractTag>();
            Status = new SceneStatus();
            ScriptFilePath = "";
            ScriptContent = "";
            CurrentLine = 0;

            ScenarioDict = new Dictionary<string, int>();
            _engine = ScriptEngine.Instance;
            IsPhraseFinish = false;
        }


        public void LoadScript()
        {
            string path = Settings.SCENARIO_SCRIPT_PATH + ScriptFilePath;

#if UNITY_STANDALONE_WIN          
            if (!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }else
                Debug.LogFormat("load script file: {0}!", path);
            StreamReader sr = File.OpenText(path);
            ScriptContent = sr.ReadToEnd();
            sr.Close();
#endif

#if UNITY_ANDROID
            TextAsset t = Resources.Load<TextAsset>(path);
            if (t == null)
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }
            else
                Debug.LogFormat("load script file: {0}!", path);
            ScriptContent = t.text;
#endif
            //_phraser.SetScript(str);
            _engine.Phrase(this);
        }

        public void LoadScriptAsync()
        {
            string path = Settings.SCENARIO_SCRIPT_PATH + ScriptFilePath;

#if UNITY_STANDALONE_WIN          
            if (!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }else
                Debug.LogFormat("load script file: {0}!", path);
            StreamReader sr = File.OpenText(path);
            ScriptContent = sr.ReadToEnd();
            sr.Close();
#endif

#if UNITY_ANDROID
            TextAsset t = Resources.Load<TextAsset>(path);
            if (t == null)
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }
            else
                Debug.LogFormat("load script file: {0}!", path);
            ScriptContent = t.text;
#endif
            //_phraser.SetScript(str);
            Thread thread = new Thread(
                () =>
                {
                    _engine.Phrase(this);
                    PhraseFinish();
                });
            thread.Start();
        }
        public void AddCommand(AbstractTag tag)
        {
            //tag.LineNo = _opTags.Count;
            tag.Engine = _engine;
            if (tag.Name == "scenario")
            {
                AddScenario(tag);
            }
            else
                Tags.Add(tag);
        }


        private void AddScenario(AbstractTag tag)
        {
            string scenarioName = tag.Params["scenario"];

            if (ScenarioDict.ContainsKey(scenarioName))
            {
                Debug.LogFormat("Scenario: {0}Is Already Exist", scenarioName);
                return;
            }
            else
            {
                ScenarioDict.Add(scenarioName, GetLastedTagLineNo());
                Debug.LogFormat("[Add Scenario]{0}:{1}", GetLastedTagLineNo(), scenarioName);
                CurrentScenario = scenarioName;
            }
        }

        int GetLastedTagLineNo()
        {
            return Tags.Count;
        }

        
    }
}
