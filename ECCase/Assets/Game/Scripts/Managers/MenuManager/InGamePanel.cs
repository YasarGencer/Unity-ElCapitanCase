using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePanel : PanelManager {
    [SerializeField] TextMeshProUGUI moveCount;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Transform GoalContainer;
    List<GoalUI> Goals;
    public override void Initialize() {
        base.Initialize();
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneStart, CreateGoals);
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneStart, SetCoinAmount);
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
}
