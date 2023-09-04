using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class InGameManager : MonoSingleton<InGameManager>
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GridManager gridManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] GameObject testBlocks;

    public GameObject BlockPrefab { get { return blockPrefab; } }
    public GridManager GridManager { get { return gridManager; } }
    public ButtonManager ButtonManager { get { return buttonManager; } }

    public bool IsPlayable { get; set; }
    private void Start() {
        IsPlayable = false;
        testBlocks.SetActive(false);
        EventRunner.LoadSceneFinish();
        MainManager.Instance.MenuManager.InGamePanel.Appear();
        gridManager.Initialize();
        buttonManager.Initialize();
    }
}
