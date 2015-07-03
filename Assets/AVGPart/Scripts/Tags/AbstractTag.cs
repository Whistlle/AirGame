using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sov.AVGPart
{
    //还是实现了命令模式= =

    /*
     * AbstractTag
     * 所有的tag都继承于AbstractTag
     * ScriptEngine将一个个执行这些命令
     * 内涵纠错功能，可以发现脚本的语法错误
     */


    public class TagInfo
    {
        public string TagName
        {
            get;
            set;
        }
        public Dictionary<string, string> Params
        {
            get;
            set;
        }
        public TagInfo(string name)
        {
            TagName = name;
            Params = new Dictionary<string, string>();
        }
    }

    public abstract class AbstractTag
    {
        //默认参数列表 相对于Hashtable Dictionary更加类型安全
        //protected Hashtable _defaultParamSet = new Hashtable();
        protected Dictionary<string, string> _defaultParamSet;

        //必要参数列表
        protected List<string> _vitalParams;

        //Params: contains the param write in the script
        public Dictionary<string, string> Params
        {
            get;
            set;
        }


        public AbstractTag()
        {

        }

        public int LineInScript
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return _tagInfo.TagName;
            }
        }
        protected TagInfo _tagInfo;

        public ScriptEngine Engine
        {
            get
            {
                if (_engine == null)
                {
                    _engine = ScriptEngine.Instance;
                }
                return _engine;
            }
            set
            {
                _engine = value;
            }
        }

        ScriptEngine _engine = null;

        public void Init(TagInfo info, int lineNo)
        {
            _tagInfo = info;
            LineInScript = lineNo;

            //set default value
            Params = new Dictionary<string, string>(_defaultParamSet);

            //set script value
            foreach (KeyValuePair<string, string> i in info.Params)
            {
                Params[i.Key] =  i.Value;
            }
        }
        /*
         * 验证此句脚本的语法, 是否包含必须项
         */
        public void CheckVital()
        {
            bool hasError = false;
            List<string> errorParams = new List<string>();

            foreach(string key in _vitalParams)
            {
                if(!Params.ContainsKey(key))
                {
                    errorParams.Add(key);
                    hasError = true;
                }
            }

            if(!hasError)
                return;

            //log the error key
            StringBuilder line = new StringBuilder("[");
            line.Append(_tagInfo.TagName);

            foreach (KeyValuePair<string, string> i in _tagInfo.Params)
            {
                line.Append(" ");
                line.AppendFormat("{0}={0}", i.Key, i.Value);
            }
            Debug.LogFormat("line:{0} {1}\nERROR: Do not contain vital param", LineInScript, line);
            foreach(string p in errorParams)
            {
                Debug.LogFormat("Miss Param:{0}\n", p);
            }
        }

        public virtual void Before()
        {

        }

        public virtual void Excute()
        {

        }

        public virtual void Complete()
        {
            
        }

        public virtual void After()
        {
            Engine.NextCommand();
        }

        public virtual void OnFinishAnimation()
        {

        }
    }

    class TagFactory
    {
        public static AbstractTag Create(TagInfo info, int lineNo)
        {
            /*
             * tagName: 全小写  e.g. settext
             * ClassName 首字母大写 e.g. SettextTag
             */
            StringBuilder className = new StringBuilder(info.TagName);
            className[0] = Char.ToUpper(className[0]);
            className.Append("Tag");
            //className.Append("Sov.AVGPart" + ".", 0);
            string cn = "Sov.AVGPart" + "." + className.ToString();
            Type type = Type.GetType(cn);

            AbstractTag tag = null;
            try
            {
                tag = (AbstractTag)Activator.CreateInstance(type);
            } 
            catch (Exception e)
            {
                //Debug.Log(e.ToString());
            }

            if (tag != null)
            {
                tag.Init(info, lineNo);

                tag.CheckVital();
                
            }
            return tag;
        }

        public static AbstractTag CreateEmptyParamsTag(string className, int lineNo)
        {
            string cn = "Sov.AVGPart" + "." + className.ToString();
            Type type = Type.GetType(cn);
            AbstractTag tag = null;
            try
            {
                tag = (AbstractTag)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                //Debug.Log(e.ToString());
            }


            if (tag != null)
            {
                TagInfo i = new TagInfo("");
                tag.Init(i, lineNo);

                tag.CheckVital();

            }
            return tag;
        }
    }
}
