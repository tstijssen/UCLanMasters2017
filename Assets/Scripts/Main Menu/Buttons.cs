using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public Button QuitButton;
    public Button StartButton;
    public Button OptionsButton;

    // Use this for initialization
    void Start () {
        QuitButton.onClick.AddListener(QuitGame);
        StartButton.onClick.AddListener(StartGame);
        // options button does not exist yet in game
        //OptionsButton.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update () {
        
                
	}

    void StartGame()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("GameSetup");
    }

    void QuitGame()
    {
        Debug.Log("Click");
        Application.Quit();
    }
  
}
