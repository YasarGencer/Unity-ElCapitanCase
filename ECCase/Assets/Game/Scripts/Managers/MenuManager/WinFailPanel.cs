using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinFailPanel : PanelManager
{
    [SerializeField] GameObject winPanel, losePanel;
    [SerializeField] Button winButton, loseButton;
    public override void Initialize() {
        base.Initialize();
        winButton.onClick.AddListener(() => NextButton());
        loseButton.onClick.AddListener(() => NextButton());
    }

    public void LosePanel() {
        Appear();
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void WinPanel() {
        Appear();
        losePanel.SetActive(false);
        winPanel.SetActive(true);
    }
    public void NextButton() {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
         
        EventRunner.LoadSceneStart();
        SceneManager.LoadScene("MainmenuScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("InGameScene");
    }
}
