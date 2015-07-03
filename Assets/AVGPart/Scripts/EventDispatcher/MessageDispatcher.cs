using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * 实现了一个消息订阅 发送系统
 */

namespace Sov.MessageNotificationCenter
{
    /* Message
     * 消息包裹
     * 由使用者创建
     */
    public class Message
    {
        //需要传递的数据
        public System.Object UserData
        {
            get;
            set;
        }

        /*
         * @param name: 需要传递的消息名称
         */
        public Message(string name)
        {
            MessageName = name;
            UserData = null;
        }

        public Message(string name, System.Object data)
        {
            MessageName = name;
            UserData = data;
        }
        public string MessageName
        {
            get;
            set;
        }
        /*
        public void PrintInfo()
        {
            Debug.LogFormat("[Message]Name:{0}\n")
        }*/
    }

    /*
     * MessageListener
     * 消息订阅者
     */
    public class MessageListener
    {
        Message _message = null;

        public string MessageName;

        /*
         * @param string name:监听事件的名称
         */
         
        public MessageListener(string name)
        {
            MessageName = name;
        }

        public MessageListener(string name, Action<Message> messageCallBack):
            this(name)
        {
            OnMessage = messageCallBack;
        }
  
        /*
         * UpdateMessage
         * 由MessageDispatcher调用
         * 并传递数据@param sendedMessage给该监听者
         */
        public void UpdateMessage(Message sendedMessage)
        {
            _message = sendedMessage;
            OnUpdate();
        } 

        public Action<Message> OnMessage;

        protected virtual void OnUpdate()
        {
            OnMessage(_message);
        }
    }
    /* MessageDispatcher
     * 消息发送 在类之间通信
     * 需求：
     * 1.能增加删除订阅者
     * 2.根据优先级发送消息（？）
     */ 
    public class MessageDispatcher : MonoBehaviour
    {
        //消息订阅者
        Dictionary<string, List<MessageListener>> _listenerMap;
        Dictionary<string, Action<Message>> _listenerDelegate;
        
        //Instance
        public static MessageDispatcher Instance
        {
            get
            {
                if (_sharedMessageDispatcher == null)
                {
                    GameObject go = new GameObject("MessageDispatcher");
                    Debug.Log("create MessageDispatcher");
                    _sharedMessageDispatcher = go.AddComponent<MessageDispatcher>();
                    _sharedMessageDispatcher.Init();
                    DontDestroyOnLoad(_sharedMessageDispatcher.gameObject);
                }
                return _sharedMessageDispatcher;
            }
        }

        //*******************   For Test  ***************
        /*
        public static MessageDispatcher Instance
        {
            get
            {
                if (_sharedMessageDispatcher == null)
                {
                   // GameObject go = new GameObject();
                 //   _sharedMessageDispatcher = go.AddComponent<MessageDispatcher>();
                    _sharedMessageDispatcher = new MessageDispatcher();
                    _sharedMessageDispatcher.Init();
                }
                return _sharedMessageDispatcher;
            }
        }*/
        //***********************************************

        private static MessageDispatcher _sharedMessageDispatcher = null;

        

        #region Public Method
        public void RegisterMessageListener(MessageListener listener)
        {
            if(!_listenerMap.ContainsKey(listener.MessageName))
            {
                //Init
                _listenerMap[listener.MessageName] = new List<MessageListener>();
            }
                _listenerMap[listener.MessageName].Add(listener);
        }

        public void RegisterMessageListener(string messageName, Action<Message> callBack)
        {
                _listenerDelegate[messageName] += callBack;
        }

        public bool DispatchMessage(Message _Message)
        {
            Debug.LogFormat("Dispatch Message: {0}", _Message.MessageName);
            string MessageName = _Message.MessageName;

            if (!_listenerMap.ContainsKey(MessageName))
            {
                return false;
            }

            //发布 通过Listener注册的事件
            foreach (MessageListener listener in _listenerMap[MessageName])
            {
                listener.UpdateMessage(_Message);
            }

            //发布 直接注册回调函数的事件
            if(_listenerDelegate.ContainsKey(MessageName))
                _listenerDelegate[MessageName](_Message);

            return true;
        }

        
        #endregion

        void Awake()
        {
            _listenerMap = new Dictionary<string, List<MessageListener>>();
            _listenerDelegate = new Dictionary<string, Action<Message>>();
        }
        void Init()
        {

        }
    }
}