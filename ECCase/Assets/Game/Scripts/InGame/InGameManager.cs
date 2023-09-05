using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InGameManager : MonoSingleton<InGameManager>
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GridManager gridManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] SpecialsManager specialsManager;
    [SerializeField] GameObject testBlocks;

    [SerializeField] LevelSettings currentLevel;

    public GameObject BlockPrefab { get { return blockPrefab; } }
    public GridManager GridManager { get { return gridManager; } }
    public ButtonManager ButtonManager { get { return buttonManager; } }
    public SpecialsManager SpecialsManager { get { return specialsManager; } }
    public LevelSettings LevelSettings { get { return currentLevel; } }

    public bool IsPlayable { get; set; }
    private void Start() {
        IsPlayable = false;
        testBlocks.SetActive(false);
        currentLevel = Instantiate(MainManager.Instance.LevelManager.GetLevelSetting());
        EventRunner.LoadSceneFinish();
        MainManager.Instance.MenuManager.InGamePanel.Appear();
        gridManager.Initialize();
        buttonManager.Initialize();
        specialsManager.Initialize();
    }
    public void UpdateMove() {
        currentLevel.MoveCount -= 1;
        if(currentLevel.MoveCount <= 0) {
            //lose level
            MainManager.Instance.MenuManager.WinFailPanel.LosePanel();
        }
        MainManager.Instance.MenuManager.InGamePanel.MoveCount(currentLevel.MoveCount);
    }
    public void UpdateGoal(GoalType type) {
        foreach (var item in currentLevel.Goals) {
            if(item.Type == type) {
                item.Counter++;
            }
        }
        int goalsCompleted = 0;
        for (int i = 0; i < currentLevel.Goals.Count; i++) {
            if (currentLevel.Goals[i].Counter >= currentLevel.Goals[i].Count)
                goalsCompleted++;
        }
        if(goalsCompleted >= currentLevel.Goals.Count) {
            //win level
            MainManager.Instance.MenuManager.WinFailPanel.WinPanel();
            MainManager.Instance.LevelManager.NextLevel();
        }
    }
}
