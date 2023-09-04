using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;  

public class MainManager : MonoSingleton<MainManager>
{    
    [SerializeField] private GameManager gameManager; 
    [SerializeField] private MenuManager menuManager; 
    [SerializeField] private CoinManager coinManager; 
    [SerializeField] private LevelManager levelManager; 
	 
	private EventManager eventManager; 

	private string lastLoadedScene = "";
	private GameObject lastLoadedScenePrefab;
	 
    public GameManager GameManager { get => gameManager; } 
    public MenuManager MenuManager { get => menuManager; } 
    public CoinManager CoinManager { get => coinManager; } 
    public LevelManager LevelManager { get => levelManager; } 
    public EventManager EventManager { get => eventManager; } 
    public string LastLoadedScene { get => lastLoadedScene; set => lastLoadedScene = value; }
    public GameObject LastLoadedScenePrefab { get => lastLoadedScenePrefab; set => lastLoadedScenePrefab = value; } 
	private void Awake()
	{
		 
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		Initialize();
	}


	public void Initialize()
	{ 
		eventManager = new EventManager();
		eventManager.Initialize();
        gameManager.Initialize(); 
		menuManager.Initialize();
		coinManager.Initialize();
        levelManager.Initialize();

        EventRunner.LoadSceneStart();
        SceneManager.LoadScene("MainmenuScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("InGameScene", LoadSceneMode.Additive);
	}
	public void UnloadScene(string sceneName)
	{
		StartCoroutine(UnloadRoutine(sceneName));
	}

	public IEnumerator UnloadRoutine(string sceneName)
	{
		yield return new WaitForSeconds(1f);
		SceneManager.UnloadScene(sceneName);
	}
} 