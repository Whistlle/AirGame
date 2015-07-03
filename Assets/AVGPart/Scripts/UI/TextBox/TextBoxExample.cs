using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Sov.MessageNotificationCenter;

namespace Sov.AVGPart
{
    /* 
     * an simple sample of the implement of base TextBox Class
     */

    public class TextBoxExample : TextBox
    {
        public bool UseEffect;
        public TextEffect TextEffect;
        public float EffectDuration;
       // public Text TextUI;

      //  string _shownText;
        /*
        public override void SetText(string text, SetTextMode mode = SetTextMode.PAGE)
        {
            Debug.LogFormat("[{0}]Set Text:{1}[{2}]", Name, text, mode.ToString());
            switch(mode)
            {
                case SetTextMode.PAGE:
                    _shownText = text;
                    UpdateText();
                    break;
                case SetTextMode.RELINE:
                    _shownText += "\n" + text;
                    TextEffect.DisplayAddText(text, EffectDuration);
                    break;
            }
        }*/

        //待显示的文字
        string _textToShow = "";
 
        public override void SetText(string text)
        {
            
            if(ShownText != "")
            {
                _textToShow  = ShownText  + text;
            }
            else
                _textToShow = text;
            UpdateText();
            
           // ShownText += text;
          //  UpdateText();
        }

        void UpdateText()
        {
            if (UseEffect)
            {
                DisplayText();
            }
            else
            {
                text.text = _textToShow;
                ShownText = _textToShow;
               // text.text = ShownText;
            }
        }

        
        public void DisplayText()
        {
            if (TextEffect.IsWaiting())
            {
                TextEffect.StartEffect(ShownText, _textToShow, EffectDuration);
                ShownText = _textToShow;
            }
            else
            {
                Debug.Log("Display Text Error");
            }
        }

        public override void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        //进入场景
        public override void EnterScene()
        {
            SetVisible(true);

            IsEnterScene = true;
        }

        public void OnClick()
        {
            if (TextEffect.IsWaiting())
            {
                MessageDispatcher.Instance.DispatchMessage(new Message("SCRIPT_CLICK_CONTINUE"));
            }
            else if (TextEffect.IsRendering())
            {
                TextEffect.DisplayTextRemain();
            }
            
        }

        // Use this for initialization
        void Start()
        {
            //check textEffect
            if (UseEffect && TextEffect == null)
            {
                Debug.LogFormat("no textEffect component on this TextBox:{0}",
                                Name);
            }
            text.text = ShownText = "";
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        
    }
}