using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public InGamePanel InGamePanel { get; private set; }
    public InGamePopUpPanel InGamePopUpPanel { get; private set; }
    public LoadingPanel LoadingPanel { get; private set; }
    public WinFailPanel WinFailPanel { get; private set; }
    public void Initialize() {

        EventRunner.LoadSceneFinish();

        InGamePanel = GetComponentInChildren<InGamePanel>();
        InGamePopUpPanel = GetComponentInChildren<InGamePopUpPanel>();
        LoadingPanel = GetComponentInChildren<LoadingPanel>();
        WinFailPanel = GetComponentInChildren<WinFailPanel>();


        InGamePanel.Initialize();
        InGamePopUpPanel.Initialize();
        LoadingPanel.Initialize();
        WinFailPanel.Initialize();

        InGamePanel.Disappear(true);
        InGamePopUpPanel.Disappear(true);
        LoadingPanel.Disappear(true);
        WinFailPanel.Disappear(true);
    }
}
