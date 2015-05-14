using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GUIScoreMenu : GUIMenu
{
    //排名 
    public Text[] ScoreRank;

    ScoreManager _scoreManager;
    // Use this for initialization
    void Start()
    {
        _scoreManager = ScoreManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        Scores scores = ScoreManager.Instance.GetScores();
        for(int i = 0; i < ScoreRank.Length; ++i)
        {
            ScoreRank[i].text = scores.PlayerScores[i].Score.ToString();
        }
    }

    public override void Enter()
    {
        UpdateScore();
        base.Enter();
    }


}
