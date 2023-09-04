using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePopUpPanel : PanelManager
{
    [SerializeField] GameObject goalPopUp;
    public override void Initialize() {
        base.Initialize(); 
    }
    public void PopUp(PopUpType Type) {
        Appear();
        switch (Type) {
            case PopUpType.GOAL:
                Show(goalPopUp);
                break;
            default:
                break;
        }
    }
    void Show(GameObject popUp) {
        popUp.SetActive(true);
    }
    IEnumerator Deactivate(GameObject popUp) {
        yield return new WaitForSeconds(1);
        Disappear();
        popUp.SetActive(false);
    }
}
class PopUp {
    public PopUpType Type;
    public string InfoText;
}
public enum PopUpType {
    GOAL
}
