using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Sov.AVGPart
{
    /*
     * ButtonObject
     * 
     * Use this to create Button UI elements in scene
     */

    class ButtonObject: AbstractObject
    {
       
        Image _image;
        Text _text;
        Button _button;

        public ButtonObject(string buttonImageFileName, string text, Action onClick)
        {
            Go = Resources.Load(Settings.PREFAB_PATH + "Button") as GameObject;
           // Go.SetActive(false);
            Go = GameObject.Instantiate<GameObject>(Go);

            _image = Go.GetComponent<Image>();

            Sprite sprite = Resources.Load<Sprite>(Settings.UI_IMAGE_PATH + buttonImageFileName);
            
            if (_image && sprite)
            {
                _image.sprite = sprite;
            }
            else
            {
                Debug.Log("Do not find image file");
            }

            _text = Go.GetComponentInChildren<Text>();
            _text.text = text;

            _button = Go.GetComponent<Button>();
            if(_button)
            {
                _button.onClick.AddListener(new UnityEngine.Events.UnityAction(onClick));
            }
        }
        

    }
}
