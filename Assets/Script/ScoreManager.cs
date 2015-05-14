using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using LitJson;

[System.Serializable]
public struct ScoreInfo: IComparable<ScoreInfo>
{
    public int CompareTo(ScoreInfo other)
    {
        return -1 * Score.CompareTo(other.Score);
    }

  //  public int Rank  { get; set; }

    public ScoreInfo(int s):this()
    {
        Score = s;
    }

    public int Score
    {
        get;
        set;
    }
}

[System.Serializable]
public class Scores
{
    public List<ScoreInfo> PlayerScores = new List<ScoreInfo>();
}

//instance
public class ScoreManager : MonoBehaviour {
    
    string _filepath;

   // GUIScoreMenu _scoreMenu;

    Scores _scores;

    bool _sortDirty = true;

    bool _isReadFile = false;

    static ScoreManager _scoreManagerInstance = null;

    JsonData _data;

    StringReader _file;
    public static ScoreManager Instance
    {
        get
        {
            return _scoreManagerInstance;
        }
    }

    void Awake()
    {
        _scoreManagerInstance = this; 
    }
    public void SaveScore()
    { 
    }
        
    public void ReadScoreFile()
    {
       string jsonStr = File.ReadAllText(_filepath);
       // string jsonStr = sr.ReadToEnd();
        //TextAsset s = Resources.Load<TextAsset>("score");
      //  string jsonStr = s.text;
       // _scores = JsonMapper.ToObject<Scores>(jsonStr);
        _data = JsonMapper.ToObject(jsonStr);
        
        for(int i = 0; i < 3; ++i)
        {
            ScoreInfo info = default(ScoreInfo);
            info.Score = (int)_data[i]["score"];
            _scores.PlayerScores.Add(info);
        }
        //sort
        _scores.PlayerScores.Sort();
        
        //update state
        _sortDirty = false;
        _isReadFile = true;
    }

    public void SaveScoreToFile()
    {
        for (int i = 0; i < 3; ++i)
        {
            _data[i]["score"] = _scores.PlayerScores[i].Score;
        }
        string str = JsonMapper.ToJson(_data);
        File.WriteAllText(_filepath, str);
        //CreateFile(_filepath, str);
    }

    public void CreateJsonToFile()
    {
        string json = @"
        [{""score"":0},{""score"":0},{""score"":0}]";
     //   string str = JsonMapper.ToJson(json);
        File.Create(_filepath).Close();
        File.WriteAllText(_filepath, json);
       // CreateFile(_filepath, json);
    }
    public bool IsBreakRecord(int score)
    {
        int thirdScore = 0;
        //sort
        if(_sortDirty)
            _scores.PlayerScores.Sort();
        thirdScore = _scores.PlayerScores[2].Score;
        
        if (score > thirdScore)
            return true;
        
        return false;
    }

    public void AddScoreToRecord(int score)
    {

        List<ScoreInfo> list = _scores.PlayerScores;

        list.Add(new ScoreInfo(score));

        _sortDirty = true;
    }

    public Scores GetScores()
    {
        _scores.PlayerScores.Sort();
        SaveScoreToFile();
        return _scores;
    }

    
	// Use this for initialization
	void Start () {
      //  ReadScoreFile();
        _filepath = Application.persistentDataPath + "/score.json";
        
        _scores = new Scores();
        
        /*
        if (!File.Exists(_filepath))
        {
           // File.Create(_filepath).Close();
            CreateJsonToFile();
            Debug.Log("cannot find json file!");
            ReadScoreFile();
            return;
        }
        StreamReader sr = File.OpenText(_filepath);
       // var sw = _file.OpenText();
        if (sr.ReadToEnd() == "")
        {
            sr.Close();
            CreateJsonToFile();
        }*/
        CreateJsonToFile();
        ReadScoreFile();
        
	}

    public void CreateFile(string path, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.Write(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    } 
	// Update is called once per frame
	void Update () {
	
	}
}
