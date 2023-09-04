using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BlockController : MonoBehaviour
{
    [SerializeField] BitController[] bitControllers;
    int openBit;
    int openLeg;
    public int OpenBit { get { return openBit; } } 
    public void Initialize(Vector2 position, int openLeg) {
        openBit = Random.Range(0, 4);
        this.openLeg = openLeg;
        transform.position = position;
        for (int i = 0; i < 4; i++) { 
            bitControllers[i].gameObject.SetActive(this.openLeg == i);
            bitControllers[i].Initialize(openBit);
        } 
    }
    public void Pop() {
        bitControllers[openLeg].Pop();
        StartCoroutine(PopCoroutine());
    }


    IEnumerator PopCoroutine() {
        yield return new WaitForSeconds(.5f);
    }
    public void Move(Vector2 pos) {
        bitControllers[openLeg].Move(pos);
    }
}
