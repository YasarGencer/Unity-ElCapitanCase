using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelSettings> levels;
    int level;
    public void Initialize() {
        level = PlayerPrefs.GetInt("Level", 0);
    } 
    public void NextLevel() {
        level++;
        PlayerPrefs.SetInt("Level", level);
    } 

    public int GetLevel() {
        return level;
    }
    public LevelSettings GetLevelSetting() {
        return levels[level % levels.Count];
    }
}
