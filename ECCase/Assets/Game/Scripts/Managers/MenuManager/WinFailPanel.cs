using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinFailPanel : PanelManager
{
    [SerializeField] GameObject winPanel, losePanel;
    [SerializeField] Button winButton, loseButton;
    [SerializeField] TextMeshProUGUI coinText, increaseText;
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

        int increase = 100 + (InGameManager.Instance.LevelSettings.MoveCount * 5); 
        coinText.text = MainManager.Instance.CoinManager.GetCoin().ToString();
        MainManager.Instance.CoinManager.Gain(increase);
        increaseText.text = "+" + increase.ToString();
        increaseText.gameObject.SetActive(true); 
        increaseText.GetComponent<CanvasGroup>().DOFade(1, 1f)
        .OnComplete(() => increaseText.GetComponent<CanvasGroup>().DOFade(0, 1f))
            .OnComplete(()=> coinText.text = MainManager.Instance.CoinManager.GetCoin().ToString());

    }
    public void NextButton() {
        Disappear(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
         
        EventRunner.LoadSceneStart();
        SceneManager.LoadScene("MainmenuScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("InGameScene");
    }
}
