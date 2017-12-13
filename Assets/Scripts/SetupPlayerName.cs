using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayerName : NetworkBehaviour {

    [SyncVar]
    public string playerName = "player";
    [SyncVar]
    public Color playerColour = Color.white;

    void OnGUI()
    {
        if (isLocalPlayer)
        {
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
}
