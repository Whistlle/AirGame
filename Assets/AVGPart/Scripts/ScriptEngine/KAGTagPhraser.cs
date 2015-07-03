using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sov.AVGPart
{
/*
    class KAGTagPhraser
    {
        static public void Phrase(string tagName, KAGWords line, int lineNo)
        {
            TagInfo tagInfo = new TagInfo(tagName);
            foreach(KAGWord param in line)
            {
                tagInfo.Params[param.Name] = param.Value;
            }
            ScriptEngine.Instance.AddCommand(TagFactory.Create(tagInfo, lineNo));
            switch (tagName)
            {
                case "wait": OnWaitTag(line); break;
                case "link": OnLinkTag(line); break;
                case "jump":
                case "layopt":
                case "loadscene":
                case "current": OnCurrentTag(line); break;
                case "anim":
                case "object_":
                case "instantiate":
                case "loadlevel":

                case "sendevent":
                case "image":
                case "condition":
                case "setflag":
                case "destroy":
                case "getuserinput":
                case "definescene":
                case "defineactor":
                case "font":
                case "eval":
                case "if_":
                case "endif":
                case "setpropactor":
                case "enterscene":
                case "enteractor":
                case "addstate":
                case "moveactor":
                case "exitactor":
                case "exitscene":

                case "showsystemui":
                case "hidesystemui":
                case "changestate":
                case "stopbgm":
                case "shake":
                case "playbgm":
                case "playse":
                case "playvoice":
                case "xchgbgm":
                case "fadoutbgm":
                case "trans":
                    OnTriggerEventWithArgsTag(line);
                    break;
                case "settext":
                    OnSetTextTag(line);
                    break;
                default: //custom tag
                    break;
            }
        }
        static void OnTriggerEventWithArgsTag(KAGWords line)
        {
            OpCommand command = new OpCommand(Opcode.MESSAGING);
            foreach(KAGWord word in line)
            {
                if(word.Name == "op")
                {
                    string eventType = "EVENT_";
                    eventType += word.Value.ToUpper();
                    command.Params["eventType"] = eventType;
                }
                else
                {
                    command.Params[word.Name] = word.Value;
                }
            }
            ScriptEngine.Instance.AddCommand(command);
        }

        static void OnSetTextTag(KAGWords line)
        {
            OpCommand command = new OpCommand(Opcode.SET_TEXT);
            foreach(KAGWord word in line)
            {
                if(word.Name == "text")
                {
                    command.Params["text"] = word.Value;
                }
                else if(word.Name == "textbox")
                {
                    command.Params["textbox"] = word.Value;
                }
                else if(word.Name == "op")
                {
                    if(word.Value == "r")
                    {
                        command.AdditionalOp = new List<Opcode> { Opcode.RELINE };
                    }
                }
            }
            ScriptEngine.Instance.AddCommand(command);
        }
        static void OnWaitTag(KAGWords line)
        {
            OpCommand command = new OpCommand(Opcode.WAIT);

            foreach (KAGWord word in line)
            {
                if (word.Name == "time")
                {
                    float milliSec = float.Parse(word.Value);
                    float sec = (float)milliSec / 1000;
                    command.Params["time"] = sec.ToString();
                }
            }
            ScriptEngine.Instance.AddCommand(command);
        }

        static void OnLinkTag(KAGWords line)
        {
            OpCommand command = new OpCommand(Opcode.LINK);
            foreach (KAGWord word in line)
            {
                if (word.Name == "target")
                {
                    command.Params["target"] = word.Value;
                }
                else if (word.Name == "text")
                {
                    command.Params["text"] = word.Value;
                }
                else if (word.Name == "op")
                {
                    if (word.Value == "endlink"
                     && word.Value == "link") //ignore endlink
                    {
                        List<Opcode> tag = KAGPhraser.PhraseMessageTag(word.Value);
                        command.AdditionalOp = tag;
                    }
                }
            }
            ScriptEngine.Instance.AddCommand(command);
        }

        static void OnCurrentTag(KAGWords line)
        {
            OpCommand command = new OpCommand(Opcode.CURRENT);
            foreach(KAGWord word in line)
            {
                if(word.Name == "layer")
                {
                    command.Params["layer"] = word.Value;
                }
            }
            ScriptEngine.Instance.AddCommand(command);
        }


    }*/
       
}
