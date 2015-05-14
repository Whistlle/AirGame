using UnityEngine;
using System.Collections;

public class BuildingScrolling : MonoBehaviour {

    public SpriteRenderer[] RollingImages;

    public bool IsStartRolling
    {
        get;
        set;
    }


    public float ScrollingSpeed
    {
        set
        {
            _scrollingSpeed = value;
        }
        
    }
 
    public void StartScrolling()
    { }
	
    float _scrollingSpeed;

    //图片宽度
    float _imageWidth;

    //相机左边界坐标
    float _cameraLeftEdgePosX;

    //当前正在显示的图片 编号
    int _currentImageIndex;

    // Use this for initialization
	void Start () {
	    //获取图片宽度
        if (RollingImages[0])
            _imageWidth = RollingImages[0].sprite.bounds.size.x;

        _cameraLeftEdgePosX = Camera.main.ScreenToWorldPoint(Vector3.zero).x;

        _scrollingSpeed = GameGenerator.Instance.ScrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	    if(IsStartRolling)
        {
            ScrollImage();
        }
        if(OutOfEdge() == true)
        {
            //将这张图放到另一张图后面
            int anotherImageIdx = 1 - _currentImageIndex;
            //获取另一张图片右边界坐标
            Vector3 pos = RollingImages[anotherImageIdx].transform.position;
            float rightPosX = pos.x + _imageWidth / 2;

            RollingImages[_currentImageIndex].transform.position =
                new Vector3(rightPosX + _imageWidth / 2,
                            pos.y,
                            pos.z);
            _currentImageIndex = anotherImageIdx;
        }
	}

    // 检测第一张图片是否移出相机左边界
    bool OutOfEdge()
    {
        //第一张图片右边界
        float leftPosX = RollingImages[_currentImageIndex].transform.position.x
            + RollingImages[_currentImageIndex].bounds.size.x / 2;
        
        //如果最后一张图得
        if(leftPosX < _cameraLeftEdgePosX)
        {
            return true;
        }
        return false;
    }

    void ScrollImage()
    {

    }
}
