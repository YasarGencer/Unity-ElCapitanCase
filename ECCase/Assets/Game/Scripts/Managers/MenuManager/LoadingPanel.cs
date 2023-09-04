using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : PanelManager {
    [SerializeField] Image loadingBar; 
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
        loadingBar.fillAmount = 0;
        StartCoroutine(LoadBar());
    } 
    IEnumerator LoadBar() {
        yield return new WaitForSeconds(.05f);
        loadingBar.fillAmount += UnityEngine.Random.Range(0.1f, 0.3f);
        if(loadingBar.fillAmount < 1)
            StartCoroutine(LoadBar());
    }
}
