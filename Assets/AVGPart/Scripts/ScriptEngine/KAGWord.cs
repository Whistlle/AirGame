using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sov.AVGPart
{

    //typedef List<KAGWord> KAGWords
    public class KAGWords: List<KAGWord>
    {

    }
    
    public class KAGWord
    {
        public enum Type
        {
            TAG,
            ATTRIBUTE,
            TEXT,
            SCENARIO,
            NAME
        }

        public KAGWord(Type type, string attribute, string value)
        {
            WordType = type;
            Name = attribute;
            Value = value;
        }

        public Type     WordType    { get; set;}
        public string   Name        { get; set;}
        public string   Value       { get; set;}

    }
}
