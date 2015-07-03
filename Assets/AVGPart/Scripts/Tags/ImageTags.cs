using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    /*
     * tag = image_change
     * 
     * <desc>
     * 更换GameObject的图片
     * 
     * <param>
     * @name:       图片名称
     * @objname:    GameObject的name, 全部小写
     * @path:       文件在"Resources/"下的相对路径
     * @fade:       是否渐变显示
     * @fadetime:   渐变时间
     * 
     * <sample>
     * [image_change name=background path=room_tall fade=true]
     */

    class Image_changeTag: AbstractTag
    {
        public Image_changeTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "objname",    ""},
                { "name",       "" },
                { "path",    "" },
                { "fade",       "false" },
                { "fadetime",   "0" } 
            };
            _vitalParams = new List<string>() {
                "name",
                "objname",
                "path"
            };
        }

        public override void Excute()
        {
            string objName = Params["objname"];
            string path = Params["path"] + Params["name"];
            Engine.Status.EnableNextCommand = false;

            //ImageObject io = ImageManager.Instance.GetImageObjectInScene(objName);
            //ImageObject io = ImageManager.Instance.GetObjectInScene<ImageObject>(objName);
            ImageObject io = ImageManager.Instance.GetObjectInScene<ImageObject>(objName);
            io.OnAnimationFinish = OnFinishAnimation;
            if (Params["fade"] == "true")
            {
                //等待动画结束函数回调继续执行
                Engine.Status.EnableNextCommand = false;
            }
            else
            {
                Engine.Status.EnableNextCommand = false;
            }
            io.ChangeImage(path,
                    float.Parse(Params["fadetime"]));

            base.Excute();

        }
        public override void After()
        {
            //base.After();
        }
        public override void OnFinishAnimation()
        {
           // if (Params["fade"] == "true")
          //  {
                Debug.Log("Finish Animation!");
                Engine.Status.EnableNextCommand = true;
                Engine.NextCommand();
           // }
        }
    }

    /*
     * tag = image_new
     * 
     * <desc>
     * 预创建新的图片
     * 
     * <param>
     * @objname:    GameObject Name
     * @name:       File name
     * @path:       Relative Path To the "/Resources/"
     * @x,y,z:      position of the image to set
     * //@show:       show immediately?
     * 
     * <sample>
     * [image_new name=sachi path=actor/]
     */
    public class Image_newTag: AbstractTag
    {
        public Image_newTag()
        {
            _defaultParamSet = new Dictionary<string,string>() {
                { "objname", "new_image"},
                { "name",    ""         },
                { "path",    ""         },
                { "x",       "0"        },
                { "y",       "0"        },
                { "z",       "0"        },
                { "scale",   "1"        },
               // { "show",    "false"    },
               // { "fade",    "false"    },
               // { "fadetime","0"        },
            };

            _vitalParams = new List<string>() {
                "name",
                "path"
            };
        }

        public override void Excute()
        {
            ImageInfo info = new ImageInfo(Params);
            //ImageObject io = ImageManager.Instance.CreateImage(info);
            //ImageObject io = ImageManager.Instance.CreateObject<ImageObject, ImageInfo>(info);
            ImageObject io = ImageManager.Instance.CreateObject<ImageObject, ImageInfo>(info);
           // Instances.Instance.ImageManager.CreateImage()
            base.Excute();
        }
    }

}
