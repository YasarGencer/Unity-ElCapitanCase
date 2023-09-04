using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject button;
    [SerializeField] Color enabledArrow, disabledArrow;
    [SerializeField] List<int2> positions;
    public List<int2> Positions { get { return positions; } }

    Vector2 arrowScale;
    bool onlyThisPlayable = true;
    public void Initialize() {
        button.SetActive(true);
        arrow.SetActive(true);
        arrowScale = arrow.transform.localScale;
        Played();
        CheckGoal();
    }
    public void Press() {
        if (InGameManager.Instance.IsPlayable == false)
            return;
        if (onlyThisPlayable == false)
            return;
        InGameManager.Instance.ButtonManager.Played(); 
        InGameManager.Instance.GridManager.RotateButtonPressed(positions);
        InGameManager.Instance.UpdateMove();
        CheckGoal();
    }
    public void PlayableAnim() {
        if (InGameManager.Instance.IsPlayable == false)
            return;
        CheckGoal();
        if (onlyThisPlayable == false)
            return;
        arrow.GetComponent<SpriteRenderer>().color = enabledArrow;
    }
    public void Played() {
        arrow.GetComponent<SpriteRenderer>().color = disabledArrow;
    }
    void RotateTheArrow() {
        if (InGameManager.Instance.IsPlayable == false)
            return;

        arrow.transform.DORotate(new Vector3(0, 0, 360), .5f).OnComplete(()=> RotateTheArrow());

        arrow.transform.DOScale(arrowScale * 1.25f, .25f).OnComplete(()=> arrow.transform.DOScale(arrowScale * .75f, .25f));
    }
    void CheckGoal() {
        onlyThisPlayable = true;
        foreach (var item in GameObject.FindObjectsOfType<GoalController>()) {
            if(Vector2.Distance(item.transform.position, transform.position) < .75f) {
                onlyThisPlayable = false;
                Played();
            }
        }
    }
}
