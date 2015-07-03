using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sov.AVGPart
{
    class SelectObject : AbstractObject
    {
        public int Height = 110;

        const float _preferredHeight = 110;

        public SelectObject(string imageFileName, string text, Action onClick)
        {
            ButtonObject bo = new ButtonObject(imageFileName, text, onClick);
            Go = bo.Go;

            //加入Layout
            LayoutElement le = Go.AddComponent<LayoutElement>();
            le.preferredHeight = _preferredHeight;

        }


       


    }
}
