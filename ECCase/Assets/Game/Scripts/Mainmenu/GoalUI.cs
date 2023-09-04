using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject completedIcon;
    [SerializeField] GoalType type;
    public GoalType Type { get { return type; } }
    int count = 0;
    public void SetCount(int count) {
        this.count = count;
        if (this.count == 0) {
            countText.gameObject.SetActive(false);
            completedIcon.SetActive(true);
        } else {
            countText.gameObject.SetActive(true);
            completedIcon.SetActive(false);
            countText.SetText(count.ToString());
        }
    } 
    public void LowerCount() {
        this.count--;
        if (this.count == 0) {
            countText.gameObject.SetActive(false);
            completedIcon.SetActive(true);
        } else {
            countText.gameObject.SetActive(true);
            completedIcon.SetActive(false);
            countText.SetText(this.count.ToString());
        }
    }
}
