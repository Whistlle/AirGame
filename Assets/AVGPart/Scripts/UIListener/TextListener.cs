using UnityEngine;
using UnityEngine.UI;
using Sov.MessageNotificationCenter;
using System.Collections;
using DG.Tweening;
using System;
namespace Sov.AVGPart
{
    /* 
     * TextListener:
     * Add it on the Text GameObject
     * Notificate TextBox to display new text
     */

    class TextListener : MessageListener
    {
        public TextListener(string name, Action<Message> messageCallBack)
            : base(name, messageCallBack)
        {

        }
    }
}