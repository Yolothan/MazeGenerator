using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRep : MonoBehaviour
{
    public List<Transform> waypointList = new List<Transform>();
    public List<GameObject> allPlanes = new List<GameObject>();    
   public List<GameObject> closer = new List<GameObject>();
    public int startingPos;
    private GameManager gameManager;    
    private Transform targetCell;      
    public float movementSpeed = 3.0f;    
    private float rotationSpeed = 2.0f;
    public int randomIntMove, waypointReached, waypointCount;
    private bool openingOne = true, openingTwo = true;
    private GameObject parent;

    
        
    private void Start()
    {        
       gameManager = FindObjectOfType<GameManager>();

        waypointList = gameManager.unVisited;        
       
       allPlanes = gameManager.allPlanesList;           

        int randomInt = Random.Range(0, waypointList.Count - 1);
        waypointCount = waypointList.Count;
        targetCell = waypointList[randomInt];
        
        StartCoroutine(Move());
        

        
    }

    private void Update()
    {
        
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            Vector3 directionToTarget = targetCell.position - transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget.normalized);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

            float distanceTarget = Vector3.Distance(transform.position, targetCell.position);

            float distanceNeighbour = Vector3.Distance(transform.position, closer[randomIntMove].transform.position);


            CheckDistanceToWaypoint(distanceTarget, distanceNeighbour);
        //this makes sure the visualrepresentation moves along the grid because it goes to the position of the direct neighbour.
            transform.position = Vector3.MoveTowards(transform.position, closer[randomIntMove].transform.position, movementStep);
        //this destroys the visualrepresentation if the last target is reached.
            if (waypointReached == waypointCount)
            {
                if (transform.position == targetCell.position)
                {
                    Destroy(gameObject);
                }
            }
            //these two if-statements makes the first bordercell an opening for the start and finish.
            if (openingOne)
            {
                if (gameManager.bordersDown.Contains(closer[randomIntMove]))
                {
                    if (transform.position == closer[randomIntMove].transform.position)
                    {
                    parent = GameObject.Find("Maze");
                    MazePart mazePart = closer[randomIntMove].GetComponent<MazePart>();
                        GameObject mazeGate = Instantiate(mazePart.gateMaze);
                        mazeGate.transform.position = mazePart.gameObject.transform.position;
                        mazeGate.transform.rotation = Quaternion.Euler(0, 180, 0);
                        mazeGate.transform.parent = parent.transform;
                        Destroy(mazePart.parts[0]);
                        openingOne = false;
                    }
                }
            }
            if (openingTwo)
            {
                if (gameManager.bordersUp.Contains(closer[randomIntMove]))
                {

                    if (transform.position == closer[randomIntMove].transform.position)
                    {
                    parent = GameObject.Find("Maze");
                    MazePart mazePart = closer[randomIntMove].GetComponent<MazePart>();
                        GameObject mazeGate = Instantiate(mazePart.gateMaze);
                        mazeGate.transform.position = mazePart.gameObject.transform.position;
                        mazeGate.transform.parent = parent.transform;
                        Destroy(mazePart.parts[1]);
                        openingTwo = false;
                    }
                }
            }
        
    }

    void CheckDistanceToWaypoint(float currentDistanceTarget, float currentDistanceNeighbour)
    {
        if (currentDistanceTarget <= 0)
        {                           
            UpdateTargetWaypoint();
            waypointReached++;
        }
        if(currentDistanceNeighbour <= 0)
        {            
                closer.Clear();
                StartCoroutine(Move());            
        }
    }

    void UpdateTargetWaypoint()
    {                    
            int randomInt = Random.Range(0, waypointList.Count - 1);
        //randomly chosing a targetwaypoint.
            if (randomInt > 0)
            {
                targetCell = waypointList[randomInt];
            }
            else
            {
                targetCell = waypointList[0];                
            }
            gameManager.unVisited.Remove(targetCell);          
    }

    IEnumerator Move()
    {
        
            MazePart mazePart = allPlanes[startingPos].GetComponent<MazePart>();
        //this part determines which one of the four neighbours is closer to the target. It uses if-statements to check
        //if a mazePart is a corner or is at the border because it has different neighbours in that case.
            for (int i = 0; i < mazePart.neighbours.Count; i++)
            {           
                if(targetCell.position.x < allPlanes[startingPos].transform.position.x)
                {                
                    if(mazePart.neighbours[i].transform.position.x < allPlanes[startingPos].transform.position.x)
                    {
                        closer.Add(mazePart.neighbours[i]);
                    }
                }
                if (targetCell.position.z < allPlanes[startingPos].transform.position.z)
                {                
                    if (mazePart.neighbours[i].transform.position.z < allPlanes[startingPos].transform.position.z)
                        {
                            closer.Add(mazePart.neighbours[i]);
                        }
                }
                if (targetCell.position.x > allPlanes[startingPos].transform.position.x)
                {               
                    if (mazePart.neighbours[i].transform.position.x > allPlanes[startingPos].transform.position.x)
                        {
                            closer.Add(mazePart.neighbours[i]);
                        }
                }
                if (targetCell.position.z > allPlanes[startingPos].transform.position.z)
                {               
                    if (mazePart.neighbours[i].transform.position.z > allPlanes[startingPos].transform.position.z)
                        {
                            closer.Add(mazePart.neighbours[i]);
                        }
                }
            }

            //Sometimes two neighbours are the same distance to the target and this chooses a random cell to go to next.
            randomIntMove = Random.Range(0, closer.Count - 1);            

            startingPos = closer[randomIntMove].GetComponent<MazePart>().uniqueInt; 
        //this removes the cells which are visited without it being a target.
        if(gameManager.unVisited.Contains(closer[randomIntMove].transform))
        {
            waypointReached++;
            gameManager.unVisited.Remove(closer[randomIntMove].transform);
        }
        
        yield return null;
    }   
}
