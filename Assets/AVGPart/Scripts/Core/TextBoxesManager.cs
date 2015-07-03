using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sov.MessageNotificationCenter;

namespace Sov.AVGPart
{
    enum TextBoxType
    {
        MAIN,
        OTHER
    };

    /*
     * TextBoxesManager
     * 挂在GameObject上以管理TextBox
     */
    class TextBoxesManager// : MonoBehaviour
    {
        public static TextBoxesManager Instance
        {
            get
            {
                if (_sharedTextBoxesManager == null)
                {
                    _sharedTextBoxesManager = new TextBoxesManager();
             
                }
                return _sharedTextBoxesManager;
            }
        }

        //public TextBox GetCurrentMainTextBox() { return _currentMainTextBox; }
        public TextBoxesManager()
        {

            _textBoxes = new Dictionary<string, TextBox>();
            TextBoxesInScene = new List<TextBox>();
         //   CurrentMainTextBox = TextBoxesInScene[0];
        //    RegisterTextBoxesInScene();
        }
        //Instance
        static TextBoxesManager _sharedTextBoxesManager = null;
        
        public List<TextBox> TextBoxesInScene;

        //registered textBox above
        Dictionary<string, TextBox> _textBoxes;

        public TextBox CurrentMainTextBox
        {
            get;
            set;
        }

        public TextBox CurrentNameTextBox
        {
            get;
            set;
        }

        public TextBox GetTextBoxInScene(string name)
        {
            foreach (TextBox t in TextBoxesInScene)
            {
                if (t.name == name)
                {
                    return t;
                }
            }

            GameObject go = GameObject.Find(name);
            if (go == null)
            {
                return null;
            }
            else
            {
                return go.GetComponent<TextBox>();
            }
        }


        //TextBox Click Event
        public void OnClickNextMessage()
        {

        }

        #region       Message Events

        //tag: settext
        public void SetText(Dictionary<string,string> data)
        {
            Debug.Log("[SetText]");
            string boxName = data["textbox"];
            if (_textBoxes.ContainsKey(boxName)) //TODO: 异常
            {
                string text = data["text"];
                TextBox textbox = _textBoxes[boxName];
                textbox.ClearMessage();
                // textbox.ShowTextBox();
                textbox.SetText(text);
            }

        }

        public void RegTextbox(string objName, string type)
        {
            TextBox tb = GetTextBoxInScene(objName);

            if(tb == null)
            {
                Debug.LogFormat("Can not reg textbox:{0}", objName);
            }

            switch(type)
            {
                case "main":
                    CurrentMainTextBox = tb;
                    break;
                case "name":
                    CurrentNameTextBox = tb;
                    break;
                default:
                    break;
            }
            if(!TextBoxesInScene.Contains(tb))
            {
                TextBoxesInScene.Add(tb);
            }
        }
        public void SetName(string text)
        {
            Debug.LogFormat("[SetName]:{0}", text);

            TextBox textbox = CurrentNameTextBox;
            if (CurrentNameTextBox)
            {
                textbox.ClearMessage();
                textbox.SetText(text);
            }
        }
        
        //tag: print
        public void PrintText(Dictionary<string, string> data)
        {
            Debug.Log("[Print Text]");

            string text = data["text"];

            CurrentMainTextBox.SetText(text);
        }

        //hide messagebox
        public void HideMsgBox(Dictionary<string, string> data)
        {
            Debug.Log("[Hide Msg Box]");
            string textboxName = data["name"];

            TextBox box;
            if (_textBoxes.ContainsKey(textboxName))
            {
                box = _textBoxes[textboxName];
                box.HideTextBox();
            }
            else if (_textBoxes.ContainsKey(textboxName))
            {
                box = _textBoxes[textboxName];
                box.HideTextBox();
            }
        }

        //change current main text box
        public void OnCurrent(Dictionary<string, string> data)
        {
            Debug.Log("[Current]");
            string layer;

            if (data.ContainsKey("layer"))
            {
                layer = data["layer"];
                if (_textBoxes.ContainsKey(layer))
                {
                    Debug.LogFormat("change main textbox to: {0}", layer);
                    CurrentMainTextBox = _textBoxes[layer];
                    CurrentMainTextBox.SetVisible(true);
                }
                else
                {
                    Debug.LogFormat("can not change main textbox: {0}", layer);
                }
            }
            else
            {
                Debug.LogFormat("message error!");
            }
        }

        //Tag: [CM]
        public void ClearMessage(Dictionary<string, string> data)
        {
            CurrentMainTextBox.ClearMessage();
        }

        //Tag: [r]
        public void Reline()
        {
            CurrentMainTextBox.Reline();
        }
        #endregion

        void RegisterTextBoxesInScene()
        {
            foreach (TextBox tb in TextBoxesInScene)
            {
                _textBoxes.Add(tb.Name, tb);
            }
        }

 
        
    };
}