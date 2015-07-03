using UnityEngine;
using System.Collections;

namespace Sov.AVGPart
{
    public class Loading : MonoBehaviour
    {
        AsyncOperation async = null;
        public string NextScene;

        public string ScenarioName;
        public string ScenarioFileName;

        Scene s = null;

        // Use this for initialization
        void Start()
        {
            ScenarioName = "story";
            ScenarioFileName = "story1";
            NextScene = "loadingStory";
            // ScriptEngine.Instance.LoadScript(ScriptFileName);
            s = new Scene(ScenarioFileName);
            s.Name = ScenarioName;
            ScenarioManager.Instance.PrePhraseScenarioAsync(s);
            StartCoroutine(LoadA());
        }

        IEnumerator LoadA()
        {
            async = Application.LoadLevelAsync(NextScene);
            async.allowSceneActivation = false;
            yield return async;
                   
        }

        void Update()
        {
            if(s != null && s.IsPhraseFinish)
            {
                async.allowSceneActivation = true;  
            }
        }
    }
}

