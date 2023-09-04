using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePanel : PanelManager {
    [SerializeField] TextMeshProUGUI moveCount;
    public override void Initialize() {
        base.Initialize();
    }
    public void MoveCount(int count) {
        moveCount.text = count.ToString();
    }
}
