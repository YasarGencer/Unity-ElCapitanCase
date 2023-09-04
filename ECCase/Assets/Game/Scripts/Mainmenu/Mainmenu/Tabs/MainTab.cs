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
        SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        EventRunner.LoadSceneStart();
        SceneManager.UnloadSceneAsync("GameMainMenu");
    } 
}
