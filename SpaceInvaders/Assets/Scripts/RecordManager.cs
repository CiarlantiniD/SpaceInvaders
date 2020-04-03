using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordManager
{
    void SaveBestScore(int score);
    int GetBestScore();
}


public class RecordManager : IRecordManager 
{
    private const string SCORE_PLAYERPREF_KEY = "SCORE";

    public void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt(SCORE_PLAYERPREF_KEY, score);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(SCORE_PLAYERPREF_KEY);
    }

    public void CleanBestScore()
    {
        PlayerPrefs.SetInt(SCORE_PLAYERPREF_KEY, 0);
    }
}
