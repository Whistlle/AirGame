using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sov.MessageNotificationCenter;

namespace Sov.AVGPart
{
    //base class of the TextBox
    public class TextBox : MonoBehaviour
    {

        public string Name;

        public Text text;

        
        public enum SetTextMode
        {
            PAGE, //[P]
            RELINE   //[R]
        }

        public string ShownText
        {
            get;
            set;
        }

        //显示TextBox，可做成动画，不同于SetVisible
        public virtual void ShowTextBox()
        {

        }

        public virtual void HideTextBox()
        {

        }

        public virtual void ClearMessage()
        {
            ShownText = "";
        }

        public virtual void Reline()
        {
            ShownText += '\n';
        }

        //设置TextBox可见与否
        public virtual void SetVisible(bool visible)
        {

        }

        //是否在场景中
        public bool IsEnterScene
        {
            get;
            set;
        }

     //   public virtual void SetText(string text, SetTextMode mode = SetTextMode.PAGE)
     //   {
            
     //   }

        public virtual void SetText(string text)
        {

        }
        //进入场景
        public virtual void EnterScene()
        {

        }

        void OnUpdateText()
        {

        }
        // Use this for initialization
        void Start()
        {
            //_textListener = new TextListener("EVENT_TEXTBOX", OnNotificate);
        }

        
        // Update is called once per frame
        void Update()
        {

        }
    }
}
