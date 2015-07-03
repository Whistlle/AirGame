using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * phrase the string into words
 * return words
 *
 * there are three kind of lines
 * Scenario: *Demonstration(English)
 * Tag:		 [enterscene name=Schoolgate destroy=true fade=true]
 * Text:	 Nice to meet you.[p]
 * 
 * see [p][r] as a tag 
 * 
 * add:
 * · use '@' to set tag. Now you can use @ or [] to set tag
 *   e.g. @enterscene name=Schoolgate destroy=true fade=true
 * · use '//' to set commit
 * · use '#' to print name text conveniently. instead of [settext]
 *   e.g. #Sachi 
 */

#if UNIT_TEST
public  class Debug
{
public static void Log(string s) { Console.WriteLine(s); }
public static void LogWarning(string s) { Console.WriteLine(s); }
public static void LogError(string s) { Console.WriteLine(s); }
}
#endif
namespace Sov.AVGPart
{

    public class Tokenizer
    {
        #region Members

        private Mode _mode;
        private int _pos;
        private string _line;
        private KAGWords _words;
        private string _command;

        #endregion

        #region PublicMethods

        public Tokenizer()
        {
            _words = new KAGWords();
        }

        public KAGWords GetToken(string str)
        {
            Init(str);

            _pos = 0;

            //phrase scenario previously
            if (_line[_pos] == '*')
            {
                ReadScenario();
                return _words;
            }
            else if(_line[_pos] == '#')
            {
              //  ++_pos;
                ReadName();
                return _words;
            }
            else if(_line[0] == '/' && _line[1] == '/')
            {
                //Commit, Do Nothing
                return null;
            }

            while (true)
            {
                if (_pos >= _line.Length)
                {
                    break;
                }
                else if (_line[_pos] == '[' || _line[_pos] == '@')
                {
                    ChangeReadMode(Mode.TAG);
                    ++_pos;
                }
                else if (_line[_pos] == ']') //ignore '['
                {
                    ChangeReadMode(Mode.GLOBAL);
                    ++_pos;
                }

                switch (_mode)
                {
                    case Mode.GLOBAL:
                        ReadText();
                        break;
                    case Mode.TAG:
                        ReadTag();
                        break;
                    case Mode.ATTRIBUTE:
                        ReadAttribute();
                        break;
                    default:
                   // Debug.Log("WTF! another read mode?");
                    break;
                }
                IgnoreBlanks();
            }

            return _words;
        }

        public void IgnoreBlanks()
        {
            if (_pos >= _line.Length)
            {
                return;
            }
            else
            {
                while (_line[_pos] == ' ' || _line[_pos] == '\t')
                {
                    ++_pos;
                }
            }
        }

        #endregion

        #region PrivateMothods
        private enum Mode
        {
            GLOBAL,
            TAG,
            ATTRIBUTE
        }

        private void Init(string str)
        {
            _pos = 0;
            _mode = Mode.GLOBAL;
            _line = str;
            _words.Clear();
        }


        private void ChangeReadMode(Mode mode)
        {
            _mode = mode;
        }
        #endregion

        private void ReadTag()
        {
            int npos = 0;
            string op = NextString(" ]", out npos);
            
            //if there is a tag
            if (op != "") 
            {
                KAGWord tag = new KAGWord(KAGWord.Type.TAG, "op", op);
                _words.Add(tag);
            }

            ChangeReadMode(Mode.ATTRIBUTE);
        }

        private void ReadAttribute()
        {
            //	CCLOG("read attribute");
            int npos = 0;
            string op = NextString(" =", out npos);

            //if (op)

            IgnoreBlanks();
            if (_line[_pos] == '=')
            {
                ++_pos;
                IgnoreBlanks();
                string value = NextString(" ]", out npos);
                //	auto value = StringUtil::nextStringStopUntil(_line, " ]", _pos, &npos);
                //	_pos = npos;

                KAGWord attr = new KAGWord(KAGWord.Type.ATTRIBUTE, op, value);
                _words.Add(attr);
            }
            else
            {
                Debug.LogFormat("read attribute wrong! op = {0}", op);
            }
        }

        private void ReadText()
        {
            int npos = 0;
            //	CCLOG("read Text!");

            string str = StringUtil.NextStringStopUntil(_line, "[\n", _pos, out npos);

            //auto str = nextString("[\n", &npos);
            //if (str)
            //{
            if (_pos < npos)
            {
               // KAGWord tag0 = new KAGWord(KAGWord.Type.TEXT, "op", "print");
                KAGWord tag1 = new KAGWord(KAGWord.Type.TEXT, "text", str);
               // _words.Add(tag0);
                _words.Add(tag1);
            }
            //}
            _pos = npos;
            ChangeReadMode(Mode.TAG);
        }

        private void ReadName()
        {
            int npos = 0;

            KAGWord tag1;

            if (_line == "#")   //No Name
            {
                tag1 = new KAGWord(KAGWord.Type.NAME, "text", "");
            }
            else
            {
                string str = StringUtil.NextStringStopUntil(_line, "[\n", _pos+1, out npos);
                tag1 = new KAGWord(KAGWord.Type.NAME, "text", str);
            }

            _words.Add(tag1);
        }
        private void ReadScenario()
        {
            int npos = 0;
            //auto str = StringUtil::nextStringStopUntil(_line, '[', _pos, &npos);
            string str = NextString("[\n", out npos);
            KAGWord op = new KAGWord(KAGWord.Type.SCENARIO, "op", "scenario");
            KAGWord tag = new KAGWord(KAGWord.Type.SCENARIO, "scenario", str);
            _words.Add(op);
            _words.Add(tag);
        }
        
        private string NextString(out int npos)
        {
            string s = StringUtil.NextString(_line, _pos, out npos);
	        _pos = npos;
	        return s;
        }

        private string NextString(string notes, out int npos)
        {
            string s = StringUtil.NextStringStopUntil(_line, notes, _pos, out npos);
            _pos = npos;
            return s;
        }
    }
    
}
