using DG.Tweening;
using System;
using UnityEngine;

public class BitController : MonoBehaviour {
    [SerializeField] GameObject[] bits;
    int openBit;
    Vector3 scale;
    public void Initialize(int openBit) {
        this.openBit = openBit;
        foreach (var item in bits) {
            item.SetActive(false);
        } 

        scale = bits[openBit].transform.localScale;
        bits[this.openBit].gameObject.SetActive(true);
        bits[this.openBit].transform.localScale = Vector3.zero;
        bits[this.openBit].transform.DOScale(scale * 1.2f, .45f).SetEase(Ease.InCirc).OnComplete(() => bits[this.openBit].transform.DOScale(scale, .2f)).SetEase(Ease.InCirc);
    } 
    public void Pop() {
        bits[this.openBit].transform.DOScale(scale * 1.2f, .35f).SetEase(Ease.InCirc).OnComplete(() => bits[this.openBit].transform.DOScale(0, .15f)).SetEase(Ease.InCirc);
    } 
}
