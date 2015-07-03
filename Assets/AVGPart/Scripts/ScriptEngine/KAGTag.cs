using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sov.AVGPart
{

    /*
     * aKAGCustomKag
     * 用于生成一个自定义的标签
     * 内涵一个处理该标签的方法
     */

    public delegate void CustomTagHandleFunc(KAGWords words);

    public class aKAGCustomTag
    {
        string _tagName;
        CustomTagHandleFunc _handleFunc;

        /*
         * @name Tag名称
         */
        public aKAGCustomTag(string name)
        {
            _tagName = name;
            _handleFunc = (func) =>
                {
                    //默认为空实现
                };
        }
        public void SetHandleFunc(CustomTagHandleFunc func)
        {
            _handleFunc = func;
        }
        public CustomTagHandleFunc getHandleFunc()
        {
            return _handleFunc; 
        }
        
    }

    //
    class KAGTagManager
    {
        int _lastestTagId;
        List<aKAGCustomTag> _registeredCustomTags;
        
        public int GetLastestTagId()
        {
            return _lastestTagId;
        }

        public void RegisterCustomTag(aKAGCustomTag tag)
        {
            _registeredCustomTags.Add(tag);
        }

        public List<aKAGCustomTag> GetCustomTags()
        {
            return _registeredCustomTags;
        }
    }
}
