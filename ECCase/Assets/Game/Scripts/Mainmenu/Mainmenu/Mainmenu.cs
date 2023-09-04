using UnityEngine;
using System;

public class Mainmenu : MonoBehaviour
{
    public static Mainmenu Instance;

    public TabMenu TabMenu;
    public TopPanel TopPanel; 
    private void Start() {
        MainManager.Instance.EventManager.AutoUnregister(new EventArgs());
        Initialize();
    }
    void Initialize() {
        Instance = this;  
        TabMenu.Initialize();
        TopPanel.Initialize();
    } 
}
