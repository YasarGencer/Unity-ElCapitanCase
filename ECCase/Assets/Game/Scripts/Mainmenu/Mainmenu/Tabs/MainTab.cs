using System.Collections;
using TMPro;
using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTab : Tab {  
    public Animator[] Characters;
    [SerializeField] TextMeshProUGUI text;
    public override void Initialize(Vector2 size) {
        base.Initialize(size);
        text.text = "Level - " + MainManager.Instance.LevelManager.GetLevel();
    }
    public void ButtonStart() {
        MainManager.Instance.MenuManager.MenuPopUpPanel.PopUp();
    } 
}
