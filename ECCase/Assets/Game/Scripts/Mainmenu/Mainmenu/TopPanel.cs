using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;  
    [SerializeField] private TextMeshProUGUI levelText;  
    [SerializeField] private Image levelImage;  
    [SerializeField] private TextMeshProUGUI healthCountText;  
    public void Initialize() {
        SetGold();
        SetHealth();
        SetLevel();
    }
    public void SetGold() {
        //goldText.text = MainManager.Instance.CurrencyManager.currencies[0].currencyAmount.ToString();
        goldText.text = MainManager.Instance.CoinManager.GetCoin().ToString();
    } 
    public void SetHealth() {
        healthCountText.text = "  ";
        healthCountText.text += "FULL";
    }
    public void SetLevel() {
        levelText.text = Random.Range(0, 25).ToString();
        levelImage.fillAmount = Random.Range(0f, 0.85f);
    }
}
