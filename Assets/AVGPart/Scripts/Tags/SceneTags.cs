using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Sov.AVGPart
{
    /*
     * tag = preload_scene
     * 
     * <desc>
     * 预加载场景而不切换，由@loadscene手动切换
     * 
     * <params>
     * @name:       场景名称
     * @path:       场景存放路径，默认为"Resources/Scene/"下
     * 
     * <sample>
     * [preload_scene name=game]
     *
     */
    class Preload_sceneTag: AbstractTag
    {
        public Preload_sceneTag()
        {
            _vitalParams = new List<string>() {
                "name"
            };

            _defaultParamSet = new Dictionary<string, string>() {
                {"name", "" },
                {"path", "" }
            };

        }

        public override void Excute()
        {
            //base.Excute();
            Debug.LogFormat("[Preload scene]:{0}", Params["name"]);
            SceneManager.Instance.PreLoadScene(Params["name"]);
        }
    }

    /*
     * tag = preload_scene
     * 
     * <desc>
     * 预加载场景而不切换，由@loadscene手动切换
     * 
     * <params>
     * @name:       场景名称
     * @path:       场景存放路径，默认为"Resources/Scene/"下
     * 
     * <sample>
     * [preload_scene name=game]
     *
     */
    class LoadsceneTag: AbstractTag
    {
        public LoadsceneTag()
        {
            _vitalParams = new List<string>() {
                "name"
            };

            _defaultParamSet = new Dictionary<string, string>() {
                {"name", "" },
                {"path", "" }
            };

        }

        public override void Excute()
        {
            //base.Excute();
            Debug.LogFormat("[Load scene]:{0}", Params["name"]);
            SceneManager.Instance.LoadScene(Params["name"]);
            Engine.Status.EnableNextCommand = false;
        }
        public override void After()
        {
            //base.After();
        }
    }

    /*
    * tag = enterscene
    * 
    * <desc>
    * 渐变进入场景
    * 
    * <params>
    * @fade:       是否允许渐变
    * @fadetime:   渐变时间
    * 
    * <sample>
    * [enterscene fadetime=2]
    *
    */
    class EntersceneTag: AbstractTag
    {
        public EntersceneTag()
        {
            _vitalParams = new List<string>()
            {

            };
            _defaultParamSet = new Dictionary<string, string>() {
                {"fadetime", "0"}
            };
        }

        public override void Excute()
        {
            //base.Excute();
            Transform UICanvas = Settings.UIRoot;
            Transform BGCanvas = Settings.BGRoot;
            
            Debug.Log("[EnterScene]");

            Engine.Status.EnableNextCommand = false;

            var ui = UICanvas.GetComponent<CanvasGroup>().DOFade(1, float.Parse(Params["fadetime"]));
            var bg = BGCanvas.GetComponent<CanvasGroup>().DOFade(1, float.Parse(Params["fadetime"]));

            ImageManager.Instance.ShowBackground(0.0f);
            bg.OnComplete(new TweenCallback(OnFinishAnimation));
        }
        public override void OnFinishAnimation()
        {
            //base.Complete();
            Engine.Status.EnableNextCommand = true;
            Engine.NextCommand();
        }
        public override void After()
        {
            //DO Nothing, Wait for the animation finish 
            //base.After();
        }
    }
}