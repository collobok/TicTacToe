using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoxGameController : MonoBehaviour
{
    public int whoesTurn;
    public int turnCounter;
    public Sprite[] playerIcons;
    public Button[] gameCells;
    public GameObject[] turnIcons;
    public int[,] markedCells = new int[8, 8];
    public Text winnerText;
    public GameObject winnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameSetup();
    }

    void gameSetup()
    {
        whoesTurn = 0;
        turnCounter = 0;

        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        turnIcons[2].SetActive(false);
        for (int i = 0; i < gameCells.Length; i++)
        {
            gameCells[i].interactable = true;
            gameCells[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                markedCells[i, j] = -1;
            }
        }
    }

    public void cellClick(int whichCell)
    {
        gameCells[whichCell].image.sprite = playerIcons[whoesTurn];
        gameCells[whichCell].interactable = false;
        
        markedCells[whichCell/8, whichCell%8] = whoesTurn;
        //Debug.Log(markedCells[0, 0]);
        turnCounter++;
        winnerCheck();

        if (whoesTurn == 0)
        {
            whoesTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
            turnIcons[2].SetActive(false);
        }
        else if (whoesTurn == 1)
        {
            whoesTurn = 2;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(false);
            turnIcons[2].SetActive(true);
        }
        else if (whoesTurn == 2)
        {
            whoesTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
            turnIcons[2].SetActive(false);
        }
    }

    void winnerCheck()
    {
        int cellCounter = 0;
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                
                if (markedCells[r, c] != -1)
                {
                    //up
                    if (r >= 3)
                    {
                        for (int up = 1; up <= 3; up++)
                        {
                            if (markedCells[r, c] == markedCells[r - up, c])
                            {
                                cellCounter++;
                            }
                        }

                        if (cellCounter == 3)
                        {
                            winnerDisplay();
                            return;
                        }
                        cellCounter = 0;
                    }
                    
                    //down
                    if (r <= 4)
                    {
                        for (int down = 1; down <= 3; down++)
                        {
                            if (markedCells[r, c] == markedCells[r + down, c])
                            {
                                cellCounter++;
                            }
                        }
                        if (cellCounter == 3)
                        {
                            winnerDisplay();
                            return;
                        }
                        cellCounter = 0;
                    }

                    //left
                    if (c >= 3)
                    {
                        for (int left = 1; left <= 3; left++)
                        {
                            if (markedCells[r, c] == markedCells[r, c - left])
                            {
                                cellCounter++;
                            }
                        }
                        if (cellCounter == 3)
                        {
                            winnerDisplay();
                            return;
                        }
                        cellCounter = 0;
                    }
                    
                    //right
                    if (c <= 4)
                    {
                        for (int right = 1; right <= 3; right++)
                        {
                            if (markedCells[r, c] == markedCells[r, c + right])
                            {
                                cellCounter++;
                            }
                        }
                        if (cellCounter == 3)
                        {
                            winnerDisplay();
                            return;
                        }
                        cellCounter = 0;
                    }

                    //top left
                    if (r >= 3 && c >=3)
                    {
                        for (int topLeft = 1; topLeft <= 3; topLeft++)
                        {
                            if (markedCells[r, c] == markedCells[r - topLeft, c - topLeft])
                            {
                                cellCounter++;
                            }
                        }
                    }
                    if (cellCounter == 3)
                    {
                        winnerDisplay();
                        return;
                    }
                    cellCounter = 0;

                    //top right
                    if (r >= 3 && c <= 4)
                    {
                        for (int topRight = 1; topRight <= 3; topRight++)
                        {
                            if (markedCells[r, c] == markedCells[r - topRight, c + topRight])
                            {
                                cellCounter++;
                            }
                        }
                    }

                    if (cellCounter == 3)
                    {
                        winnerDisplay();
                        return;
                    }
                    cellCounter = 0;

                    //down left
                    if (r <= 4 && c >= 3)
                    {
                        for (int bottomLeft = 1; bottomLeft <= 3; bottomLeft++)
                        {
                            if (markedCells[r, c] == markedCells[r + bottomLeft, c - bottomLeft])
                            {
                                cellCounter++;
                            }
                        }
                    }
                    if (cellCounter == 3)
                    {
                        winnerDisplay();
                        return;
                    }
                    cellCounter = 0;

                    //down right
                    if (r <= 4 && c <= 4)
                    {
                        for (int bottomRight = 1; bottomRight <= 3; bottomRight++)
                        {
                            if (markedCells[r, c] == markedCells[r + bottomRight, c + bottomRight])
                            {
                                cellCounter++;
                            }
                        }
                    }
                    if (cellCounter == 3)
                    {
                        winnerDisplay();
                        return;
                    }
                    cellCounter = 0;
                }
            }
        }
    }

    void winnerDisplay()
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoesTurn == 0)
        {
            winnerText.text = "Player " + "V" + " Wins!";
        }
        else if (whoesTurn == 1)
        {
            winnerText.text = "Player " + "O" + " Wins!";
        }
        else if (whoesTurn == 2)
        {
            winnerText.text = "Player " + "X" + " Wins!";
        }
        /*
        foreach (Button element in gameCells)
        {
            element.interactable = false;
        }
        */
    }

    public void Rematch()
    {
        gameSetup();
        winnerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
