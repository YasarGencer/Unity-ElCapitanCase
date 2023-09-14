using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] List<ButtonController> buttons;
    public List<ButtonController> Buttons { get { return buttons; } }
    public void Initialize() {
        foreach (var item in buttons) {
            item.Initialize();
        } 
    }
    public void Played() { 
        foreach (var button in buttons) {
            button.Played();
        }
        InGameManager.Instance.IsPlayable= false;
    }
    public void PlayableAnim() {
        foreach (var button in buttons) {
            button.PlayableAnim();
        }
    }
}
