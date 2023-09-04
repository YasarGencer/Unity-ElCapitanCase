using DG.Tweening;
using TMPro;
using UnityEngine;

public class TabMenuButton : MonoBehaviour
{
    [SerializeField] private float selectedSize;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI selectedTitle;

    private float xPos;
    private int index;
    private TabMenu menu;

    private Tween scaleTween;
    
    public void Initialize(string tabName, int index, TabMenu menu)
    {
        title.text = tabName;
        selectedTitle.text = tabName;
        
        this.index = index;
        this.menu = menu;
    }
    
    public void OnButtonDown()
    {
        menu.MoveContainer(index);
    }

    public void SetSelected()
    {
        SetSize(selectedSize, true);
        title.gameObject.SetActive(false);
        selectedTitle.gameObject.SetActive(true);
    }

    public void SetUnselected()
    {
        SetSize(1);
        title.gameObject.SetActive(true);
        selectedTitle.gameObject.SetActive(false);
    }

    private void SetSize(float size, bool update = false)
    {
        scaleTween?.Kill();
        scaleTween = transform.DOScale(size, scaleDuration)
            .SetEase(scaleEase);
        
        if (update)
            scaleTween.OnUpdate(() => menu.UpdateLayout());
    }
}
