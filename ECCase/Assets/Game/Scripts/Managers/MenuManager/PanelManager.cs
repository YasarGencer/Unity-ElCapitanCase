using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelManager : MonoBehaviour {
    private CanvasGroup canvasGroup; 
    public float appearTime = 0.01f; 
    public float disappearTime = 0.01f;

    private Coroutine appearDisappearCoroutine;

    public virtual void Initialize() {
        canvasGroup = GetComponent<CanvasGroup>();

        appearTime = appearTime <= 0.01f ? 0.01f : appearTime;
        disappearTime = disappearTime <= 0.01f ? 0.01f : disappearTime;  
    }
    public virtual void Appear(EventArgs eventArgs = null) {
        StopAppearDisappearCoroutine();
        gameObject.SetActive(true);
        appearDisappearCoroutine = StartCoroutine(AppearRoutine());
    }
    public void Appear() {
        StopAppearDisappearCoroutine();
        gameObject.SetActive(true);
        appearDisappearCoroutine = StartCoroutine(AppearRoutine());
    }

    public IEnumerator AppearRoutine() {
        if (canvasGroup != null) {
            while (canvasGroup.alpha < 1) {
                canvasGroup.alpha += Time.unscaledDeltaTime / appearTime;
                yield return null;
            }
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }

    public virtual void Disappear(bool instantDisappear = false) {

        if (!gameObject.activeSelf)
            return;

        if (instantDisappear) {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            gameObject.SetActive(false);
            return;
        }
         
        StopAppearDisappearCoroutine();
        appearDisappearCoroutine = StartCoroutine(DisappearRoutine());
    }

    private void StopAppearDisappearCoroutine() {
        if (appearDisappearCoroutine != null) {
            StopCoroutine(appearDisappearCoroutine);
        }
    }

    public IEnumerator DisappearRoutine() {
        if (canvasGroup != null) {
            while (canvasGroup.alpha > 0) {
                canvasGroup.alpha -= Time.unscaledDeltaTime / disappearTime;
                yield return null;
            }
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        gameObject.SetActive(false);
    }
}
