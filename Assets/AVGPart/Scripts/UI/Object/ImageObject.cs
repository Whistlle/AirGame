using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sov.AVGPart
{
    class ImageObject:AbstractObject
    {
        Image _image;
        ImageInfo _info;
        /*
        public ImageObject(string objectName)
        {
        }
        */

        public ImageObject()
        {
            
        }

        
        #region Factory
        public override void Init(ObjectInfo info)
        {
            base.Init(info);
            
            _info = (ImageInfo)info;

            Go = new GameObject(info.ObjName);

            _image = Go.AddComponent<Image>();
            //create image
            Sprite i = Resources.Load<Sprite>(_info.Path + _info.Name);
            if (i == null)
            {
                Debug.LogFormat("Cannot load image file:{0}", _info.Path + _info.Name);
            }
        }

        public override void Init(string objName)
        {
            //attach gameobject in scene
            base.Init(objName);

            _image = Go.GetComponent<Image>();

        }
        /*
        public static ImageObject CreateWithSceneObject(string objectName)
        {
            ImageObject io = new ImageObject();
            io.Go = GameObject.Find(objectName);
            if (io.Go == null)
            {
                Debug.LogFormat("Cannot find object:{0}", objectName);
                return null;
            }
            io._image = io.Go.GetComponent<Image>();
            return io;
        }

        public static ImageObject CreateWithNewImage(string imageFileName, string objectName)
        {
            ImageObject io = new ImageObject();

            io.Go = new GameObject(objectName);

            io._image = io.Go.AddComponent<Image>();
            //create image
            Sprite i = Resources.Load<Sprite>(imageFileName);
            if (i == null)
            {
                Debug.LogFormat("Cannot load image file:{0}", imageFileName);
            }
            return io;
        }

        public static ImageObject CreateWithNewImage(ImageInfo info)
        {
            GameObject go = Resources.Load<GameObject>(info.Path + info.Name);
            go = GameObject.Instantiate(go);
            //create image
            Sprite i = Resources.Load<Sprite>(info.Path + info.Name);
            if (i == null)
            {
                Debug.LogFormat("Cannot load image file:{0}", info.Path + info.Name);
            }
            return go.GetComponent<ImageObject>();
        }

        public static ImageObject CreateWithInfo(ImageInfo info)
        {
            
            ImageObject io = new ImageObject();

            io.Go = new GameObject(info.ObjName);

            io.SetPosition3D(info.Position);

            io._image = io.Go.AddComponent<Image>();
            
            ImageObject io = CreateWithNewImage(info.Path + info.Name, info.ObjName);

            io.SetPosition3D(info.Position);

            io.SetParent(info.Root);
            return io;
        }

        public static ImageObject CreateWithPrefab(ImageInfo info)
        {
            GameObject go = Resources.Load<GameObject>(info.Path + info.Name);
            go = GameObject.Instantiate(go);
            
            return go.GetComponent<ImageObject>();
        }
        */
        #endregion

        public void ChangeImage(string newImageFileName, float fadeTime)
        {
            Sprite i = Resources.Load<Sprite>(newImageFileName);
            _image.sprite = i;
            FadeIn(fadeTime);
            //   Sequence s = DOTween.Sequence();
            //     s.Append(t);

            //    if(OnAnimationFinish != null)
            //     s.AppendCallback(new TweenCallback(OnAnimationFinish));
        }

        public override void FadeIn(float fadetime)
        {
            if (fadetime == 0)
            {
                Go.SetActive(true);
                OnAnimationFinish();
            }
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
            {
                Go.SetActive(true);
                OnAnimationFinish();
            }
            else
            {
                _image.color = new Color(255, 255, 255, 255);
                Tween t = _image.DOFade(0, fadetime);
                if (OnAnimationFinish != null)
                    t.OnComplete(new TweenCallback(OnAnimationFinish));
            }
        }
    }
}
