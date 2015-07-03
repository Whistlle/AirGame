using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    /*
     * tag = bg_change
     * 
     * <desc>
     * 更改场景中的背景图片
     * 
     * <param>
     * @name:       图片名称
     * @objname:    GameObject的name, 全部小写
     * @path:       Settings.BK_IMAGE_PATH下的相对路径
     * @fade:       是否渐变显示
     * @fadetime:   渐变时间
     * 
     * <sample>
     * [bg_change name=background path=room_tall fade=true]
     *
     */
    class Bg_changeTag : Image_changeTag
    {
        public Bg_changeTag() : base()
        {
            _vitalParams = new List<string>() {
                "name",
            };
        }
        public override void Excute()
        {
            Debug.Log("[bg_change]");
            //之前填入参数 参数都有且为默认值
            if (Params["path"] != "")
            {
                string s = Params["path"];
                Params["path"] = Settings.BG_IMAGE_PATH + s;
            }
            else
            {
                Params["path"] = Settings.BG_IMAGE_PATH;
            }

            if (Params["objname"] == "")
            {
                //默认为 backgorund
                Params["objname"] = "Background";
            }
            else
            {
                Params["objname"] = StringUtil.ToTitleCapital(Params["objname"]);
            }
            base.Excute();
        }
        
    }
}
