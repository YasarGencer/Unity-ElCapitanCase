using UnityEngine;
using System;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour { 
    public void Initialize() {
        MainManager.Instance.EventManager.Register(EventTypes.LevelRestart, LoadLevel);
        MainManager.Instance.EventManager.Register(EventTypes.LevelFinish, LoadLevel);
        MainManager.Instance.EventManager.Register(EventTypes.LevelSuccess, LevelSuccess);
        MainManager.Instance.EventManager.Register(EventTypes.LevelLoaded, LevelLoaded);
    }

    private void StartGame(EventArgs args) {
        EventRunner.LevelStart();
        MainManager.Instance.EventManager.Unregister(EventTypes.OnHoldStart, StartGame);
    }

    //Initialize PlayerController and any other level scene elements
    public void LevelLoaded(EventArgs args) {
         
    }

    public void LoadLevel(EventArgs args) {
        SceneManager.LoadScene("LevelLoaderScene", LoadSceneMode.Additive);
    }

    public void LevelSuccess(EventArgs args) {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        int levelId = PlayerPrefs.GetInt("Level"); 
 
    } 
}

