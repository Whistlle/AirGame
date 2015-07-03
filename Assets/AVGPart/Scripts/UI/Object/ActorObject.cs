using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sov.AVGPart
{
    class ActorObject: AbstractObject
    {

        string ActorPrefabPath = Settings.PREFAB_PATH + "Image";

        Image _image;

        public ActorObject(): base()
        { 
           
        }

        public override void Init(ObjectInfo infoo)
        {
            ImageInfo info = (ImageInfo)infoo;
            //load image
            GameObject go = Resources.Load<GameObject>(Settings.PREFAB_PATH + "Image");
            go = GameObject.Instantiate(go);
            
            //set tag

            go.layer = Settings.ACTOR_LAYER;

            //set name
            go.name = info.ObjName;

            //add Image
            _image = go.GetComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(info.Path + info.Name);

            if (sp)
            {
                _image.sprite = sp;
            }
            else
            {
                Debug.LogFormat("Actor: {0} not found", info.Path + info.Name);
            }

            Transform t = go.transform;

            //set parent
            t.SetParent(Settings.ActorRoot, true);

            //set position and scale
            t.position = info.Position;
            
            t.localScale = new Vector3(info.Scale, info.Scale, info.Scale);

            //set image origin size
            _image.SetNativeSize();
            //set image size
            
            go.SetActive(false);
            this.Go = go;
        }

        public override void FadeIn(float fadetime)
        {
            if (fadetime == 0)
                Go.SetActive(true);
            else
            {
                _image.color = new Color(255, 255, 255, 0);
                Tween t = _image.DOFade(1, fadetime);
                if (OnAnimationFinish != null)
                    t.OnComplete(new TweenCallback(OnAnimationFinish));
            }
        }

        public override void FadeOut(float fadetime)
        {
            if (fadetime == 0)
                Go.SetActive(true);
            else
            {
                _image.color = new Color(255, 255, 255, 255);
                Tween t = _image.DOFade(0, fadetime);
                if (OnAnimationFinish != null)
                    t.OnComplete(new TweenCallback(OnAnimationFinish));
            }
        }

        public override void SetPosition3D(Vector3 p)
        {
            Go.GetComponent<RectTransform>().anchoredPosition3D = p;
        }

    }
}
