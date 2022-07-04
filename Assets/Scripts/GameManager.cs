using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject plane, visualRepresentation;
    public GameObject UI, gameUI;
    public int width, height;
    private int i;
    private bool timer = false;
    private List<GameObject> planes = new List<GameObject>();       
    [HideInInspector]
    public List<GameObject> allPlanesList = new List<GameObject>();
    [HideInInspector]
    public List<Transform> unVisited = new List<Transform>();
    [HideInInspector]
    public List<GameObject> bordersLeft = new List<GameObject>();
   [HideInInspector]
    public List<GameObject> bordersRight = new List<GameObject>();
   [HideInInspector]
    public List<GameObject> bordersUp = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> bordersDown = new List<GameObject>();      
 
    // Start is called before the first frame update
    public void StartGenerating()
    {        
        UI.SetActive(false);
        gameUI.SetActive(true);
        StartCoroutine(StartGeneratingPlane());        
    }    

    public void StartRegenerate()
    {
        //This code is used to regenerate the maze. First the VisRep is deleted and after all the lists are cleared before so
        //the maze can be regenerated.
        if(GameObject.FindWithTag("VisRep") != null)
        {
            GameObject visRep = GameObject.FindWithTag("VisRep");
            visRep.GetComponent<VisualRep>().enabled = false;
            Destroy(visRep);
        }
        GameObject maze = GameObject.Find("Maze");
        allPlanesList.Clear();
        unVisited.Clear();
        bordersLeft.Clear();
        bordersRight.Clear();
        bordersUp.Clear();
        bordersDown.Clear();
        planes.Clear();   
        Destroy(maze);
        timer = true;        
    }

    private void Update()
    {
        //Not ideal solution, but if the app started regenerating a maze errors occured because the code was not
        //executed in the right order. This delay is implemented to force the app in the right order.
        if(timer)
        {
            i = i + 1;
            if(i == 10)
            {
                StartCoroutine(StartGeneratingPlane());
                i= 0;
                timer = false;
            }
        }
    }
    public void ReturnMenu()
    {
        //this is the same code as the StartRegenerating() but without the generating-part because this is used to go back to the menu
        //and generate from there.
        if (GameObject.FindWithTag("VisRep") != null)
        {
            GameObject visRep = GameObject.FindWithTag("VisRep");
            Destroy(visRep);
        }
        GameObject maze = GameObject.Find("Maze");
        allPlanesList.Clear();
        unVisited.Clear();
        bordersLeft.Clear();
        bordersRight.Clear();
        bordersUp.Clear();
        bordersDown.Clear();
        planes.Clear();
        Destroy(maze);        
    }

     IEnumerator StartGeneratingPlane()
    {
        int counterHeight = 1;
        int counterName = 0;
        GameObject parent = new GameObject("Maze");
        //all the planes are instantiated sideways
        while (planes.Count < width && height > 0)
        {
            GameObject newPlane = Instantiate(plane);
            newPlane.name = planes.Count.ToString();
            newPlane.transform.parent = parent.transform;
            allPlanesList.Add(newPlane);
            unVisited.Add(newPlane.transform);
            
            if(GameObject.Find((planes.Count - 1).ToString()) != null)
            {
                GameObject oldPlane = GameObject.Find((planes.Count - 1).ToString());
                newPlane.transform.position = new Vector3(oldPlane.transform.position.x+1, oldPlane.transform.position.y, oldPlane.transform.position.z);
            }
            planes.Add(newPlane);
            bordersDown.Add(newPlane);
            
        }

        //Detecting the first and last of the down row to add to the right and left border.
        bordersLeft.Add(bordersDown[0]);     
        bordersRight.Add(bordersDown[bordersDown.Count-1]);
        //all the planes are instantiated in the height.
        while (height > 1 && counterHeight < height)
        {            
            for (int i = 0; i < planes.Count; i++)
            {                
                GameObject newPlane = Instantiate(plane);
                newPlane.name = (planes.Count + counterName).ToString();
                newPlane.transform.position = new Vector3(planes[i].transform.position.x, planes[i].transform.position.y, 
                    planes[i].transform.position.z + counterHeight);
                newPlane.transform.parent = parent.transform;
                counterName++;
                allPlanesList.Add(newPlane);
                unVisited.Add(newPlane.transform);

                if(counterHeight == (height - 1))
                {
                    bordersUp.Add(newPlane);
                }

                //The first row has always a x-value of 0, is if the plane has this value it can be added to the left border.
                if(newPlane.transform.position.x == 0)
                {
                    bordersLeft.Add(newPlane);                    
                }
                //The last row has always an x-value of the width - 1, so if the plane has this value it can be added to the right border.
                if(newPlane.transform.position.x == (width - 1))
                {
                    bordersRight.Add(newPlane);
                }
                
            }
            counterHeight++;
        }
        planes.Clear();
            
        StartCoroutine(GenerateMaze());
        yield return null;
    }

    IEnumerator GenerateMaze()
    {       
        GameObject visualRep = Instantiate(visualRepresentation);
        VisualRep visualRepCom = visualRep.GetComponent<VisualRep>();        
        visualRep.name = "VisualRepresentation";
               
        int randomNumber = Random.Range(0, allPlanesList.Count - 1);
        visualRepCom.startingPos = randomNumber;
        

        visualRep.transform.position = new Vector3(allPlanesList[randomNumber].transform.position.x, visualRep.transform.position.y, 
            allPlanesList[randomNumber].transform.position.z);        
        unVisited.Remove(allPlanesList[randomNumber].transform);
        

        yield return null;
    }   
}
