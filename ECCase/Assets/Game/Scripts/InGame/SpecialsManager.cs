using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialsManager : MonoBehaviour
{
    [SerializeField] GameObject special1, special2;
    [SerializeField] GameObject speacialEffect1;
    [SerializeField] int neededLevelFor1 = 3, neededLevelFor2 = 10;
    [SerializeField] int price1 = 3, price2 = 10;

    [SerializeField] PlacementController placementController;

    int selectedSpecial;

    public void Initialize() {
        if (MainManager.Instance.LevelManager.GetLevel() >= neededLevelFor1)
            MainManager.Instance.MenuManager.InGamePanel.OpenSpecial(1);
        else
            MainManager.Instance.MenuManager.InGamePanel.CloseSpecial(1);
        if (MainManager.Instance.LevelManager.GetLevel() >= neededLevelFor2)
            MainManager.Instance.MenuManager.InGamePanel.OpenSpecial(2);
        else
            MainManager.Instance.MenuManager.InGamePanel.CloseSpecial(2);

        MainManager.Instance.MenuManager.InGamePanel.UpdateSpecialUIInfo(neededLevelFor1, neededLevelFor2, price1, price2, Press1, Press2); 

        placementController.Initialize();
    }
    void Press1() {
        if (InGameManager.Instance.IsPlayable == false)
            return;
        if (MainManager.Instance.CoinManager.Spend(price1) == false) {
            MainManager.Instance.MenuManager.InGamePanel.SpecialPunch(1, false);
            return;
        }
        SpecialPress(1);
    }
    void Press2() {
        if (InGameManager.Instance.IsPlayable == false)
            return;
        if (MainManager.Instance.CoinManager.Spend(price2) == false) {
            MainManager.Instance.MenuManager.InGamePanel.SpecialPunch(2, false);
            return;
        }
        SpecialPress(2);
    }
    public void SpecialPress(int type) {
        selectedSpecial = type;
        InGameManager.Instance.IsPlayable = false;
        OpenPlacings();
        MainManager.Instance.MenuManager.InGamePanel.SpecialPunch(type, true);
    } 
    void OpenPlacings() {
        placementController.Open();
    }
    public void SpecialAttack(int2 gridPos, Vector3 spawnPos) {
        placementController.Close();
        InGameManager.Instance.ButtonManager.Played();
        switch (selectedSpecial) {
            case 1:
                GameObject bomb = Instantiate(special1, new Vector3(spawnPos.x, spawnPos.y, -1), Quaternion.identity);

                Vector2 scale = bomb.transform.localScale;

                bomb.transform.DOScale(scale * .75f, .25f).OnComplete(()=> bomb.transform.DOScale(scale * 1.5f, .15f)).OnComplete(()=> {
                    bomb.transform.DOScale(0, .05f);

                    Instantiate(speacialEffect1, bomb.transform.position, Quaternion.identity);

                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x, gridPos.y); 
                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x + 1, gridPos.y); 
                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x - 1, gridPos.y); 
                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x, gridPos.y + 1); 
                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x, gridPos.y - 1);

                    bomb.transform.DOScale(0, .5f).OnComplete(() => InGameManager.Instance.GridManager.MovingBitsDown());

                }); 
                break; 
            case 2:
                GameObject rocket = Instantiate(special2, new Vector3(spawnPos.x, spawnPos.y, -1), Quaternion.identity);

                Vector2 scale2 = rocket.transform.localScale;

                rocket.transform.GetChild(0).DOLocalMove(new Vector3(50,0), 1f).SetEase(Ease.InCirc);
                rocket.transform.GetChild(1).DOLocalMove(new Vector3(0,50), 1f).SetEase(Ease.InCirc);
                rocket.transform.GetChild(2).DOLocalMove(new Vector3(-50,0), 1f).SetEase(Ease.InCirc);
                rocket.transform.GetChild(3).DOLocalMove(new Vector3(0, -50), 1f).SetEase(Ease.InCirc);

                for (int i = 0; i < 6; i++) {
                    InGameManager.Instance.GridManager.RemoveFromGrid(gridPos.x, i);
                    InGameManager.Instance.GridManager.RemoveFromGrid(i, gridPos.y);
                }
                rocket.transform.DOScale(rocket.transform.localScale, 1f).OnComplete(() => InGameManager.Instance.GridManager.MovingBitsDown()); 
                break;
            default:
                break;
        }
    } 
}
