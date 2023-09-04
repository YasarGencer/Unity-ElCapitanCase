using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class GridManager : MonoBehaviour {
    BlockController[][] grid;
    public BlockController[][] Grid { get { return grid; } }
    [SerializeField] private Transform gridContainer;

    [SerializeField] GameObject[] positions;
    public void Initialize() {
        DOTween.SetTweensCapacity(500, 500);
        grid = new BlockController[6][];
        for (int i = 0; i < 6; i++) {
            grid[i] = new BlockController[6];  
        }
        //DebugWholeGrid(); 
        Invoke("PlaceOnGrid", 1f); 
    } 
    // CheckGrid => MovingBitsDown => PlaceNewBits
    void PlaceOnGrid() { 
        for (int i = 0; i < 6; i++) { // x axis
            for (int j = 0; j < 6; j++) { // y axis 
                if (grid[j][i] == null) {
                    grid[j][i] = Instantiate(InGameManager.Instance.BlockPrefab, gridContainer).GetComponent<BlockController>();
                    grid[j][i].Initialize(ConvertGridToPos(j, i), FindOpenLeg(j, i));
                }
            }
        }
        Invoke("CheckGrid", .5f);
    }
    void CheckGrid() {
        bool didPop = false;
        bool[][] controlled = InitializeBoolArray();

        List<int2> list = new List<int2>();
        for (int i = 0; i < 6; i++) // x axis
            for (int j = 0; j < 6; j++) { // y axis
                if (controlled[j][i])
                    continue;
                controlled[j][i] = true;
                list.Clear();
                list.Add(new int2(j, i));
                int counter = 0;
                int currentBit = grid[j][i].OpenBit;

                int stackOverflow = 0;
                //check borders
                while (counter < list.Count) {
                    if (stackOverflow > 5)
                        break;
                    stackOverflow++;
                    //Debug.Log("----------------");
                    bool[][] notMatched = InitializeBoolArray();
                    for (int x = list[counter].y - 1; x < list[counter].y + 2; x++) // neighbours for loops 
                        for (int y = list[counter].x - 1; y < list[counter].x + 2; y++)
                            if (y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null) // making sure it is in the array
                                if (notMatched[y][x] == false) // do not check the ones that checked as a different colour            
                                    if (currentBit == grid[y][x].OpenBit) {
                                        //Debug.Log("MATCH => " + x + ", " + y + " for =>" + i + " ," + j);
                                        list.Add(new int2(y, x));
                                        controlled[y][x] = true;
                                        counter++;
                                        stackOverflow = 0;
                                    } else {
                                        //Debug.Log("NOT A MATCH => " + x + ", " + y + " for =>" + i + " ," + j);
                                        notMatched[y][x] = true;
                                    }
                }
                if (list.Count > 5)
                    foreach (var item in list) {
                        RemoveFromGrid(item.x, item.y);
                        didPop = true;
                    }
            }
        if (didPop)
            Invoke("MovingBitsDown", .5f);
        else {
            PlayersTurn();
        }

    }
    void MovingBitsDown() {
        bool hasMovedThisLoop = true;
        bool didMove = false;
        while (hasMovedThisLoop) {
            hasMovedThisLoop = false;
            for (int i = 0; i < 6; i++) // x axis
                for (int j = 1; j < 6; j++) { // y axis
                    if (grid[j][i] != null) {
                        if (grid[j - 1][i] == null) {
                            MoveBit(j, i);
                            hasMovedThisLoop = true;
                            didMove = true;
                        }
                    }

                }
        }
        if (didMove)
            Invoke("PlaceOnGrid", .5f);
        else {
            PlayersTurn();
        }
    }


    void PlayersTurn() {
        InGameManager.Instance.IsPlayable = true;
        InGameManager.Instance.ButtonManager.PlayableAnim();
    }

    public void RotateButtonPressed(List<int2> positions) {
        Rotate4Bit(positions); 
        Invoke("CheckGrid", .5f);
    }

    Vector2 ConvertGridToPos(int x, int y) {
        return positions[x * 6 + y].transform.position;
    }
    int FindOpenLeg(int j, int i) {
        if (j % 2 == 0) {
            if (i % 2 == 0)
                return 0;
            return 2;
        } else {
            if (i % 2 == 0)
                return 1;
            return 3;
        }
    }
    void DebugWholeGrid() {
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 6; j++) {
                string type = "red";
                switch (grid[j][i].OpenBit) {
                    case 0:
                        type = "red";
                        break;
                    case 1:
                        type = "blue";
                        break;
                    case 2:
                        type = "yellow";
                        break;
                    case 3:
                        type = "green";
                        break;
                    default:
                        break;
                }
                Debug.Log("Position " + i + ", " + j + " => " + type);
            }
    }
    void RemoveFromGrid(int j, int i) {
        if (grid[j][i] == null)
            return;
        grid[j][i].Pop();
        grid[j][i] = null;
    }
    bool[][] InitializeBoolArray() {

        bool[][] array = new bool[6][];
        for (int p = 0; p < 6; p++) {
            array[p] = new bool[6];
        }
        return array;
    }
     void MoveBit(int j, int i) {
        if (grid[j][i] == null)
            return;
        grid[j][i].Move(ConvertGridToPos(j - 1, i));
        grid[j - 1][i] = grid[j][i];
        grid[j][i] = null;
     }
    void Rotate4Bit(List<int2> positions) {

        grid[positions[0].x][positions[0].y].Move(ConvertGridToPos(positions[3].x, positions[3].y));
        grid[positions[1].x][positions[1].y].Move(ConvertGridToPos(positions[0].x, positions[0].y));
        grid[positions[2].x][positions[2].y].Move(ConvertGridToPos(positions[1].x, positions[1].y));
        grid[positions[3].x][positions[3].y].Move(ConvertGridToPos(positions[2].x, positions[2].y));

        BlockController temp = grid[positions[0].x][positions[0].y];
        grid[positions[0].x][positions[0].y] = grid[positions[1].x][positions[1].y];
        grid[positions[1].x][positions[1].y] = grid[positions[2].x][positions[2].y];
        grid[positions[2].x][positions[2].y] = grid[positions[3].x][positions[3].y];
        grid[positions[3].x][positions[3].y] = temp;


    }
}