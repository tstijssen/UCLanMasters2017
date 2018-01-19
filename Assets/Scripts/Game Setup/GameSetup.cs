using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour {

    public Button TeamUpButton;
    public Button TeamDownButton;
    public Button GameStart;
    private Text m_TeamNoText;

    public static int m_TeamNo = 1;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        m_TeamNoText = GameObject.Find("Teams").GetComponent<Text>();
        m_TeamNoText.text = "Teams: " + m_TeamNo.ToString();
        TeamUpButton.onClick.AddListener(AddTeam);
        TeamDownButton.onClick.AddListener(SubTeam);
        GameStart.onClick.AddListener(Launch);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    void UpdateTeams()
    {

    }

    void UpdateLevel()
    {
    }

    void AddTeam()
    {
        if(m_TeamNo < 4)
        {
        m_TeamNo++;
        m_TeamNoText.text = "Teams: " + m_TeamNo.ToString();

        }
    }

    void SubTeam()
    {
        if(m_TeamNo > 1)
        {
            m_TeamNo--;
            m_TeamNoText.text = "Teams: " + m_TeamNo.ToString();
        }
    }


    void Launch()
    {
        
        SceneManager.LoadScene("Battle Local");
    }
}
