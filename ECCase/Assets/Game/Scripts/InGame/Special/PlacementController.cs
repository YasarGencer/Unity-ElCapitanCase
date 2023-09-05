using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementController : MonoBehaviour
{ 
    [SerializeField] List<Places> places;
    public void Initialize() {
        foreach (var item in places) {
            item.Initialize();
        }
    }

    public void Open() { 
        foreach (var item in places) {
            item.Open();
        }
    }
    public void Close() {
        foreach (var item in places) {
            item.Close();
        }
    }
}
