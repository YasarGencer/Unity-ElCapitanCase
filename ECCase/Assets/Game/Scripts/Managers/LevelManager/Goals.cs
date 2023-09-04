using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Goals
{
    public GameObject Prefab;
    public GameObject UIPrefab;
    public GoalType Type;
    public List<int2> Positions;
    public int Count { get { return Positions.Count; } }
    public int Counter { get; set; }
    public Sprite uiSprite;
}
public enum GoalType {
    BOX, 
    PLATE
}
