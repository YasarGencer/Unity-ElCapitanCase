using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGamePanel : PanelManager {
    [SerializeField] TextMeshProUGUI moveCount;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Transform GoalContainer;
    List<GoalUI> Goals;
    [SerializeField] SpecialUI first, second;
    [System.Serializable]
    struct SpecialUI {
        public GameObject Lock, Unlock;
        public TextMeshProUGUI LevelText, PriceText;
        public Button specialButton;
    }
    public override void Initialize() {
        base.Initialize();
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneStart, CreateGoals);
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneStart, SetCoinAmount);
        MainManager.Instance.EventManager.Register(EventTypes.CurrencySpent, SetCoinAmount);
    }
    void CreateGoals(EventArgs args) {
        for (int i = 0; i < GoalContainer.childCount; i++) {
            Destroy(GoalContainer.GetChild(i).gameObject);
        }
        Goals= new List<GoalUI>();

        foreach (var item in MainManager.Instance.LevelManager.GetLevelSetting().Goals) {
            GoalUI goal = (Instantiate(item.UIPrefab, GoalContainer).GetComponent<GoalUI>());
            Goals.Add(goal);
            goal.SetCount(item.Count);
        }
    }
    public void MoveCount(int count) {
        moveCount.text = count.ToString();
    }
    public void SetGoalCount(GoalType type) {
        foreach (var item in Goals) {
            if (item.Type == type) {
                item.LowerCount();
            }
        }
    }
    public void SetCoinAmount(EventArgs args) {
        coinText.text = MainManager.Instance.CoinManager.GetCoin().ToString();
    }
    public void UpdateSpecialUIInfo(int first,int second, int firstP, int secondP, UnityAction specialPress1, UnityAction specialPress2) {
        this.first.LevelText.text = "level " + first.ToString();
        this.second.LevelText.text = "level " + second.ToString();
        this.first.PriceText.text = firstP.ToString();
        this.second.PriceText.text = secondP.ToString();
        this.first.specialButton.onClick.RemoveAllListeners();
        this.second.specialButton.onClick.RemoveAllListeners();
        this.first.specialButton.onClick.AddListener(() => specialPress1());
        this.second.specialButton.onClick.AddListener(()=> specialPress2());

    } 
    public void OpenSpecial(int type) {
        switch (type) {
            case 1:
                first.Lock.gameObject.SetActive(false);
                first.Unlock.gameObject.SetActive(true);
                break;
            case 2:
                second.Lock.gameObject.SetActive(false);
                second.Unlock.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void CloseSpecial(int type) {
        switch (type) {
            case 1:
                first.Lock.gameObject.SetActive(true);
                first.Unlock.gameObject.SetActive(false);
                break;
            case 2:
                second.Lock.gameObject.SetActive(true);
                second.Unlock.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
