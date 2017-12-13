using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum TeamNames { Red, Blue, Yellow, Green };

class Team
{
    TeamNames m_TeamName;
    string m_TeamNameString;
    int m_ScoreNum;
    Text m_ScoreText;

    // no default constructor
    public Team(TeamNames team)
    {
        m_TeamName = team;

        // get string version of team name
        switch (m_TeamName)
        {
            case TeamNames.Red:
                m_TeamNameString = "Red";
                break;
            case TeamNames.Blue:
                m_TeamNameString = "Blue";
                break;
            case TeamNames.Yellow:
                m_TeamNameString = "Yellow";
                break;
            case TeamNames.Green:
                m_TeamNameString = "Green";
                break;
        }

        // get score label GUI object using team name
        m_ScoreText = GameObject.Find(m_TeamNameString).GetComponent<Text>();
        m_ScoreText.text = m_TeamNameString + ": 0";
    }

    public TeamNames GetTeam()
    {
        return m_TeamName;
    }

    // increment score number and update GUI text
    public void AddScore()
    {
        m_ScoreNum++;
        m_ScoreText.text = m_TeamNameString + ": " + m_ScoreNum.ToString();
    }
}

public class UIElements : MonoBehaviour
{

    private Team []m_Teams;
    private Text m_Winner;
    
    private Text m_Timer;

    int count;
    float second;
    private GameObject GameInfo;

    // Use this for initialization
    void Start()
    {
        int numTeams = GameSetup.m_TeamNo;
        m_Teams = new Team[numTeams];

        m_Timer = GameObject.Find("Timer").GetComponent<Text>();



        // initialize team classes
        for (int i = 0; i < numTeams; ++i)
        {
            m_Teams[i] = new Team((TeamNames)i);
        }

        m_Timer.text = "Time: 60";
        count = 60;
        second = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //// check if a client has connected to the game
        //if(NetworkManager.singleton.IsClientConnected())
        //{
        //    second += Time.deltaTime;
        //    if (second >= 1)
        //    {
        //        count--;
        //        m_Timer.text = "Time: " + count.ToString();
        //        second = 0f;
        //    }

        //    if (count == 0)
        //    {
        //        //EndGame();
        //    }
        //}
    }
    void EndGame()
    {
        //NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("GameSetup");
    }

    public void UpdateScore(TeamNames teamNo)
    {
        m_Teams[(int)teamNo].AddScore();
    }
}
