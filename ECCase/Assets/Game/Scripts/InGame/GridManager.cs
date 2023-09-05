using DG.Tweening;
using System;
using System.Collections.Generic; 
using Unity.Mathematics; 
using UnityEngine; 

public class GridManager : MonoBehaviour {
    BlockController[][] grid;
    public BlockController[][] Grid { get { return grid; } }
    [SerializeField] private Transform gridContainer;

    [SerializeField] GameObject[] positions;
    public void Initialize() { 
        grid = new BlockController[6][];
        for (int i = 0; i < 6; i++) {
            grid[i] = new BlockController[6];  
        }
        //DebugWholeGrid(); 
        PlaceLevelSettings();
        Invoke("PlaceOnGrid", 1f); 
    } 
    // CheckGrid => MovingBitsDown => PlaceNewBits
    void PlaceLevelSettings() {
        for (int i = 0; i < InGameManager.Instance.LevelSettings.Goals.Count; i++) {
            GameObject prefab = InGameManager.Instance.LevelSettings.Goals[i].Prefab;
            for (int j = 0; j < InGameManager.Instance.LevelSettings.Goals[i].Positions.Count; j++) {
                GoalController goal = Instantiate(prefab, gridContainer).GetComponent<GoalController>();
                int x = InGameManager.Instance.LevelSettings.Goals[i].Positions[j].x;
                int y = InGameManager.Instance.LevelSettings.Goals[i].Positions[j].y;
                grid[x][y] = goal;
                goal.Initialize(ConvertGridToPos(x, y), FindOpenLeg(x, y), x, y);
            }
        }
    }
    void PlaceOnGrid() {
        bool isPlaced = false;
        for (int i = 0; i < 6; i++) { // x axis
            for (int j = 0; j < 6; j++) { // y axis 
                if (grid[j][i] == null) {
                    grid[j][i] = Instantiate(InGameManager.Instance.BlockPrefab, gridContainer).GetComponent<BlockController>();
                    grid[j][i].Initialize(ConvertGridToPos(j, i), FindOpenLeg(j, i));
                    isPlaced = true;
                }
            }
        }
        if (isPlaced) {
            Invoke("CheckGrid", .5f);
        } else {
            PlayersTurn();
        }
    }
    void CheckGrid() {
        bool didPop = false;
        bool[][] controlled = InitializeBoolArray();

        List<int2> list = new List<int2>();
        for (int i = 0; i < 6; i++) // x axis
            for (int j = 0; j < 6; j++) { // y axis
                if (controlled[j][i])
                    continue;
                if (grid[j][i] == null)
                    continue;
                //controlled[j][i] = true;
                list.Clear();
                list.Add(new int2(j, i));
                int currentBit = grid[j][i].OpenBit;

                //check borders
                for (int k = 0; k < list.Count; k++) {
                    bool[][] notMatched = InitializeBoolArray();
                    int2 pos = list[k];

                    int x, y;
                    //CHECK FOUR CORNERS
                    y = pos.x - 1;
                    x = pos.y;
                    if(y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null)  
                        if (notMatched[y][x] == false)            
                        if (currentBit == grid[y][x].OpenBit) { 
                            list.Add(new int2(y, x));
                            controlled[y][x] = true;  
                        } else { 
                            notMatched[y][x] = true;
                        } 
                    y = pos.x;
                    x = pos.y - 1;
                    if (y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null)
                        if (notMatched[y][x] == false)
                            if (currentBit == grid[y][x].OpenBit) {
                                list.Add(new int2(y, x));
                                controlled[y][x] = true;
                            } else {
                                notMatched[y][x] = true;
                            }
                    y = pos.x + 1;
                    x = pos.y;
                    if (y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null)
                        if (notMatched[y][x] == false)
                            if (currentBit == grid[y][x].OpenBit) {
                                list.Add(new int2(y, x));
                                controlled[y][x] = true;
                            } else {
                                notMatched[y][x] = true;
                            }
                    y = pos.x;
                    x = pos.y + 1;
                    if (y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null)
                        if (notMatched[y][x] == false)
                            if (currentBit == grid[y][x].OpenBit) {
                                list.Add(new int2(y, x));
                                controlled[y][x] = true;
                            } else {
                                notMatched[y][x] = true;
                            } 
                    //CHECK FOUR CORNERS
                }
                /*
                bool found = true;
                int stackOverflow = 0;
                int counter = 0;
                while (found) {
                    found = false;
                    if (stackOverflow > 5)
                        break;
                    stackOverflow++;
                    //Debug.Log("----------------");
                    bool[][] notMatched = InitializeBoolArray();
                    int2 pos = list[counter];
                    for (int x = pos.y - 1; x < pos.y + 2; x++) // neighbours for loops 
                        for (int y = pos.x - 1; y < pos.x + 2; y++) 
                                    if (y < 6 && x < 6 && y >= 0 && x >= 0 && list.Contains(new int2(y, x)) == false && grid[y][x] != null) // making sure it is in the array
                                        if (notMatched[y][x] == false) // do not check the ones that checked as a different colour            
                                            if (currentBit == grid[y][x].OpenBit) {
                                                //Debug.Log("MATCH => " + x + ", " + y + " for =>" + i + " ," + j);
                                                list.Add(new int2(y, x));
                                                controlled[y][x] = true;
                                                counter++;
                                                stackOverflow = 0;
                                                found = true;
                                            } else {
                                                //Debug.Log("NOT A MATCH => " + x + ", " + y + " for =>" + i + " ," + j);
                                                notMatched[y][x] = true;
                                            }
                }
                */
                if (list.Count >= 4)
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
    public void MovingBitsDown() {
        bool hasMovedThisLoop = true; 
        while (hasMovedThisLoop) {
            hasMovedThisLoop = false;
            for (int i = 0; i < 6; i++) // x axis
                for (int j = 1; j < 6; j++) { // y axis
                    if (grid[j][i] != null) {
                        if (grid[j - 1][i] == null) {
                            if (grid[j][i].GetComponent<GoalController>() as GoalController == null) {
                                MoveBit(j, i);
                                hasMovedThisLoop = true; 
                            }
                        }
                    }

                }
        } 
        Invoke("PlaceOnGrid", .5f); 
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
    public void RemoveFromGrid(int j, int i) {
        if (j > 6 || i > 6 || i < 0 || j < 0)
            return;
        if (grid[j][i] == null)
            return;
        if (grid[j][i].GetComponent<GoalController>()) {
            grid[j][i].GetComponent<GoalController>()?.Pop();
            grid[j][i] = null;
        }
        else {
            grid[j][i].Pop();
            grid[j][i] = null;
            //check corners for goals
            int a = i - 1;
            int b = j;
            if (b >= 0 && a >= 0 && a < 6 && b < 6)
                if (grid[b][a] != null) {
                    grid[b][a].GetComponent<GoalController>()?.Pop();
                }
            a = i;
            b = j - 1;
            if (b >= 0 && a >= 0 && a < 6 && b < 6)
                if (grid[b][a] != null) {
                    grid[b][a].GetComponent<GoalController>()?.Pop();
                }
            a = i + 1;
            b = j;
            if (b >= 0 && a >= 0 && a < 6 && b < 6)
                if (grid[b][a] != null) {
                    grid[b][a].GetComponent<GoalController>()?.Pop();
                }
            a = i;
            b = j + 1;
            if (b >= 0 && a >= 0 && a < 6 && b < 6)
                if (grid[b][a] != null) {
                    grid[b][a].GetComponent<GoalController>()?.Pop();
                }
        }
    }
    public void RemoveFromGrid2(int x, int y) {
        grid[x][y] = null;
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

    public bool IsGoal(int2 position) {
        return grid[position.x][position.y].gameObject.GetComponent<GoalController>() as GoalController != null;
    }
}