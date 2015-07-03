using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Sov.AVGPart
{
    public class Util
    {
        public static IEnumerator DelayToInvoke(Action action, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            action();
        }

    }
    public class StringUtil
    {
        public const string BLANKS = " \n";
        static public string NextStringStopUntil(string str, string note, int pos, out int npos)
        {
            int head = pos;
            string tempN = note;
            
            //string tempStr;
            bool success = false;
            if (pos >= str.Length)
            {
                npos = pos;
                return "";        
            }
            
                
            for (var ch = str[pos]; pos < str.Length; ch = str[pos])
            {
                foreach (var n in tempN)
                {
                    if (n == ch)
                    {
                        success = true;
                        break;
                    }
                }
                if (success)
                    break;
                //tempStr.push_back(ch);
                if (pos + 1 >= str.Length)
                {
                    ++pos;
                    break;
                }
                else
                    ++pos;
            }
            npos = pos;
            string s = str.Substring(head, pos - head);
            return s;
        }


        static public string NextStringStopUntil(string str, char note, int pos, out int npos)
        {
            return NextStringStopUntil(str, note.ToString(), pos, out npos);
        }

        /*
        * read next string stop when read blanks
        * @param int pos current pos in str
        * @param int* npos pos after func
        */
        static public string NextString(string str, int pos, out int npos)
        {
            string s = NextStringStopUntil(str, BLANKS, pos, out npos);
            return s;
        }



        static public string NextString(string str, int pos = 0)
        {
            int npos = 0;
            return NextString(str, pos, out npos);
        }

        /*
         * 首字母大写
         */
        static public string ToTitleCapital(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb[0] = Char.ToUpper(sb[0]);
            return sb.ToString();
        }
    }
}
