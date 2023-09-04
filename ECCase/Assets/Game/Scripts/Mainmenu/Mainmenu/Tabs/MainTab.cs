using System.Collections; 
using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTab : Tab {  
    public Animator[] Characters;
    public override void Initialize(Vector2 size) {
        base.Initialize(size); 
    }
    public void ButtonStart() {  
        EventRunner.LoadSceneStart();
        SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainmenuScene");
    } 
}
