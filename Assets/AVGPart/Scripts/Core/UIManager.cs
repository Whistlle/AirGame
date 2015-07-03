using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sov.AVGPart
{
    
    class UIManager
    {
        public Transform SelectLayout;

        List<GameObject> _selectList;
        
        public UIManager()
        {
            SelectLayout = GameObject.Find("SelectLayout").transform;
            if(SelectLayout == null)
            {
                Debug.LogError("SelectLayout do not exist!");
            }
            _selectList = new List<GameObject>();

        }
        public void AddSelect(string imageFileName, string text, Action onClick)
        {
            SelectObject selectObject = new SelectObject(imageFileName, text, onClick);
            selectObject.Go.transform.SetParent(SelectLayout, false);   
            _selectList.Add(selectObject.Go);
        }

        public void ShowSelects()
        {
            foreach (Transform t in SelectLayout)
            {
                //t.GetComponent<Image>().DOFade()
          //      GameObject.Instantiate(t.gameObject);
                t.gameObject.SetActive(true);
            }
            //SelectLayout.gameObject.SetActive(true);
        }

        public void ClearSelects()
        {
            foreach(GameObject go in _selectList)
            {
                GameObject.Destroy(go);
            }
            _selectList.Clear();
        }
    }
}
