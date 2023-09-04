using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public InGamePanel InGamePanel { get; private set; }
    public InGamePopUpPanel InGamePopUpPanel { get; private set; }
    public LoadingPanel LoadingPanel { get; private set; }
    public WinFailPanel WinFailPanel { get; private set; }
    public MenuPopUpPanel MenuPopUpPanel { get; private set; }
    public void Initialize() {

        EventRunner.LoadSceneFinish();

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        InGamePanel = GetComponentInChildren<InGamePanel>();
        InGamePopUpPanel = GetComponentInChildren<InGamePopUpPanel>();
        LoadingPanel = GetComponentInChildren<LoadingPanel>();
        WinFailPanel = GetComponentInChildren<WinFailPanel>();
        MenuPopUpPanel = GetComponentInChildren<MenuPopUpPanel>();


        InGamePanel.Initialize();
        InGamePopUpPanel.Initialize();
        LoadingPanel.Initialize();
        WinFailPanel.Initialize();
        MenuPopUpPanel.Initialize();

        InGamePanel.Disappear(true);
        InGamePopUpPanel.Disappear(true);
        LoadingPanel.Disappear(true);
        WinFailPanel.Disappear(true);
        MenuPopUpPanel.Disappear(true);
    }
}
