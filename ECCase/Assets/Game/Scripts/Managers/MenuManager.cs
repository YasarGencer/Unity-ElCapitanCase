using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public InGamePanel InGamePanel {get;}
    public LoadingPanel LoadingPanel {get;}
    public WinFailPanel WinFailPanel {get;}
    public void Initialize() {
        InGamePanel.Initialize();
        LoadingPanel.Initialize();
        WinFailPanel.Initialize();

        InGamePanel.Disappear(true);
        LoadingPanel.Disappear(true);
        WinFailPanel.Disappear(true);
    }
}
