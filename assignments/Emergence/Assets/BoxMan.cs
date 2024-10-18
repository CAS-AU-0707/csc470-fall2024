using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMan : MonoBehaviour
{
    public GameObject cellPrefab;

    Box[,] grid;
    int gridx = 101;
    int gridy = 101;
    float spacing = 1.1f;
    float simulationTimer;
    float simulationRate = .1f;
    int iterations = 300;
    
    public int[] playerSelections = new int[4]; // Tracks how many boxes each player has selected
    public bool gameStarted = false;

    void Start()
    {
        simulationTimer = simulationRate;

        grid = new Box[gridx, gridy];
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing;
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                grid[x, y] = cell.GetComponent<Box>();

                grid[x, y].xIndex = x;
                grid[x, y].yIndex = y;

                if (x == 50 || y == 50)
                {
                    // Create middle border at the 51st row and column
                    grid[x, y].SetBorder();
                }
                else
                {
                    // Set initial cells as blank (dead)
                    grid[x, y].SetC(0);
                }
            }
        }
    }

    void Update()
    {
        // Game will not start until all players have selected 15 boxes and spacebar is pressed
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) && AllPlayersSelected())
            {
                gameStarted = true;
                EnableMiddleRow();  // Turn middle row into dead cells
            }
        }
        else
        {
            if (iterations > 0)
            {
                simulationTimer -= Time.deltaTime;
                if (simulationTimer < 0)
                {
                    Simulate();  // Evolve the grid based on the rules
                    simulationTimer = simulationRate;
                    iterations--;
                }
            }
        }
    }

    // Check if all players have selected 15 boxes
    bool AllPlayersSelected()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerSelections[i] < 15) return false;
        }
        return true;
    }

    // Enables the middle row after the spacebar is pressed
    void EnableMiddleRow()
    {
        for (int x = 0; x < gridx; x++)
        {
            grid[x, 50].SetC(0);  // Enable the middle row
        }
        for (int y = 0; y < gridy; y++)
        {
            grid[50, y].SetC(0);  // Enable the middle column
        }
    }

    void Simulate()
    {
        int[,] nextState = new int[gridx, gridy];

        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                int currentPlayer = grid[x, y].player;
                int[] neighbors = CountNeighbors(x, y);

                // Reproduction for dead cells
                if (currentPlayer == 0)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        if (neighbors[i] == 3)
                        {
                            nextState[x, y] = i;  // Dead cell becomes the player's color
                        }
                    }
                }
                else
                {
                    // Combat: Check for color dominance
                    for (int i = 1; i <= 4; i++)
                    {
                        if (i != currentPlayer && neighbors[i] == neighbors[currentPlayer] + 1)
                        {
                            nextState[x, y] = i;  // Overpowered by another player's color
                        }
                        else
                        {
                            nextState[x, y] = currentPlayer;  // No change if no overpowering
                        }
                    }
                }
            }
        }

        // Apply the new states
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                grid[x, y].SetC(nextState[x, y]);
            }
        }
    }

    public int[] CountNeighbors(int xIndex, int yIndex)
    {
        int[] counts = new int[5];  // Indexes 1-4 for players, index 0 for dead cells
        for (int x = xIndex - 1; x <= xIndex + 1; x++)
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {
                if (x >= 0 && x < gridx && y >= 0 && y < gridy && !(x == xIndex && y == yIndex))
                {
                    counts[grid[x, y].player]++;
                }
            }
        }
        return counts;  // Returns an array with counts for each player (index 1-4)
    }

    // Method to update player selection count when a box is clicked
    public void PlayerBoxSelected(int player)
    {
        playerSelections[player - 1]++;
    }
}
