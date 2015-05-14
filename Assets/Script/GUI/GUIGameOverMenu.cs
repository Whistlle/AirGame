using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
public class GUIGameOverMenu : GUIMenu {

    //distance?
    public Text Score;

    public Text BestScore;

    bool _textAnimationFinish = false;

    int _bestScore;
	// Use this for initialization
	void Start () {
        BestScore.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayScore()
    {
        int score = GameManager.Instance.Distance;
        
        StartCoroutine(DisplayNumberAnimation(Score, GameManager.Instance.Distance)); 

        
    }

    public IEnumerator DisplayNumberAnimation(Text text, int num)
    {
        //计算数字的位数
        string numStr = num.ToString();
        int numCarry = numStr.Length;        
        
        int numToPrint = 0;
        while(numToPrint != num)
        {
            //每一帧所有位数统一加1
            for(int i = 1; i <= num; i *= 10)
            {
                //获取该位上的数字
                int digitNum = num / i % 10;
                int digitToPrint = numToPrint / i % 10;
                
                if(digitToPrint + 1 <= digitNum)
                {
                    ++digitToPrint;
                    numToPrint += i;
                }
                else
                {
                    continue;
                }
            }
            //update text
            text.text = numToPrint.ToString();
            yield return new WaitForSeconds(0.15f);
            yield return 0;
        } 
        if(_bestScore < num)
        {
            UpdateBestScore(num);
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
    }
    void UpdateBestScore(int s)
    {
        int best = int.Parse(BestScore.text);
        if(best < s)
            BestScore.text = s.ToString();
    }
}
