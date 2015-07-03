using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Sov.AVGPart
{
    /*
     * tag = print
     * 
     * <desc>
     * 设置主文本框中的语句
     * 
     * <param>
     * @text:   待显示的文本
     * 
     * <sample>
     * Hello,World[p]
     */
    public class PrintTag : AbstractTag
    {
        public PrintTag()
        {
            _vitalParams = new List<string> {
                "text"
            };

            _defaultParamSet = new Dictionary<string, string> {
                {"text", ""}
            };
        }

        public override void Excute()
        {
            Instances.Instance.TextBoxesManager.PrintText(Params);
            base.Excute();
        }
        public override void After()
        {
            base.After();
        }
    }
    /*
     * tag = settext
     *
     * <desc>
     * 设置文本框内的语句
     * 
     * <param>
     * @text:       待显示的文本
     * @textbox:    文本框名称
     * 
     * <sample>
     * [settext text=Sachi textbox=TextBox_Name]
     */
    public  class SettextTag : AbstractTag
    {
        public SettextTag()
        {
            _vitalParams = new List<string> {
                "text"
            };

            _defaultParamSet = new Dictionary<string,string>() {
                { "text",    ""},
                { "textbox", ""}
            };

        }

        public override void Excute()
        {
            TextBoxesManager.Instance.SetText(Params);
            base.Excute();
        }
    }

    /*
     * tag = setname
     *
     * <desc>
     * 显示人名
     *  
     * <sample>
     * #Sachi
     */
    public class SetnameTag : AbstractTag
    {
        public SetnameTag()
        {
            _vitalParams = new List<string> {
                "text"
            };

            _defaultParamSet = new Dictionary<string, string>() {
                { "text",    ""},
            };
        }

        public override void Excute()
        {
            TextBoxesManager.Instance.SetName(Params["text"]);
            base.Excute();
        }
    }

    /*
     * tag = current
     * 
     * <desc>
     * 切换当前显示的主文本框
     * 
     * <param>
     * @layer:  主文本框名称
     * 
     * <sample>
     * [current layer=TextBox]
     * 
     */
    public class CurrentTag: AbstractTag
    {
        public CurrentTag()
        {
            _vitalParams = new List<string> {
                "layer"
            };

            _defaultParamSet = new Dictionary<string, string>() {
                { "layer",  "TextBox"},
            };
        }

        public override void Excute()
        {
            Instances.Instance.TextBoxesManager.OnCurrent(Params);
         
            base.Excute();
        }
    }
    /*
     * tag = reg_textbox
     * 
     * <desc>
     * 绑定textbox
     * 
     * <param>
     * @objname:     
     * @type:       
     * 
     * <sample>
     * [reg_textbox objname=TextBox_Name type=name]
     * 
     */
    public class Reg_textboxTag: AbstractTag
    {
        public Reg_textboxTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "objname", "" },
                { "type",    "main"}  
            };
            _vitalParams = new List<string>() {
                "objname"
            };
        }

        public override void Excute()
        {
            Debug.Log("[Register TextBox]");
            base.Excute();
            TextBox t = Instances.Instance.TextBoxesManager.GetTextBoxInScene(Params["name"]);
            if(t == null)
            {
                Debug.LogFormat("Can not find TestBox:{0}", Params["name"]);
                return;
            }
            if (Params["type"] == "main")
            {
                Instances.Instance.TextBoxesManager.CurrentMainTextBox = t;
            }
            else if (Params["type"] == "name")
            {
                Instances.Instance.TextBoxesManager.CurrentNameTextBox = t;
            }
        } 
    }

    /* ********************* Message Tag ******************* */
    /*
     * tag = l
     * 
     * <desc>
     * 暂停等待继续
     * 
     * <sample>
     * Hi,World.[l]
     */
    public class LTag: AbstractTag
    {
        public LTag()
        {
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            //Do Nothing
            Debug.Log("[l]");
            base.Excute();
        }
        public override void After()
        {
            Engine.Status.EnableNextCommand = false;
        }
    }

    /*
     * tag = cm
     * 
     * <desc>
     * 清除主文本框中的文字
     * 
     * <sample>
     * [cm]
     * 
     */
    public class CmTag: AbstractTag
    {
        public CmTag()
        {
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            Instances.Instance.TextBoxesManager.ClearMessage(Params);
  
            base.Excute();
        }
    }

    /*
     * tag = p
     * 
     * <desc>
     * 清除当前文本框内容并显示新的文本
     * 
     * <sample>
     * Hello,World[p]
     * 
     */
    public class PTag: AbstractTag
    {
        public PTag()
        {
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();
        }

        public override void Before()
        {
            base.Before();
        //    AbstractTag tag = TagFactory.CreateEmptyParamsTag("LTag", LineInScript);
         //   Engine.InsertCommandBefore(tag);
        //    Engine.SkipThisTag = true;
        }

        public override void Excute()
        {
            Debug.Log("[p]");
            Instances.Instance.TextBoxesManager.ClearMessage(Params);    
            base.Excute();
        }

        public override void After()
        {
            Engine.Status.EnableNextCommand = false;
        }
    }

    /*
     * tag = r
     * 
     * <desc>
     * 在当前文本框显示基础上换行显示新文本
     * 默认为[r]
     * 
     * <sample>
     * Hello,World[r]
     * 
     */
    public class RTag: AbstractTag
    {
        public RTag()
        {
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();
        }

        public override void Before()
        {
            base.Before();
        //    AbstractTag tag = TagFactory.CreateEmptyParamsTag("LTag", LineInScript);
        //    Engine.InsertCommandBefore(tag);
        //    Engine.SkipThisTag = true;
        }

        public override void Excute()
        {
            Debug.Log("[r]");
            Instances.Instance.TextBoxesManager.Reline();
            base.Excute();
        }

        public override void After()
        {
            Engine.Status.EnableNextCommand = false;
        }
    }
    /* ********************* Message Tag ******************* */




}
