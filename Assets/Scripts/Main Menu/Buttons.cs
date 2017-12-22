using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public Button QuitButton;
    public Button StartLocalButton;
    public Button StartNetworkedButton;
    public Button SettingsButton;
    
    // Use this for initialization
    void Start () {
        QuitButton.onClick.AddListener(QuitGame);
        StartLocalButton.onClick.AddListener(StartLocal);
        StartNetworkedButton.onClick.AddListener(StartNetworked);
        SettingsButton.onClick.AddListener(GameSettings);
    }
	
	// Update is called once per frame
	void Update () {
        
                
	}

    void GameSettings()
    {
        Debug.Log("Click");
        // settings screen not implemented yet
    }

    void StartNetworked()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("Lobby");
    }

    void StartLocal()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("GameSetup");    // change this scene to the local game KENDALL
    }

    void QuitGame()
    {
        Debug.Log("Click");
        Application.Quit();
    }
  
}
