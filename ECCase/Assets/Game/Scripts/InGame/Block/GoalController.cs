using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : BlockController {
    Vector3 scale;
    [SerializeField] GoalType Type;
    bool hasPopped;
    int x, y;
    public void Initialize(Vector2 position, int openLeg, int x, int y) {
        this.x = x; this.y = y;
        transform.position = position;
        hasPopped = false;
        gameObject.SetActive(true);
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale * 1.2f, .45f).SetEase(Ease.InCirc).OnComplete(() => transform.DOScale(scale, .2f)).SetEase(Ease.InCirc);

        openBit = (int)Random.Range(5, 5000f);

    }
    public void Pop() {
        if (hasPopped)
            return;
        hasPopped = true;
        transform.DOScale(scale * 1.2f, .35f).SetEase(Ease.InCirc).OnComplete(() => transform.DOScale(0, .15f)).SetEase(Ease.InCirc).OnComplete(() => gameObject.SetActive(false));
        //StartCoroutine(PopCoroutine());
        InGameManager.Instance.UpdateGoal(Type);
        InGameManager.Instance.GridManager.RemoveFromGrid2(x, y);
    }


    IEnumerator PopCoroutine() {
        yield return new WaitForSeconds(.5f);
    }
    public void Move(Vector2 pos) { 
    }
}
