using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public MazePart part;
    public MazePart partNeighbour;
    public int idNeighbourWall, uniqueId;
    public GameObject neighbourWall;
    public GameObject parentObject;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        AssignNeighbours();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "VisRep")
        {            
            if (part.destroyCount < 1)
            {
                partNeighbour.destroyCount++;
                part.destroyCount++;
                Destroy(neighbourWall);
                Destroy(gameObject);
            }
        }
    }

    private void AssignNeighbours()
    {
        if (!gameManager.bordersUp.Contains(parentObject) && !gameManager.bordersDown.Contains(parentObject)
            && !gameManager.bordersLeft.Contains(parentObject) && !gameManager.bordersRight.Contains(parentObject))
        {
            partNeighbour = part.neighbours[uniqueId].GetComponent<MazePart>();
            neighbourWall = partNeighbour.parts[idNeighbourWall];
            return;
        }


        if (gameManager.bordersLeft.Contains(parentObject))
        {
            if (gameManager.bordersLeft[0] == parentObject)
            {
                if (gameObject.name == "2")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[3];
                }
                else if(gameObject.name == "1")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[0];
                }
                return;
            }
            if (gameManager.bordersLeft[0] != parentObject &&
                gameManager.bordersUp[0] != parentObject)
            {
                if (gameObject.name == "2")
                {
                    partNeighbour = part.neighbours[2].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[3];
                }
                else if (gameObject.name == "1")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[0];
                }
                else if (gameObject.name == "0")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[1];
                }
                return;
            }
        }
        if (gameManager.bordersRight.Contains(parentObject))
        {
            if (gameManager.bordersRight[0] == parentObject)
            {
                if (gameObject.name == "3")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[2];
                }
                else if (gameObject.name == "1")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[0];
                }
                return;
            }

            if (gameManager.bordersRight[0] != parentObject &&
                gameManager.bordersRight[gameManager.bordersRight.Count - 1] != parentObject)
            {
                if (gameObject.name == "1")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[0];
                }
                else if (gameObject.name == "3")
                {
                    partNeighbour = part.neighbours[2].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[2];
                }
                else if (gameObject.name == "0")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[1];
                }
                return;
            }
        }
        if (gameManager.bordersDown.Contains(parentObject))
        {
            if (gameManager.bordersDown[0] != parentObject &&
                gameManager.bordersDown[gameManager.bordersDown.Count - 1] != parentObject)
            {
                if (gameObject.name == "1")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[0];
                }
                else if (gameObject.name == "3")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[2];
                }
                else if (gameObject.name == "2")
                {
                    partNeighbour = part.neighbours[2].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[3];
                }
                return;
            }
        }
        if (gameManager.bordersUp.Contains(parentObject))
        {
            if (gameManager.bordersUp[0] == parentObject)
            {
                if (gameObject.name == "2")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[3];
                }
                else if (gameObject.name == "0")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[1];
                }
                return;
            }
            if (gameManager.bordersUp[gameManager.bordersUp.Count - 1] == parentObject)
            {
                if (gameObject.name == "3")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[2];
                }
                else if (gameObject.name == "0")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[1];
                }
                return;
            }
            if (gameManager.bordersUp[gameManager.bordersUp.Count - 1] != parentObject &&
                gameManager.bordersUp[0] != parentObject)
            {
                if (gameObject.name == "0")
                {
                    partNeighbour = part.neighbours[0].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[1];
                }
                else if (gameObject.name == "3")
                {
                    partNeighbour = part.neighbours[1].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[2];
                }
                else if (gameObject.name == "2")
                {
                    partNeighbour = part.neighbours[2].GetComponent<MazePart>();
                    neighbourWall = partNeighbour.parts[3];
                }
                return;
            }

        }
        
    }
}
