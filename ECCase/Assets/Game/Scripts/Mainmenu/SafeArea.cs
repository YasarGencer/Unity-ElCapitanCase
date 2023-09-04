using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private float offset;
    private RectTransform rectTransform;
    private Rect safeArea;
    private Vector2 maxAnchor;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        float height = safeArea.yMin + safeArea.height;  
        maxAnchor = Screen.safeArea.position + safeArea.size;
        if (Mathf.Abs(height - Screen.height) > 10f && height < Screen.height)
        {
            maxAnchor += Vector2.up * offset;
        }
        
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMax = maxAnchor;
    }
}
