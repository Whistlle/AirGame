using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sov.AVGPart
{
    
    class ImageInfo : ObjectInfo
    {
        public string Path = "";
        public Vector3 Position = new Vector3(0, 0, 0);
        public bool Show = false;
        public bool Fade = false;
        public float Fadetime = 0.0f;
        public string Root = "";
        public float Scale = 1;
        public string PrefabName = "";

        public ImageInfo(Dictionary<string, string> param)
        {
            if (param.ContainsKey("objname"))
            {
                ObjName = param["objname"];
            }
            if (param.ContainsKey("name"))
            {
                Name = param["name"];
            }
            if (param.ContainsKey("path"))
            {
                Path = param["path"];
            }
            if (param.ContainsKey("show"))
            {
                Show = bool.Parse(param["show"]);
            }
            if (param.ContainsKey("fadeTime"))
            {
                Fadetime = float.Parse(param["fadeTime"]);
            }
            if (param.ContainsKey("root"))
            {
                Root = param["root"];
            }
            if (param.ContainsKey("scale"))
            {
                Scale = float.Parse(param["scale"]);
            }
            if (param.ContainsKey("PrefabName"))
            {
                PrefabName = param["PrefabName"];
            }
        }
    }

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

            //TODO: create by prefab
            //TODO: 同一加载管理Prefab
            if (_info.PrefabName != "")
            {
                Go = Resources.Load<GameObject>(_info.PrefabName);
                if (Go == null)
                {
                    Go = new GameObject(info.ObjName);
                    _image = Go.AddComponent<Image>();
                }
                else
                {
                    Go = GameObject.Instantiate(Go);
                    _image = Go.GetComponent<Image>();
                }
            }
            else
            {
                Go = new GameObject(info.ObjName);
                _image = Go.AddComponent<Image>();
            }
            
            //set gameobject name
            Go.name = _info.ObjName;
            //create image
            Sprite i = Resources.Load<Sprite>(_info.Path + _info.Name);
            if (i == null)
            {
                Debug.LogFormat("Cannot load image file:{0}", _info.Path + _info.Name);
            }
            _image.sprite = i;
            //set root
            GameObject parent = GameObject.Find(_info.Root);
            if(parent)
            {
                Go.transform.SetParent(parent.transform, false);
            }
            _image.SetNativeSize();

            Go.SetActive(false);
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
            if (fadeTime == 0)
            {
                //Go.SetActive(true);
                
                OnAnimationFinish();
            }
            else
            {
                FadeIn(fadeTime);
            }//   Sequence s = DOTween.Sequence();
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
