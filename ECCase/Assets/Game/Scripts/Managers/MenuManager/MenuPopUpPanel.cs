using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPopUpPanel : PanelManager {
    [SerializeField] Button PlayButton, BackButton;
    [SerializeField] Transform Container;
    public override void Initialize() {
        base.Initialize();
        PlayButton.onClick.AddListener(() => Play());
        BackButton.onClick.AddListener(() => ClosePopUp());
    }
    public void PopUp() {
        Appear();
        for (int i = 0; i < Container.childCount; i++) {
            Destroy(Container.GetChild(i).gameObject);
        }
        foreach (var item in MainManager.Instance.LevelManager.GetLevelSetting().Goals) {
            Instantiate(item.UIPrefab, Container).GetComponent<GoalUI>().SetCount(item.Count);
        }
    }
        
    public void ClosePopUp() {
        Disappear();
    }
    void Play() {
        EventRunner.LoadSceneStart();
        //SceneManager.LoadScene("MainmenuScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainmenuScene");
        Disappear(true);
    }
}
