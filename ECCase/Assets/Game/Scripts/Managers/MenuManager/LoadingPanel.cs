using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : PanelManager {
    public override void Initialize() {
        base.Initialize();
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneStart, LoadStart);
        MainManager.Instance.EventManager.Register(EventTypes.LoadSceneFinish, LoadFinish);
    }

    private void LoadFinish(EventArgs arg0) {
        Disappear();
    }

    private void LoadStart(EventArgs arg0) {
        Appear();
    }
}
