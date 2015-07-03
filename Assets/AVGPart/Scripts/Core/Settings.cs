using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    //挂在场景中用来设置
    class Settings : MonoBehaviour
    {
        //public static string SCENARIO_SCRIPT_PATH   = "/AVGPart/Resources/ScenarioScripts/";
        public static string SCENARIO_SCRIPT_PATH   = "ScenarioScripts/";
        public static string PREFAB_PATH            = "Prefab/";
        public static string UI_IMAGE_PATH          = "UImage/";
        public static string BG_IMAGE_PATH          = "BG/";
        public static string ACTOR_IMAGE_PATH       = "Actor/Image/";

        public static string SCENE_PATH             = "Scene/";

        public static string UI_ROOT_IN_SCENE       = "UICanvas";
        public static string ACTOR_ROOT_IN_SCENE    = "ActorCanvas";
        public static string BG_ROOT_IN_SCENE       = "BGCanvas";

        public static int UI_LAYER = 5;
        public static int ACTOR_LAYER = 9;
        public static int BG_LAYER = 8;

        public static float Actor_Y     = -86f;
        public static float Center_X    = 0f;
        public static float Left_X      = -477f;
        public static float Right_X     = 476f;
        public static float Mid_Left_X  = 0f;
        public static float Mid_Right_X = 0f;
        public static float Far_Z       = 0f;
        public static float Near_Z      = 0f;
        public static float Normal_Z    = 0f;

        public static Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.UI_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find UICanvas, create it in Scene!");
                        return null;
                    }
                    _uiRoot = go.transform;
                }
                return _uiRoot;
            }
        }
        public static Transform _uiRoot = null;

        public static Transform BGRoot
        {
            get
            {
                if (_bgRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.BG_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find BGCanvas, create it in Scene!");
                        return null;
                    }
                    _bgRoot = go.transform;
                }
                return _bgRoot;
            }
        }
        public static Transform _bgRoot = null;

        public static Transform ActorRoot
        {
            get
            {
                if (_actorRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.ACTOR_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find ActorCanvas, create it in Scene!");
                        return null;
                    }
                    _actorRoot = go.transform;
                }
                return _actorRoot;
            }
        }
        public static Transform _actorRoot = null;
    }
}
