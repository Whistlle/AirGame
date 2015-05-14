using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour
{

    public ScrollingImage[] Images;

   // public float ScrollSpeed;



    int _frontIndex = 0;
    // Use this for initialization
    void Start()
    {
       // StartScroll = true;
    }

    // Update is called once per frame
    public void Scroll(float scrollSpeed)
    {
       // if (StartScroll)
       // {
            foreach (ScrollingImage s in Images)
            {
                s.transform.position -= new Vector3(1, 0, 0) * scrollSpeed * Time.deltaTime;
            }
            if (IsOutOfCamera(_frontIndex))
            {
                ExchangeImage();
            }
       // }
    }

    //交换两张图片的前后顺序
    void ExchangeImage()
    {
        ScrollingImage front =  Images[_frontIndex];
        ScrollingImage back =  Images[1-_frontIndex];

        front.transform.position = new Vector3(back.RightEdgePosX + back.Width/2,
                                               front.transform.position.y,
                                               front.transform.position.z);
        _frontIndex = 1 - _frontIndex;
    }
    bool IsOutOfCamera(int index)
    {   
      float CameraLeftX = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x;
     // float CameraLeftY = Camera.main.ScreenToWorldPoint(new Vector3(pixelWidth,0,0)).x;
        if(CameraLeftX >  Images[index].RightEdgePosX)
        {
            return true;
        }
        return false;
    }

}
