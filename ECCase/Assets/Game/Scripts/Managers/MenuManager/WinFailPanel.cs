using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinFailPanel : PanelManager
{ 
    [SerializeField] panel winPanel, losePanel;

    [SerializeField] TextMeshProUGUI coinText, increaseText;
    [System.Serializable]
    struct panel {
        public GameObject Panel;
        public GameObject Header;
        public GameObject HeaderAnim;
        public GameObject TopIcon;
        public Button Button;
    }

    Vector2 increaseScale;
    public override void Initialize() {
        base.Initialize();
        winPanel.Button.onClick.AddListener(() => NextButton());
        losePanel.Button.onClick.AddListener(() => NextButton());
        increaseScale = increaseText.transform.localScale;
    }

    public async void LosePanel() {
        await Task.Delay(1000);
        Appear();
        OpenPanel(losePanel);
    }

    public async  void WinPanel() {
        await Task.Delay(1000);
        Appear();
        OpenPanel(winPanel);

        int increase = 100 + (InGameManager.Instance.LevelSettings.MoveCount * 5); 
        coinText.text = MainManager.Instance.CoinManager.GetCoin().ToString();
        MainManager.Instance.CoinManager.Gain(increase);

        increaseText.text = "+" + increase.ToString();
        increaseText.gameObject.SetActive(true); 
        increaseText.transform.localScale = Vector2.zero;
        increaseText.transform.DOScale(increaseScale * 1.2f, .5f)
            .OnComplete(() => increaseText.transform.DOScale(increaseScale, .25f)
                .OnComplete(() => {
                    increaseText.transform.DOScale(increaseScale, .25f);
                    Vector2 scale = coinText.transform.localScale;
                    coinText.transform.DOScale(scale * 1.2f, .5f)
                        .OnComplete(() => {
                            coinText.transform.DOScale(scale, .25f);
                            coinText.text = MainManager.Instance.CoinManager.GetCoin().ToString();
                            increaseText.transform.DOScale(Vector2.zero, .25f);
                        });    
                }));

    }
    void OpenPanel(panel panel) { 
        winPanel.Panel.SetActive(false);
        losePanel.Panel.SetActive(false);
        panel.Panel.SetActive(true);

        Vector2 scale = panel.TopIcon.transform.localScale;
        panel.TopIcon.transform.DOScale(Vector2.zero, 0f);
        panel.TopIcon.transform.DOScale(scale * 1.2f, .5f).OnComplete(() => panel.TopIcon.transform.DOScale(scale , .25f));

        Vector3 position = panel.Header.transform.localPosition;
        Vector3 position2 = panel.HeaderAnim.transform.localPosition; 
        panel.Header.transform.DOLocalMove(new Vector3(-750f, position.y, position.z), 0f);
        panel.Header.transform.DOLocalMove(position, .25f).SetEase(Ease.Linear)
            .OnComplete(() => panel.HeaderAnim.transform.DOLocalMove(new Vector3(1000f, position2.y, position2.z), 1f)
               .OnComplete(() => panel.HeaderAnim.transform.DOLocalMove(position2, 1f)));

        Vector2 scale2 = panel.Button.transform.localScale;
        panel.Button.transform.DOScale(Vector2.zero, 0f);
        panel.Button.transform.DOScale(scale2 * 1.2f, .5f).OnComplete(() => panel.Button.transform.DOScale(scale2, .25f)); 
    }
    public void NextButton() {
        Disappear(true);
        winPanel.Panel.SetActive(false);
        losePanel.Panel.SetActive(false);
         
        EventRunner.LoadSceneStart();
        SceneManager.LoadScene("MainmenuScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("InGameScene");
    }
}
