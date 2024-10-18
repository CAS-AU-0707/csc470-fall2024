using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Renderer cubeRenderer;
    public Color p1Color;
    public Color p2Color;
    public Color p3Color;
    public Color p4Color;
    public Color deadColor;
    public Color borderColor;

    public int xIndex;
    public int yIndex;
    public int player = 0;
    public bool isBorder = false;  // Flag to check if the box is part of the middle row/column

    BoxMan boxm;

    void Start()
    {
        SetC(player);

        GameObject gmObj = GameObject.Find("GameManager");
        boxm = gmObj.GetComponent<BoxMan>();
    }

    void Update()
    {

    }

    // Handle mouse click for selecting boxes before the game starts
    void OnMouseDown()
    {
        if (!boxm.gameStarted && player == 0 && IsValidSelection(xIndex, yIndex))  // Allow selection only if the box is dead and in a valid area
        {
            int currentPlayer = DetermineCurrentPlayer();  // Get the player number who is selecting
            if (boxm.playerSelections[currentPlayer - 1] < 15)
            {
                SetC(currentPlayer);  // Set the color to the player's color
                boxm.PlayerBoxSelected(currentPlayer);
            }
        }
    }

    // Determines the current player based on the number of selections already made
    int DetermineCurrentPlayer()
    {
        for (int i = 0; i < 4; i++)
        {
            if (boxm.playerSelections[i] < 15) return i + 1;
        }
        return 1;  // Default to player 1 if no other valid selections
    }

    // Set the box color based on the player number
    public void SetC(int p)
    {
        player = p;  // Update player ownership
        switch (player)
        {
            case 1:
                cubeRenderer.material.color = p1Color;
                break;
            case 2:
                cubeRenderer.material.color = p2Color;
                break;
            case 3:
                cubeRenderer.material.color = p3Color;
                break;
            case 4:
                cubeRenderer.material.color = p4Color;
                break;
            default:
                cubeRenderer.material.color = deadColor;
                break;
        }
    }

    // Set a box as a border cell (middle row/column)
    public void SetBorder()
    {
        cubeRenderer.material.color = borderColor;
        player = -1;  // Mark this as a border cell, initially unselectable
        isBorder = true;  // Mark it as part of the middle row/column
    }

    // Allow the middle row/column to change color after game starts
    public void ActivateBorder()
    {
        if (isBorder)
        {
            SetC(0);  // Change the color to dead and allow it to be modified in the game
        }
    }

    // Check if the box is in a valid selection area based on the current player
    bool IsValidSelection(int x, int y)
    {
        if (isBorder) return false;  // Cannot select border cells

        int currentPlayer = DetermineCurrentPlayer();
        // Define valid areas for each player
        switch (currentPlayer)
        {
            case 1:
                return x < 25 && y < 25;  // Player 1: Top-left corner
            case 2:
                return x >= 76 && y < 25;  // Player 2: Top-right corner
            case 3:
                return x < 25 && y >= 76;  // Player 3: Bottom-left corner
            case 4:
                return x >= 76 && y >= 76;  // Player 4: Bottom-right corner
            default:
                return false;
        }
    }
}
