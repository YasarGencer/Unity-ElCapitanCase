using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public InGamePanel InGamePanel { get; private set; }
    public LoadingPanel LoadingPanel { get; private set; }
    public WinFailPanel WinFailPanel { get; private set; }
    public void Initialize() {

        EventRunner.LoadSceneFinish();

        InGamePanel = GetComponentInChildren<InGamePanel>();
        LoadingPanel = GetComponentInChildren<LoadingPanel>();
        WinFailPanel = GetComponentInChildren<WinFailPanel>();


        InGamePanel.Initialize();
        LoadingPanel.Initialize();
        WinFailPanel.Initialize();

        InGamePanel.Disappear(true);
        LoadingPanel.Disappear(true);
        WinFailPanel.Disappear(true);
    }
}
