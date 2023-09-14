using DG.Tweening; 
using System.Collections; 
using Unity.Mathematics; 
using UnityEngine;

public class Places : MonoBehaviour
{
    Vector2 scale;
    bool isOpen = true; 
    public void Initialize() {
        scale = transform.localScale;  
        Close();
    }
    public void Close() {
        if (isOpen == false)
            return;
        isOpen = false;
        transform.DOScale(0, .5f).OnComplete(()=> gameObject.SetActive(isOpen)); 
    }

    public void Open() {
        if (isOpen == true)
            return;
        if (InGameManager.Instance.GridManager.IsGoal(new int2(int.Parse(this.gameObject.name.Split(',')[0]), int.Parse(this.gameObject.name.Split(',')[1]))) == true)
            return;
        isOpen = true;
        gameObject.SetActive(isOpen);
        StartCoroutine(Punch()); 
    } 
    IEnumerator Punch() {
        if (isOpen == true) {
            transform.DOScale(scale * 1.5f, 1f).OnComplete(() => transform.DOScale(0, 1f));
            yield return new WaitForSeconds(2f);
            if (isOpen) {
                StartCoroutine(Punch());
            } else {
                Close();
            }
        }
    } 
    public void Press() {
        InGameManager.Instance.SpecialsManager.SpecialAttack(new int2(int.Parse(this.gameObject.name.Split(',')[0]), int.Parse(this.gameObject.name.Split(',')[1])), this.transform.position);
    }
}
