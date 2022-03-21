using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayServiceManager : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("success");
            }
            else
            {
                Debug.Log("error auth");
            }
        });
    }

    public void UpdateRaiting(int score)
    {
        Social.ReportScore(score, GPS.leaderboard_raiting, (bool success) => { });
    }

    public void OpenLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
