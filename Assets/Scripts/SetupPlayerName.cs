using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayerName : NetworkBehaviour {

    [SyncVar]
    public string playerName = "player";
    [SyncVar]
    public Color playerColour = Color.white;

    public GameObject[] playerList; // list of active players in the game

    private int playerScore = 0; 

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            string scoreboardText = "SCORE:";
            foreach (GameObject p in playerList)    // update the score board
            {
                scoreboardText += "\n";
                scoreboardText += p.GetComponent<SetupPlayerName>().playerName;
                scoreboardText += " : ";
                scoreboardText += p.GetComponent<SetupPlayerName>().playerScore;
            }

            GUI.Label(new Rect(200, 0, 100, 30 + playerList.Length * 30), scoreboardText);     // display the scoreboard to the screen

            playerName = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), playerName);
            if (GUI.Button(new Rect(130, Screen.height - 40, 80, 30), "Change"))
            {
                CmdChangeName(playerName);
            }
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        playerName = newName;
    }

	// Use this for initialization
	void Start () {
        if(isLocalPlayer)
        {
            playerList = GameObject.FindGameObjectsWithTag("Player");   // find all players in the game
            // change tank colour
            Renderer[] rends = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rends)
                r.material.color = playerColour;
        }
	}

    void Update()
    {
        this.GetComponentInChildren<TextMesh>().text = playerName;
    }

    public int GetScore()
    {
        return playerScore;
    }
}
