using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePart : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public List<GameObject> neighbours = new List<GameObject>();
    public int uniqueInt;
    private GameManager gameManager;
    private int width;
    public int destroyCount;
    public GameObject gateMaze;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uniqueInt = int.Parse(gameObject.name);
        width = (int) gameManager.width;
        AssignNeighbours();
    }

    
    private void AssignNeighbours()
    {
        //Determines the neighbours of the mazeparts. The if-statements are used for the corners and borders of the maze.
        //Those parts have different neighbours.
        if (!gameManager.bordersUp.Contains(gameObject) && !gameManager.bordersDown.Contains(gameObject)
           && !gameManager.bordersLeft.Contains(gameObject) && !gameManager.bordersRight.Contains(gameObject))
        {
            neighbours.Add(gameManager.allPlanesList[uniqueInt - width]);
            neighbours.Add(gameManager.allPlanesList[uniqueInt + width]);
            neighbours.Add(gameManager.allPlanesList[uniqueInt - 1]);
            neighbours.Add(gameManager.allPlanesList[uniqueInt + 1]);
            return;
        }

        if (gameManager.bordersLeft.Contains(gameObject))
        {
            if (gameManager.bordersLeft[0] == gameObject)
            {
                neighbours.Add(gameManager.bordersDown[1]);
                neighbours.Add(gameManager.bordersLeft[1]);
                return;
            }
            if (gameManager.bordersLeft[0] != gameObject &&
                gameManager.bordersUp[0] != gameObject)
            {
                neighbours.Add(gameManager.allPlanesList[uniqueInt - width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt + width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt + 1]);
                return;
            }
        }
        if (gameManager.bordersRight.Contains(gameObject))
        {
            if (gameManager.bordersRight[0] == gameObject)
            {
                neighbours.Add(gameManager.bordersDown[gameManager.bordersDown.Count - 2]);
                neighbours.Add(gameManager.bordersRight[1]);
                return;
            }
            if (gameManager.bordersRight[0] != gameObject &&
                gameManager.bordersRight[gameManager.bordersRight.Count - 1] != gameObject)
            {
                neighbours.Add(gameManager.allPlanesList[uniqueInt - width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt + width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt - 1]);
                return;
            }
        }
        if (gameManager.bordersDown.Contains(gameObject))
        {
            if (gameManager.bordersDown[0] != gameObject &&
                gameManager.bordersDown[gameManager.bordersDown.Count - 1] != gameObject)
            {
                neighbours.Add(gameManager.allPlanesList[uniqueInt + width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt - 1]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt + 1]);
                return;
            }
        }
        if (gameManager.bordersUp.Contains(gameObject))
        {            
            if (gameManager.bordersUp[0] == gameObject)
            {
                neighbours.Add(gameManager.bordersLeft[gameManager.bordersLeft.Count - 2]);
                neighbours.Add(gameManager.bordersUp[1]);
                return;
            }
            if (gameManager.bordersUp[gameManager.bordersUp.Count - 1] == gameObject)
            {
                neighbours.Add(gameManager.bordersUp[gameManager.bordersUp.Count - 2]);
                neighbours.Add(gameManager.bordersRight[gameManager.bordersRight.Count - 2]);
                return;
            }
            if (gameManager.bordersUp[gameManager.bordersUp.Count - 1] != gameObject &&
                gameManager.bordersUp[0] != gameObject)
            {
                neighbours.Add(gameManager.allPlanesList[uniqueInt - width]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt - 1]);
                neighbours.Add(gameManager.allPlanesList[uniqueInt + 1]);
                return;
            }
        }       
    }
}
