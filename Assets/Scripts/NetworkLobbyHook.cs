using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobbyProfile = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupPlayerName localPlayer = gamePlayer.GetComponent<SetupPlayerName>();

        localPlayer.playerName = lobbyProfile.playerName;
        localPlayer.playerColour = lobbyProfile.playerColor;

        

    }
}
