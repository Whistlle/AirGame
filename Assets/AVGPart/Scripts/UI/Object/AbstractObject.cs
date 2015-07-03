using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    public abstract class ObjectInfo
    {
        public string Name = "";
        public string ObjName = "";
    }

    /*
     * AbstractObject
     * 
     * 引擎通过这个创建UI元素
     * 
     */
    public class AbstractObject
    {
        /*
         * 定义元素的位置
         */
        public AbstractObject()
        {

        }


        public GameObject Go;

        public Action OnAnimationFinish
        {
            protected get;
            set;
        }

        //attach gameobject in scene
        public virtual void Init(string objName)
        {
            //create Object
            Go = GameObject.Find(objName);
            if (Go == null)
            {
                Debug.LogFormat("Cannot find object:{0}", objName);
                return;
            }
        }

        public virtual void Init(ObjectInfo info)
        {
            //create Object

        }

        public virtual void SetPosition3D(float x, float y, float z)
        {
           
            Go.transform.position = new Vector3(x, y, z);
        }

        public virtual void SetPosition3D(Vector3 p)
        {
            Go.transform.position = p;
        }

        public virtual void SetPosition2D(float x, float y)
        {
            Vector3 v3 = Go.transform.position;
            Go.transform.position = new Vector3(x, y, v3.z);
        }

        public virtual void SetParent(string name)
        {
            GameObject p =  GameObject.Find(name);
            if(p == null)
            {
                Debug.LogFormat("Can not find Parent:{0}", name);
                return;
            }
            Go.transform.SetParent(p.transform);
        }
        public virtual void FadeIn(float fadetime)
        {

        }
        public virtual void FadeOut(float fadetime)
        {

        }
    }
}
