using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Sov.AVGPart
{
    class KAGPhraser
    {
        
        public KAGPhraser()
        {
            _tokenizer = new Tokenizer();
            _uploadTags = new List<AbstractTag>();
           // _tagManager = new KAGTagManager();
        }


        int _currentPhraseLineNo = 0;

        List<AbstractTag> _uploadTags;
        
        string _scriptStream;
        
       // List<KAGWords> _phrasedLines;
        
        Tokenizer _tokenizer;
        Scene _scenario;
        public List<AbstractTag> Phrase(Scene s)
        {
            //reset
            _currentPhraseLineNo = 0;
            _uploadTags.Clear();

            _scriptStream = s.ScriptContent;

            _scenario = s;

            Phrase();
            return _uploadTags;
        }

              
        /*
        static public List<Opcode> PhraseMessageTag(string tag)
        {
            List<Opcode> ops = new List<Opcode>();
            switch (tag)
            {
                case "s":

                case "cm":
                    break;
                case "er":
                    ops.Add(Opcode.PAGE);
                    break;
                case "l":
                    ops.Add(Opcode.WAIT_TOUCH);
                    break;
                case "p":            
                    ops.Add(Opcode.WAIT_TOUCH);
                    ops.Add(Opcode.PAGE);
                    break;
                case "r":             
                    ops.Add(Opcode.WAIT_TOUCH);
                    ops.Add(Opcode.RELINE);
                    break;
                case "endlink":
                case "hidemessage":
                default:
                    break;
            }
            return ops;
        
        }*/
        /*
        public void SetScript(string String)
        {
            _scriptStream = String;
        }*/


        #region PrivateMethod
        void Phrase()
        {

            string[] list;
            list = _scriptStream.Split(new Char[] { '\n' }, StringSplitOptions.None);

            foreach (string line in list)
            {
                string str = line.Trim();
                if (str == "")
                {
                    _currentPhraseLineNo++;
                    continue;
                }

                KAGWords l = _tokenizer.GetToken(str);
                if (l != null)
                {
                    PhraseALine(l);
                }
                _currentPhraseLineNo++;
            }
        }

        void PhraseALine(KAGWords line)
        {
            KAGWord op = line[0];
            if (op.WordType == KAGWord.Type.TEXT)
            {
                PhraseText(line);
                
            }
            else if(op.WordType == KAGWord.Type.NAME)
            {
                PhraseName(line);
            }
            else
            {
                TagInfo tagInfo = new TagInfo(op.Value.ToLower());
                foreach (KAGWord param in line)
                {
                    if (op != param)
                    {
                        tagInfo.Params[param.Name] = param.Value;
                    }
                }
                CreateAndSendTagToEngine(tagInfo);
            }
        }

        /*
        void PhraseScenario(KAGWords line)
        {

            OpCommand command = new OpCommand(Opcode.SCENARIO);

            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "scenario")
                {
                    command.Params["scenario"] = word.Value;
                }
            }

            ScriptEngine.Instance.AddCommand(command);
        }

        void PhraseScenario(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("scenario");
            foreach (KAGWord param in line)
            {
                tagInfo.Params[param.Name] = param.Value;
            }
            ScriptEngine.Instance.AddCommand(TagFactory.Create(tagInfo));
        }*/
        void PhraseText(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("print");
            
            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "text")
                {
                    tagInfo.Params["text"] = word.Value;
                    CreateAndSendTagToEngine(tagInfo);
                }
                else if (name == "op")
                {
                    TagInfo tagInfo1 = new TagInfo(word.Value.ToLower());
                    CreateAndSendTagToEngine(tagInfo1);
                }
            }
        }
        
        void PhraseName(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("setname");

            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "text")
                {
                    tagInfo.Params["text"] = word.Value;
                    CreateAndSendTagToEngine(tagInfo);
                }
            }
        }
        void CreateAndSendTagToEngine(TagInfo tagInfo)
        {
            AbstractTag tag = TagFactory.Create(tagInfo, _currentPhraseLineNo);
            if (tag != null)
            {
               // ScriptEngine.Instance.AddCommand(tag);
               //_uploadTags.Add(tag);
                _scenario.AddCommand(tag);
            }
            else
                Debug.LogFormat("Tag:{0} is not implemented!", tagInfo.TagName);
        }
        #endregion

        
     //   KAGTagManager _tagManager;
    }
}
