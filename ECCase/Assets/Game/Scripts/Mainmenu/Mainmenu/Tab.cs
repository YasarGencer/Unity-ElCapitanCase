using TMPro;
using UnityEngine.UI;
using UnityEngine;

public abstract class Tab : MonoBehaviour
{
    public string tabName; 
    [SerializeField] private int containerOrder;
    public int ContainerOrder { get { return containerOrder; } }
    [HideInInspector] public Vector2 containerPosition; 
    
    private RectTransform panelRect;

    public virtual void Initialize(Vector2 size)
    {
        panelRect = GetComponent<RectTransform>();
        containerPosition = size.x * containerOrder * Vector2.right;
        panelRect.anchoredPosition = containerPosition;
    }
}
[System.Serializable]
public class PanelSelector {
    public Image image;
    public TextMeshProUGUI text;
    public Sprite activeSprite, inactiveSprite;
    public TMPro.TMP_FontAsset activeFont, inactiveFont;

    public void SelectPanel() {
        if (activeSprite)
            image.sprite = activeSprite;
        if (activeFont)
            text.font = activeFont;
    }
    public void DeselectPanel() {
        if (inactiveSprite)
            image.sprite = inactiveSprite;
        if (inactiveFont)
            text.font = inactiveFont;
    }
}