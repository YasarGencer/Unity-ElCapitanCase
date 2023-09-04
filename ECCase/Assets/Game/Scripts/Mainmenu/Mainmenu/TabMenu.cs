using DG.Tweening; 
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    [System.Serializable]
    public class TabData
    {
        public Tab tab;
        public TabMenuButton button;
        public float backgroundOpacity;

        public void Initialize(Vector2 size, int index, string tabName, TabMenu menu)
        {
            tab.Initialize(size);
            button.Initialize(tabName, index, menu);
        }
    }

    [Header("Tab Settings")]
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform selectedTab;
    [SerializeField] private RectTransform tabGroup;
    [SerializeField] private TabData[] tabs;
    [SerializeField] private Image tabBackground;

    [Header("Animation")]
    [SerializeField] private float tabTransitionDuration;
    [SerializeField] private Ease tabTransitionEase;
    [SerializeField] private AnimationCurve stopEase;
    
    private Vector2 anchoredPosition;
    private Vector2 targetAnchoredPosition;
    private Vector2 canvasSize;
    private Vector2 border;
    private Vector2 stopBorder;
    private int currentTab;
    private Sequence containerTween;
    private Tween selectedTabTween; 
    public void Initialize() {

        canvasSize = new Vector2(container.rect.width, container.rect.height);

        border = new Vector2(tabs[0].tab.ContainerOrder, tabs[^1].tab.ContainerOrder) * canvasSize.x;
        stopBorder = border + canvasSize.x * 0.5f * new Vector2(-1, 1);
        
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].Initialize(canvasSize, i, tabs[i].tab.tabName, this);
        }
        
        anchoredPosition = container.anchoredPosition; 
        currentTab = Mathf.RoundToInt((float)tabs.Length / 2) - 1;
        container.anchoredPosition3D = -tabs[currentTab].tab.containerPosition;
        tabs[currentTab].button.SetSelected();

        tabs[currentTab].button.OnButtonDown();
    }

    public void MoveContainer(int index)
    {
        tabs[currentTab].button.SetUnselected();
        tabs[index].button.SetSelected(); 
        Vector2 desiredContainerPos = -tabs[index].tab.containerPosition;
        float startX = selectedTab.position.x;

        if (containerTween != null && containerTween.active)
            containerTween.Kill();
        
        containerTween = DOTween.Sequence();
        containerTween.Append(container.DOAnchorPos(desiredContainerPos, tabTransitionDuration)
            .SetEase(tabTransitionEase)
            .OnUpdate(() =>
            {
                anchoredPosition = container.anchoredPosition;
                targetAnchoredPosition = anchoredPosition;
            })
            .OnComplete(() =>
            {
                anchoredPosition = desiredContainerPos;
                targetAnchoredPosition = anchoredPosition;
            }));

        containerTween.Join(
            DOTween.To(() => tabBackground.color.a, x => SetBackgroundOpacity(x), tabs[index].backgroundOpacity,
                tabTransitionDuration)
                .SetEase(tabTransitionEase));

        selectedTabTween?.Kill();
        selectedTabTween = DOTween.To(() => 0, t =>
            {
                float targetX = tabs[index].button.transform.position.x;
                float currentX = Mathf.Lerp(startX, targetX, t);
                Vector3 currentPos = selectedTab.position;
                currentPos.x = currentX;
                selectedTab.position = currentPos;

            }, 1f, tabTransitionDuration)
            .SetEase(tabTransitionEase);
        
        currentTab = index;
    }

    public void SlideContainer(float delta)
    {
        targetAnchoredPosition.x += delta;
        if (currentTab > 0 && currentTab < tabs.Length - 1)
        {
            anchoredPosition = targetAnchoredPosition;
        }
        else
        {
            float borderValue = 0;
            float stopBorderValue = 0;
            if (Mathf.Sign(targetAnchoredPosition.x) > 0)
            {
                borderValue = border.y;
                stopBorderValue = stopBorder.y;
                Debug.Log("Border:"+borderValue+"\nStopBorder:"+stopBorderValue);
            }
            else
            {
                borderValue = border.x;
                stopBorderValue = stopBorder.x;
                Debug.Log("Border:"+borderValue+"\nStopBorder:"+stopBorderValue);
            }
            //targetAnchoredPosition.x = Mathf.Clamp(targetAnchoredPosition.x, stopBorder.x, stopBorder.y);
            float t = Mathf.InverseLerp(stopBorderValue, borderValue, targetAnchoredPosition.x);
            t = Mathf.Clamp01(t);
            Vector2 borderPos = new Vector2(stopBorderValue, 0);
            anchoredPosition = Vector2.Lerp(borderPos, targetAnchoredPosition, stopEase.Evaluate(t));
        }

        Vector2 currentTabAnchoredPos = tabs[currentTab].tab.containerPosition;
        float slideDistance = -anchoredPosition.x + currentTabAnchoredPos.x;
        int direction = (int)Mathf.Sign(slideDistance);
        int nextTab = currentTab + direction;
        if (nextTab >= 0 && nextTab < tabs.Length)
        {
            float t = Mathf.InverseLerp(0, canvasSize.x, Mathf.Abs(slideDistance));
            t = Mathf.Clamp01(t);
            float opacity = Mathf.Lerp(tabs[currentTab].backgroundOpacity, tabs[nextTab].backgroundOpacity, t);
            SetBackgroundOpacity(opacity);
        }

        containerTween?.Kill();
        container.anchoredPosition = anchoredPosition;
    }
    
    public void EndSliding(bool passNextTab)
    {
        Vector2 currentPos = -tabs[currentTab].tab.containerPosition;

        int nextTab = currentTab  -(int) Mathf.Sign(anchoredPosition.x - currentPos.x);
        if (nextTab >= 0 && nextTab < tabs.Length && passNextTab)
        {
            MoveContainer(nextTab);
        }
        else
        {
            MoveContainer(currentTab);
        }
    }
    
    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tabGroup);
    }

    private void SetBackgroundOpacity(float opacity)
    {
        if (tabBackground == null)
            return;
        Color clr = tabBackground.color;
        clr.a = opacity;
        tabBackground.color = clr;
    }
    public TabData GetTabData(int value) {
        return tabs[value];
    }
}
