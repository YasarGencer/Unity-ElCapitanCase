using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Places : MonoBehaviour
{
    Vector2 scale;
    bool isOpen; 
    public void Initialize() {
        scale = transform.localScale;
        isOpen = false; 
        Close();
    }
    public void Close() {
        isOpen = false;
        transform.DOScale(0, .5f);
    }

    public void Open() { 
        if (InGameManager.Instance.GridManager.IsGoal(new int2(int.Parse(this.gameObject.name.Split(',')[0]), int.Parse(this.gameObject.name.Split(',')[1]))) == true)
            return;
        isOpen = true;
        StartCoroutine(Punch()); 
    } 
    IEnumerator Punch() {
        transform.DOScale(scale * 1.5f, 1f).OnComplete(()=> transform.DOScale(0, 1f));
        yield return new WaitForSeconds(2f);
        if (isOpen) {
            StartCoroutine(Punch());
        } else {
            Close();
        }
    } 
    public void Press() {
        InGameManager.Instance.SpecialsManager.SpecialAttack(new int2(int.Parse(this.gameObject.name.Split(',')[0]), int.Parse(this.gameObject.name.Split(',')[1])), this.transform.position);
    }
}
