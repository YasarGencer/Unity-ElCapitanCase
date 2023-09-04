using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSetting", menuName = "ScriptableObjects/LevelSettings", order = 1)]

public class LevelSettings : ScriptableObject {
    [SerializeField] List<Goals> goals;
    [SerializeField] int moveCount; 
    public List<Goals> Goals { get { return goals; } }
    public int MoveCount {
        get { return moveCount; }
        set { moveCount = value; }
    }
}