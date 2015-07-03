using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace Sov.AVGPart
{
    /*
     * tag = scenario
     * 
     * <desc>
     *  记录以下内容为一个新片段，供跳转等实现
     *  
     * <sample>
     * *Demonstration(English)/START/1_Dialog
     * 
     */
    public class ScenarioTag: AbstractTag
    {
        public ScenarioTag()
        {
            _vitalParams = new List<string>() {
                "scenario"
            };

            _defaultParamSet = new Dictionary<string, string>() {
                { "scenario", "" }
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("[Scenario]: {0}", Params["scenario"]);
            //Done By ScriptEngine
            /*
            if(Params.ContainsKey("scenario"))
            {
                string scenarioName = Params["scenario"];
                Engine.AddScenario(scenarioName);
            }*/
             
            base.Excute();
        }
    }

    /*
     * tag = s
     * 
     * <desc>
     * 脚本运行到此处时停止
     * 
     * <sample>
     * 
     * [select num = 2]
     * [select_new  target=*select_a1]View from the beginning.[end]
     * [select_new  target=*select_a2]Finish this demo.[end]
     * [s]
     * 
     */
    public class STag: AbstractTag
    {
        public STag()
        {
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();

        }

        public override void Excute()
        {
            base.Excute();

            Engine.Status.EnableClickContinue   = false;
            Engine.Status.EnableNextCommand     = false;        
        }
    }

    /*
     * tag = select_new
     * 
     * <desc>
     * 显示脚本前面[select_new]的选择肢
     * 
     * <sample>
     * [select_show]
     * 
     */

    public class Select_showTag: AbstractTag
    {
        public Select_showTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
            };

            _vitalParams = new List<string>() {
            };
        }

        public override void Excute()
        {
            Instances.Instance.UIManager.ShowSelects();
            base.Excute();
        }
    }

    /*
     * tag = select_new
     * 
     * <desc>
     * 创建新的选择肢
     * 
     * <param>
     * @target: 点击后跳转的Scenario标签
     * @text:   标签上显示的文字
     * 
     * <sample>
     * [select_new  target=*select_a1]Nico~[end]
     * 
     */
    public class Select_newTag: AbstractTag
    {
        public Select_newTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
                {"target", ""},
                {"text",   ""}
            };

            _vitalParams = new List<string>() {
                "target"
            };
        }

        public override void Excute()
        {
            string target = Params["target"];
            Instances.Instance.UIManager.AddSelect("button", Params["text"]
                      , () =>
                      {
                          Engine.JumpToScenario(target);
                          Instances.Instance.UIManager.ClearSelects();
                      }
                                                  );
            base.Excute();
        }

    }

    /*
     * tag = jump
     * 
     * <desc>
     * 跳转到Param["target"]的Scenario处
     * 
     * <param>
     * @target: Target scenario to jump
     * 
     * <sample>
     * *select_niko
     * [jump target=*select_niko]
     * 
     */

    public class JumpTag : AbstractTag
    {
        public JumpTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
                {"target", ""},
            };

            _vitalParams = new List<string>() {
                "target"
            };
        }
        public override void Excute()
        {
            base.Excute();
        }
        public override void After()
        {
            Engine.JumpToScenario(Params["target"]);
            //Do Not CurrentLine + 1
            //base.After();
        }
    }

    /*
     * tag = wait
     * 
     * <desc>
     * 暂停指定时间后继续
     * 
     * <param>
     * @time:   需要等待的时间，单位为秒
     * 
     * <sample>
     * [wait time=1000]
     * 
     */
    public class WaitTag: AbstractTag
    {
        public WaitTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "time", "0"}
            };

            _vitalParams = new List<string>() {
                "time"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("Wait: {0}ms", Params["time"]);
            Engine.Status.EnableNextCommand = false;
            Engine.StartCoroutine(Util.DelayToInvoke(this.OnFinishAnimation, int.Parse(Params["time"]) / 1000f));
        }

        public override void After()
        {
            //Do Nothing
        }

        public override void OnFinishAnimation()
        {
            Debug.Log("Wait Finish!");
            Engine.Status.EnableNextCommand = true;
            Engine.NextCommand();
        }
    }



}
