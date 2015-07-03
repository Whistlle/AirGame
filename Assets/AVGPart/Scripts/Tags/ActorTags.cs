using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    /*
     * tag = actor_new
     * 
     * <desc>
     * 预创建新的立绘, 默认为不激活状态
     * 
     * <params>
     * @name:       立绘的文件名，是一个prefab
     * @objname:    创建的GameObject的名称，默认为文件名
     * @path:       立绘存放路径，"Resources/Actor/Image"下的相对路径
     * @pos:        立绘显示的水平位置, 是一个enum, 有以下五种：
     *              {center, left, right, mid_left, mid_right}
     * @z_pos:      立绘显示的纵向位置，及近和远
     *              {far, near, normal}
     * @scale:      立绘的扩大倍数，1为原始
     * 
     * <sample>
     * [actor_new name=Sachi position=center]
     *
     */

    public class Actor_newTag: AbstractTag 
    {
        public Actor_newTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "objname", ""         },
                { "name",    ""         },
                { "path",    ""         },
              //{ "x",       "0"        },
              //{ "y",       "0"        },
              //{ "z",       "0"        },
              //{ "show",    "false"    },
              //{ "fade",    "false"    },
              //{ "fadetime","0"        },
              //{ "pos",     "center"   },
              //{ "z_pos",   "normal"   },
                { "scale",   "1"        }
            };

            _vitalParams = new List<string>() {
                "name",
                "path"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("Create Actor: {0}", Params["name"]);
            //set objname
            if(Params["objname"] == "")
            {
                Params["objname"] = Params["name"];
            }

            //set path
            string path = Params["path"];
            path = Settings.ACTOR_IMAGE_PATH + path;
            Params["path"] = path;

            //set position
            /*
            Vector3 pos = GetActorPosition(Params["pos"], Params["z_pos"]);
            Params["x"] = pos.x.ToString();
            Params["y"] = pos.y.ToString();
            Params["z"] = pos.z.ToString();
            */
            
            ImageInfo info = new ImageInfo(Params);
            //set position
            info.Position = new Vector3(0, 0, 0);

            ActorObject ao = ImageManager.Instance.CreateObject<ActorObject, ImageInfo>(info);

            //base.Excute();
        }


    }

    /* 
     * tag = enteractor
     * 
     * <desc>
     * 立绘出场
     * 
     * <params>
     * @name:       the name of the actor
     * @pos:        立绘显示的水平位置, 是一个enum, 有以下五种, 默认为center
     *              {center, left, right, mid_left, mid_right}
     * @z_pos:      立绘显示的纵向位置，及近和远, 默认为normal
     *              {far, near, normal}   
     * @fade:       是否淡入
     * @fadetime:   淡入时间
     * 
     * <sample>
     * [enteractor name=Sachi position=center fade=true]
     *
     */
    public class EnteractorTag: AbstractTag
    {
        public EnteractorTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "name",    ""         },
                { "fade",    "false"    },
                { "fadetime","0.5"      },
                { "pos",     "center"   },
                { "z_pos",   "normal"   },
                { "scale",   "1"        }
            };

            _vitalParams = new List<string>() {
                "name",
            };
        }

        public override void Excute()
        {
            //base.Excute();

            //actor name
            string actorName = Params["name"];

            Debug.LogFormat("Enter Actor: {0}", actorName);

            //get actor
            ActorObject ao = ImageManager.Instance.GetCreatedObject<ActorObject>(actorName);
            if (ao == default(ActorObject))
                return;

            ao.Go.SetActive(true);

            Vector3 pos = GetActorPosition(Params["pos"], Params["z_pos"]);
            ao.SetPosition3D(pos);
            ao.OnAnimationFinish = OnFinishAnimation;

            float time = float.Parse(Params["fadetime"]);

            
            if(Params["fade"] == "true")
            {
                Engine.Status.EnableNextCommand = false;
                ao.FadeIn(time);
            }
            else
            {               
                ao.FadeIn(0);
            }
        }

        Vector3 GetActorPosition(string positionX, string positionZ)
        {
            float y = Settings.Actor_Y;
            float z = Settings.Normal_Z;
            switch (positionZ)
            {
                case "Far":
                    z = Settings.Far_Z;
                    break;
                case "Near":
                    z = Settings.Near_Z;
                    break;
                case "Normal":
                default:
                    z = Settings.Normal_Z;
                    break;
            }

            switch (positionX)
            {
                case "center":
                    return new Vector3(Settings.Center_X, y, z);
                case "left":
                    return new Vector3(Settings.Left_X, y, z);
                case "right":
                    return new Vector3(Settings.Right_X, y, z);
                case "mid_left":
                    return new Vector3(Settings.Mid_Left_X, y, z);
                case "mid_right":
                    return new Vector3(Settings.Mid_Right_X, y, z);
                default: //center by default
                    return new Vector3(Settings.Center_X, y, z);
            }
        }

        public override void OnFinishAnimation()
        {
            if (Params["fade"] == "true")
            {
                Debug.Log("Finish Animation!");
                Engine.Status.EnableNextCommand = true;
                Engine.NextCommand();
            }
        }

        public override void After()
        {
            //base.After();
        }
    }
}
